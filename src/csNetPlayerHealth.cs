using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class csNetPlayerHealth : NetworkBehaviour {
	public GameObject HpBar;

	[SyncVar(hook = "OnChangeHealth")]
	[SerializeField] private int health;

	public int maxHealth = 100;
	public int damageIncreasingRatio = 70;
	private Animator animator;
	private Transform hpbar;

	void Awake(){
		animator = GetComponent<Animator> ();
	}

	void OnEnable () {
		gameObject.GetComponent<CapsuleCollider> ().enabled = true;
		foreach (Collider coll in gameObject.GetComponentsInChildren<CapsuleCollider>())
			coll.enabled = true;
		foreach (Collider coll2 in gameObject.GetComponentsInChildren<BoxCollider>())
			coll2.enabled = true;

		if (HpBar != null) {
			HpBar.SetActive (true);
			hpbar = HpBar.transform.Find ("pivot").transform;
			hpbar.transform.localScale = new Vector3 (health, 1, 1);
		}
		health = maxHealth;
		OnChangeHealth (health);
	}

	// 현재체력 - 데미지 + 방어력
	public void subtractMyHealth(int value){
		if (!isServer) {
			return;
		}
//		Debug.Log ("health," + isServer);
		health = health - (int)((value * damageIncreasingRatio) / 100);
	}

	void OnChangeHealth(int _health){
		health = _health;
		if (HpBar != null) {
			if (_health > 0)
				hpbar.transform.localScale = new Vector3 (_health, 1, 1);
			else
				hpbar.transform.localScale = new Vector3 (0, 1, 1);
		}

		if (hpbar.transform.localScale.x <= 0) {
			if (gameObject.tag == "Player") {
				gameObject.GetComponent<csNetPlayerCtrl> ().playerstate = csNetPlayerCtrl.PlayerState.idle;
				SendMessage ("PlayerDie",SendMessageOptions.DontRequireReceiver);
				gameObject.GetComponent<csNetPlayerCtrl> ().enabled = false;
			} else if (gameObject.tag == "AI" || gameObject.tag == "C06") {
				gameObject.GetComponent<csAICtrl> ().playerstate = csAICtrl.PlayerState.idle;
				SendMessage ("AIDie",SendMessageOptions.DontRequireReceiver);
			}
			StartCoroutine (dead ());
		}
	}

	IEnumerator dead(){
		animator.SetTrigger ("isDie");
		if (HpBar != null)
			HpBar.SetActive (false);
		foreach (Collider coll in gameObject.GetComponentsInChildren<CapsuleCollider>())
			coll.enabled = false;
		foreach (Collider coll2 in gameObject.GetComponentsInChildren<BoxCollider>())
			coll2.enabled = false;
		gameObject.GetComponent<CapsuleCollider> ().enabled = false;
		yield return new WaitForSeconds (2.5f);

		gameObject.SetActive (false);
	}

}
