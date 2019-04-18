using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class csCamera : MonoBehaviour {
	static public Transform targetTr;
	public float dist = 3.0f;
	public float height = 1.5f;
	public float dampTrace = 25.0f;
	private bool stop = false;
	private Transform tr;

	void Start () {
		tr = GetComponent<Transform> ();
		StartCoroutine (checkTargetPosition ());
	}

	IEnumerator checkTargetPosition(){
		while (!stop) {
			if (targetTr) {
				if (targetTr.gameObject.GetComponent<csNetPlayerCtrl> ().cDirection == false) {
					Debug.Log ("woww");
					dist = 3.0f;
				}
				stop = true;
			}

			yield return new WaitForSeconds (0.1f);
		}
	}

	void FixedUpdate(){
		tr.position = Vector3.Lerp (tr.position, targetTr.position - (targetTr.right * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);
	}
}

