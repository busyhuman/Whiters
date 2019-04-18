using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGizmos : MonoBehaviour {
	public Color _color = Color.yellow;
	public float _radius = 0.3f;

	void OnDrawGizmos(){
		Gizmos.color = _color;
		Gizmos.DrawSphere (this.transform.position, _radius);
	}
}