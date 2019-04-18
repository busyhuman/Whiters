using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class csLobbyMgr : MonoBehaviour {

	private static csLobbyMgr _instance = null;
	public static int currentCharacter = 1;

	public static csLobbyMgr Instance {
		get {
			if (_instance == null) {
				Debug.LogError ("cslobbyMgr == null");
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
