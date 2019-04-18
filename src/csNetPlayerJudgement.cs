using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csNetPlayerJudgement : MonoBehaviour {

	public GameObject Player;
	private Animator animator;
	private Transform tr;

// 	[SyncVar(hook = "OnChangeStamina")]
	[SerializeField] private int stamina;

	public int maxStamina = 12;
	private float staminaDelay = 9.0f;

	void Awake(){
		animator = GetComponentInParent<Animator> ();
	}

	void Start(){
		tr = Player.GetComponent<Transform> ();
		StartCoroutine (initStamina ());
		stamina = maxStamina;
	}
	   
	IEnumerator initStamina(){
		while (true) {
			stamina = maxStamina;
			yield return new WaitForSeconds (staminaDelay);
		}
	}

	// 체력데미지 감소량, 넉백거리, 스태미나 감소량
	void whenMyCharacterIsHit(int healthDamage, int distance, int staminaDamage){
		Player.SendMessage ("subtractMyHealth", healthDamage, SendMessageOptions.DontRequireReceiver);
		tr.Translate (Vector3.forward * -distance * Time.deltaTime);
//		stamina -= staminaDamage;
	}

	void OnChangeStamina(int _stamina){
		stamina = _stamina;
	}

	void OnTriggerEnter(Collider coll){
		int idleOrCrouch = 0;
		if (Player.tag == "Player") {
			idleOrCrouch = Player.GetComponent<csNetPlayerCtrl> ().doISetActive;
		} else if(Player.tag == "AI") {
			idleOrCrouch = Player.GetComponent<csAICtrl> ().doISetActive;
		}

		if (coll.gameObject.tag == "leftleg") {
			if (gameObject.tag == "ishit1") {
				animator.SetTrigger ("hit1");
				whenMyCharacterIsHit (10, 5, 4);
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock1");
				}
			} else if (gameObject.tag == "ishit2") {
				if (idleOrCrouch == 0) {
					animator.SetTrigger ("guard2_L");
					whenMyCharacterIsHit (0, 3, 3);
				} else {
					animator.SetTrigger ("hit2_L");
					whenMyCharacterIsHit (7, 5, 4);
				}
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock2");
				}
			} else if (gameObject.tag == "ishit3") {
				if (idleOrCrouch == 4) {
					whenMyCharacterIsHit (0, 3, 2);
				} else {
					animator.SetTrigger ("hit3_L");
					whenMyCharacterIsHit (7, 5, 4);
				}
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock3");
				}
			}
				
		} else if (coll.gameObject.tag == "rightleg") {
			if (gameObject.tag == "ishit1") {
				animator.SetTrigger ("hit1");
				whenMyCharacterIsHit (10, 5, 4);
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock1");
				}
			} else if (gameObject.tag == "ishit2") {
				if (idleOrCrouch == 0) {
					animator.SetTrigger ("guard2_R");
					whenMyCharacterIsHit (0, 3, 3);
				} else {
					animator.SetTrigger ("hit2_R");
					whenMyCharacterIsHit (7, 5, 4);
				}
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock2");
				}
			} else if (gameObject.tag == "ishit3") {
				if (idleOrCrouch == 4) {
					whenMyCharacterIsHit (0, 3, 2);
				} else {
					animator.SetTrigger ("hit3_R");
					whenMyCharacterIsHit (7, 5, 4);
				}
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock3");
				}
			}



		} else if (coll.gameObject.tag == "leftarm") {
			if (gameObject.tag == "ishit1") {
				animator.SetTrigger ("hit1");
				whenMyCharacterIsHit (7, 3, 4);
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock1");
				}
			} else if (gameObject.tag == "ishit2") {
				if (idleOrCrouch == 0) {
					animator.SetTrigger ("guard2_L");
					whenMyCharacterIsHit (0, 2, 2);
				} else {
					animator.SetTrigger ("hit2_L");
					whenMyCharacterIsHit (4, 3, 3);
				}
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock2");
				}
			} else if (gameObject.tag == "ishit3") {
				if (idleOrCrouch == 4) {
					whenMyCharacterIsHit (0, 2, 2);
				} else {
					animator.SetTrigger ("hit3_L");
					whenMyCharacterIsHit (4, 3, 3);
				}
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock3");
				}
			}




		} else if (coll.gameObject.tag == "rightarm") {
			if (gameObject.tag == "ishit1") {
				animator.SetTrigger ("hit1");
				whenMyCharacterIsHit (7, 3, 4);
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock1");
				}
			} else if (gameObject.tag == "ishit2") {
				if (idleOrCrouch == 0) {
					animator.SetTrigger ("guard2_R");
					whenMyCharacterIsHit (0, 2, 2);
				} else {
					animator.SetTrigger ("hit2_R");
					whenMyCharacterIsHit (4, 3, 3);
				}
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock2");
				}
			} else if (gameObject.tag == "ishit3") {
				if (idleOrCrouch == 4) {
					whenMyCharacterIsHit (0, 2, 2);
				} else {
					animator.SetTrigger ("hit3_R");
					whenMyCharacterIsHit (4, 3, 3);
				}
				if (stamina <= 0) {
					stamina = maxStamina;
					animator.SetTrigger ("knock3");
				}
			}
		}

	}
}
