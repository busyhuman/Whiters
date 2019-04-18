using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Animator))]
public class csPlayerCtrl1 : MonoBehaviour {
	/* 위에서부터
	 * 플레이어 상태목록
	 * 현재 플레이어 상태 0 = idle 1 = 주먹 2 = 발 3 = 나머지 4 = 앉기
	 * 애니메이터
	 * 트랜스폼
	 * 벡터3
	 * 주먹콤보버튼
	 * 발차기콤보버튼
	 * 기본상태로 돌아가기 위한 시간을 잼
	 * 버튼입력이없으면 true로 바뀜
	 * 엎드려있는지 확인
	 * 캐릭터이동속도
	 * idlecool > actiondelay가 되면 idle 상태로 돌아간다.
	 */

	public enum PlayerState{idle,mvForward,mvBackward,Lst,Rnd,crouch,crForward,Up,hit};
	public PlayerState playerstate = PlayerState.idle;
	public int doISetActive;
	public GameObject lefthand;
	public GameObject righthand;
	public GameObject leftleg;
	public GameObject rightleg;
	public float movingAttackSpeed=1.0f;
	public float dashFspeed = 3.0f;
	public float dashBspeed = 1.5f;
	public float defaultSpeed = 1.0f;
	private int whereIsHit;
	private Animator animator;
	private Transform tr;
	private Vector3 moveDir;
	private int Acnt = 0;
	private int Bcnt = 0;
	private int doubleFcnt = 0;
	private int doubleBcnt = 0;
	private float idleCool = 0.0f;
	private bool idlestart = false;
	private bool crouch = false;
	public float moveSpeed;
	private const float actionDelay = 0.5f;
	private GameObject gameMgr;
	private GameObject UIMgr;
	void Awake(){
		tr = GetComponent<Transform> ();
		animator = GetComponent<Animator> ();
		gameMgr = GameObject.FindGameObjectWithTag ("gameMgr");
		UIMgr = GameObject.FindGameObjectWithTag ("UIMgr");
	}

	void Start(){
		moveSpeed = defaultSpeed;
	}

	void OnEnable(){
		gameObject.tag = "Player";
		gameObject.GetComponent<csPlayerCtrl> ().enabled = true;
	}

	// 어느 버튼이든 입력되었을 때 실행시키는 함수
	void initIdle(){
		idlestart = false;
		idleCool = 0.0f;
	}

	void FixedUpdate(){
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		float v = CrossPlatformInputManager.GetAxis ("Vertical");
		moveDir = h * Vector3.forward; //+ v*Vector3.right;
		tr.Translate (moveDir * moveSpeed * Time.deltaTime);
		
		if (doISetActive == 0 || doISetActive == 3 || doISetActive == 4) {
			lefthand.SetActive(false);
			leftleg.SetActive(false);
			righthand.SetActive(false);
			rightleg.SetActive(false);
		}
		else if (doISetActive == 1) {
			lefthand.SetActive(true);
			leftleg.SetActive(false);
			righthand.SetActive(true);
			rightleg.SetActive(false);
		}
		else if (doISetActive == 2) {
			lefthand.SetActive(false);
			leftleg.SetActive(true);
			righthand.SetActive(false);
			rightleg.SetActive(true);
		}
	}

	void Update(){
		if (idlestart) {
			idleCool += Time.deltaTime;
			if (idleCool > actionDelay) {
				playerstate = PlayerState.idle;
				idlestart = false;
			}
		}

		if (Input.GetKey (KeyCode.S)) {
			crouch = true;
			playerstate = PlayerState.crouch;
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			animator.SetBool ("Up", true);
		}

		if (Input.GetKeyDown (KeyCode.D)) {
			initIdle ();
			doubleFcnt++;
			animator.SetInteger ("double_F", doubleFcnt);
		} else if (Input.GetKeyDown (KeyCode.A)) {
			initIdle ();
			doubleBcnt++;
			animator.SetInteger ("double_B", doubleBcnt);
		}

		if (Input.GetKey (KeyCode.D)) {
			if (crouch) {
				playerstate = PlayerState.crForward;
			} else {
				playerstate = PlayerState.mvForward;
			}
		} else if (Input.GetKey (KeyCode.A)) {
			if (crouch) {
				crouch = !crouch;
			}
			playerstate = PlayerState.mvBackward;
		} else if (Input.GetKeyDown (KeyCode.Alpha1) && playerstate != PlayerState.Rnd) {
			initIdle ();
			++Acnt;
			playerstate = PlayerState.Lst;
		} else if (Input.GetKeyDown (KeyCode.Alpha2) && playerstate != PlayerState.Lst) {
			initIdle ();
			++Bcnt;
			playerstate = PlayerState.Rnd;
		}

		if (!Input.anyKey || playerstate != PlayerState.mvForward || playerstate != PlayerState.mvBackward) {
			idlestart = true;
		}  

		//	Debug.Log ("Acnt = " + Acnt.ToString ());
		//	Debug.Log ("idlecool " + idleCool.ToString ());
		if (playerstate == PlayerState.mvForward) {
			animator.SetBool ("mvF", true);
			animator.SetBool ("mvB", false);
			animator.SetBool ("crouch", crouch);
			animator.SetInteger ("Lst", 0);
			Acnt = 0;
			animator.SetInteger ("Rnd", 0);
			Bcnt = 0;
			doISetActive = 3;
			doubleBcnt = 0;
			animator.SetInteger ("double_B", 0);
			if (doubleFcnt > 1) {
				moveSpeed = dashFspeed;
			} else
				moveSpeed = defaultSpeed;
		} else if (playerstate == PlayerState.mvBackward) {
			animator.SetBool ("mvB", true);
			animator.SetBool ("mvF", false);
			animator.SetBool ("crouch", crouch);
			animator.SetInteger ("Lst", 0);
			Acnt = 0;
			animator.SetInteger ("Rnd", 0);
			Bcnt = 0;
			doISetActive = 3;
			doubleFcnt = 0;
			animator.SetInteger ("double_F", 0);
			if (doubleBcnt > 1) {
				moveSpeed = dashBspeed;
			} else
				moveSpeed = defaultSpeed;
		} else if (playerstate == PlayerState.idle) {
			animator.SetBool ("mvF", false);
			animator.SetBool ("mvB", false);
			animator.SetInteger ("Lst", 0);
			Acnt = 0;
			animator.SetInteger ("Rnd", 0);
			Bcnt = 0;
			crouch = false;
			animator.SetBool ("crouch", crouch);
			animator.SetBool ("Up", false);
			doISetActive = 0;
			doubleFcnt = 0;
			doubleBcnt = 0;
			animator.SetInteger ("double_F", 0);
			animator.SetInteger ("double_B", 0);
			moveSpeed = defaultSpeed;
		} else if (playerstate == PlayerState.Lst) {
			animator.SetInteger ("Lst", Acnt);
			animator.SetBool ("mvF", false);
			animator.SetBool ("mvB", false);
			animator.SetInteger ("Rnd", 0);
			Bcnt = 0;
			doISetActive = 1;
			doubleFcnt = 0;
			doubleBcnt = 0;
			animator.SetInteger ("double_F", 0);
			animator.SetInteger ("double_B", 0);
			tr.Translate (Vector3.forward * movingAttackSpeed * Time.deltaTime);
		} else if (playerstate == PlayerState.Rnd) {
			animator.SetInteger ("Rnd", Bcnt);
			animator.SetBool ("mvF", false);
			animator.SetBool ("mvB", false);
			animator.SetInteger ("Lst", 0);
			Acnt = 0;
			doISetActive = 2;
			doubleFcnt = 0;
			doubleBcnt = 0;
			animator.SetInteger ("double_F", 0);
			animator.SetInteger ("double_B", 0);
		} else if (playerstate == PlayerState.crouch) {
			animator.SetBool ("crouch", crouch);
			animator.SetBool ("mvF", false);
			animator.SetBool ("mvB", false);
			animator.SetInteger ("Lst", 0);
			Acnt = 0;
			animator.SetInteger ("Rnd", 0);
			Bcnt = 0;
			doISetActive = 4;
			doubleFcnt = 0;
			doubleBcnt = 0;
			animator.SetInteger ("double_F", 0);
			animator.SetInteger ("double_B", 0);
		} else if (playerstate == PlayerState.crForward) {
			animator.SetBool ("crouch", crouch);
			animator.SetBool ("mvF", true);
			animator.SetBool ("mvB", false);
			animator.SetInteger ("Lst", 0);
			Acnt = 0;
			animator.SetInteger ("Rnd", 0);
			Bcnt = 0;
			doISetActive = 3;
			doubleFcnt = 0;
			doubleBcnt = 0;
			animator.SetInteger ("double_F", 0);
			animator.SetInteger ("double_B", 0);
		}
		/*else if (playerstate == PlayerState.knockdown) {
			animator.SetBool ("mvF", false);
			animator.SetBool ("mvB", false);
			animator.SetInteger ("Lst", 0);
			Acnt = 0;
			animator.SetInteger ("Rnd", 0);
			Bcnt = 0;
			doISetActive = 0;
		}*/
	}

	void PlayerDie(){
		UIMgr.SendMessage ("showGameOverMenu", SendMessageOptions.DontRequireReceiver);
	}
}