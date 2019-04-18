using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csShadow : MonoBehaviour {

	public GameObject obj;

	void Update () {
		if (!obj.activeSelf) {
			gameObject.SetActive (false);
		}
		transform.position = new Vector3 (obj.transform.position.x, this.gameObject.transform.position.y, obj.transform.position.z);
	}
}
