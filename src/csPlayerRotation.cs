using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPlayerRotation : MonoBehaviour {

	void Start () {
		gameObject.transform.LookAt  (GameObject.Find ("Center").transform);
		if (gameObject.transform.position.x > 0)
			gameObject.GetComponent<csNetPlayerCtrl> ().cDirection = false;
	}
		
}
