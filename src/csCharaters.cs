using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class csCharaters : MonoBehaviour {
	public GameObject[] players;
	public GameObject initPlace;

	void OnTriggerStay(Collider coll){
		if (CrossPlatformInputManager.GetButtonDown("left ctrl") || CrossPlatformInputManager.GetButtonDown("left alt")) {
			if (coll.gameObject.name == "selectCharacter0") {
				csLobbyMgr.currentCharacter = 0;
//				Debug.Log ("currentCharacter = " + csLobbyMgr.currentCharacter);
				coll.gameObject.SendMessage ("ChangeCharacterAndBeacon", SendMessageOptions.DontRequireReceiver);
			} else if (coll.gameObject.name == "selectCharacter1") {
				csLobbyMgr.currentCharacter = 1;
	//			Debug.Log ("currentCharacter = " + csLobbyMgr.currentCharacter);
				coll.gameObject.SendMessage ("ChangeCharacterAndBeacon", SendMessageOptions.DontRequireReceiver);
			} else if (coll.gameObject.name == "selectCharacter2") {
				csLobbyMgr.currentCharacter = 2;
		//		Debug.Log ("currentCharacter = " + csLobbyMgr.currentCharacter);
				coll.gameObject.SendMessage ("ChangeCharacterAndBeacon", SendMessageOptions.DontRequireReceiver);
			} else if (coll.gameObject.name == "StartMatchMaker") {
				coll.gameObject.SendMessage ("StartMatchMaker", SendMessageOptions.DontRequireReceiver);
			} else if (coll.gameObject.name == "selectScene0") {
				coll.gameObject.SendMessage ("loadvsCom",SendMessageOptions.DontRequireReceiver);
			} else if (coll.gameObject.name == "selectScene1") {
				coll.gameObject.SendMessage ("loadvsZombies",SendMessageOptions.DontRequireReceiver);
			} else if (coll.gameObject.name == "selectScene2") {
				coll.gameObject.SendMessage ("loadQuit",SendMessageOptions.DontRequireReceiver);
			}


			gameObject.transform.position = initPlace.transform.position;
			for (int i = 0; i < players.Length; i++)
				players [i].SetActive (false);
			players [csLobbyMgr.currentCharacter].SetActive (true);
		}


	}
}
