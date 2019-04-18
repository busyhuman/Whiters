using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csHpBar : MonoBehaviour {

	public GameObject obj;
	public float left = 0.0f;
	public float up = 0.0f;

	void Update () {
		transform.Rotate (0, 0, 0);
		transform.position = new Vector3 (obj.transform.position.x + left, obj.transform.position.y + up, obj.transform.position.z);
	}
}
