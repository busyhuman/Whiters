   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class csComputer : MonoBehaviour {
	public enum PlayerState{idle,mvForward,mvBackward,Lst,Rnd,crouch,crForward,Up,hit};
	public PlayerState playerstate = PlayerState.idle;
	public int doISetActive;
	public GameObject lefthand;
	public GameObject righthand;
	public GameObject leftleg;
	public GameObject rightleg;
	public float movingAttackSpeed = 0.3f;
	public float dashFspeed = 3.0f;
	public float dashBspeed = 1.5f;
	public float attackDist = 1.2f;
	public float walkDist = 2000.0f;
	public int UIC=0;
	public int LR = 0;
	private int whereIsHit;
	private Animator animator;
	private Transform tr;
	private Transform targetTr;
	private Vector3 moveDir;
	private int Acnt = 0;
	private int Bcnt = 0;
	public int doubleFcnt = 0;
	private int doubleBcnt = 0;
	private float idleCool = 0.0f;
	private bool idlestart = false;
	private bool crouch = false;
	private const float actionDelay = 0.5f;
	private NavMeshAgent nvAgent;
	private float defaultSpeed = 0.9f;
	private GameObject[] humans;

	void Awake(){
		tr = GetComponent<Transform> ();
		animator = GetComponent<Animator> ();
		nvAgent = GetComponent<NavMeshAgent> ();
		nvAgent.speed = defaultSpeed;
	}

	void Start(){
		targetTr = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		StartCoroutine (checkAIDistance ());
		StartCoroutine (checkAIAction ());
		StartCoroutine (changeUIC ());
		StartCoroutine (changeLR ());
	}

	void initIdle(){
		idlestart = false;
		idleCool = 0.0f;
	}

	IEnumerator  changeUIC(){
		while (true) {
			yield return new WaitForSeconds (Random.Range (1.0f, 3.0f));
			UIC = (int)Random.Range (0.0f, 2.99f);
		}
	}

	IEnumerator changeLR(){
		while (true) {
			yield return new WaitForSeconds (Random.Range (2.0f, 5.0f));
			LR = (int)Random.Range (0.0f, 1.99f);
		}
	}

	IEnumerator checkAIDistance(){
		while (true) {
			yield return new WaitForSeconds (0.2f);
			float dist;
			dist = Vector3.Distance (tr.position, targetTr.position);

			if (dist <= attackDist) {
			//	if(!(targetTr.gameObject.GetComponent<csPlayerCtrl>().playerstate == PlayerState.idle || targetTr.gameObject.GetComponent<csPlayerCtrl>().playerstate == PlayerState.crouch) )
				if (LR == 0) {
					playerstate = PlayerState.Lst;
				} else if (LR == 1) {
					playerstate = PlayerState.Rnd;
				}
				yield return new WaitForSeconds (0.15f);
				if (UIC == 0) {
					animator.SetBool ("Up", true);
					animator.SetBool ("crouch", false);
				} else if (UIC == 1) {
					animator.SetBool ("Up", false);
					animator.SetBool ("crouch", false);
				} else if (UIC == 2) {
					animator.SetBool ("Up", false);
					animator.SetBool ("crouch", true);
				}
			} else if(dist <=walkDist){
				playerstate = PlayerState.mvForward;
			}
			initIdle ();

		}
	}


	IEnumerator checkAIAction(){
		while (true) {
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
				nvAgent.Resume ();
				nvAgent.destination = targetTr.position;
				animator.SetInteger ("double_F", doubleFcnt);
				if (doubleFcnt > 1) {
					nvAgent.speed = dashFspeed;
				} else
					nvAgent.speed = defaultSpeed;
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
					nvAgent.speed = dashBspeed;
				} else
					nvAgent.speed = defaultSpeed;
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
				doubleBcnt = 0;
				animator.SetInteger ("double_B", 0);
				nvAgent.speed = defaultSpeed;
				nvAgent.Stop ();
			} else if (playerstate == PlayerState.Lst) {
				animator.SetInteger ("Lst", Acnt);
				animator.SetBool ("mvF", false);
				animator.SetBool ("mvB", false);
				animator.SetInteger ("Rnd", 0);
				Bcnt = 0;
				doISetActive = 1;
				doubleBcnt = 0;
				animator.SetInteger ("double_B", 0);
				animator.SetInteger ("Lst", 10);
				nvAgent.Stop ();
				tr.Translate (Vector3.forward * movingAttackSpeed * Time.deltaTime);
			} else if (playerstate == PlayerState.Rnd) {
				animator.SetInteger ("Rnd", Bcnt);
				animator.SetBool ("mvF", false);
				animator.SetBool ("mvB", false);
				animator.SetInteger ("Lst", 0);
				Acnt = 0;
				doISetActive = 2;
				doubleBcnt = 0;
				animator.SetInteger ("double_B", 0);
				animator.SetInteger ("Rnd", 10);
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
			yield return new WaitForSeconds (0.1f);
		}
	}

	void FixedUpdate(){
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
	}
}
