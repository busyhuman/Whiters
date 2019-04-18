using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csKeypadMgr : MonoBehaviour {

	private static csKeypadMgr _instance = null;

	public static csKeypadMgr Instance {
		get {
			if (_instance == null) {
				Debug.LogError ("csKeypadMgr == null");
			}

			return _instance;
		}
	}

	// Use this for initialization
	void Awake() {
		if (_instance) {
			DestroyImmediate (this);
		} else {
			_instance = this;
			DontDestroyOnLoad (this);
		}
	}

}
