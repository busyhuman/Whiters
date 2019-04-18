using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class csStartMatchMaker : MonoBehaviour {
	public GameObject LobbyManagerPrefab;
	private GameObject LobbyManager;
//	public GameObject keypad;

	void StartMatchMaker(){
	//	LobbyManager.SetActive (true);
//		keypad.SetActive(false);
		Destroy (GameObject.Find ("LobbyManager"));	
		LobbyManager = (GameObject)Instantiate (LobbyManagerPrefab) as GameObject;
	}
}
