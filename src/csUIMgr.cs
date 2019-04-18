using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class csUIMgr : MonoBehaviour {
	public Text txt1;
	public Text txt2;
	public GameObject panel1;
	public GameObject LobbyMgr;
	private GameObject gameMgr;

	void Update(){
		if (Input.GetKeyUp(KeyCode.Escape)) {
			OnClickEscapeBtn ();
		}
	}

	void Awake(){
		gameMgr = GameObject.FindGameObjectWithTag ("gameMgr");
	}

	void showAIKilled(){
		int num = gameMgr.GetComponent<csZonbieGameMgr> ().AIDeadCnt;
		if (txt1.IsActive() && !panel1.activeSelf) {
			txt1.text = num.ToString ();
		}
	}

	void showGameOverMenu(){
		panel1.SetActive (true);
	}

	 void OnClickEscapeBtn(){
		if (panel1.activeSelf)
			panel1.SetActive (false);
		else
			panel1.SetActive (true);
	}

	public void OnClickBtnVsCom(){
		SceneManager.LoadScene ("vsCom");
	}

	public void OnClickBtnVsZombies(){
		SceneManager.LoadScene ("vsZombies");
	}

	public void OnClickBtnLobby(){
		SceneManager.LoadScene ("Lobby");
	}

	public void OnCloseLobbyMgr(){
	//	LobbyMgr.SetActive (false);
		Destroy (GameObject.FindGameObjectWithTag("LobbyManager"));
	}

	public void OnClickBtnVsPlayer(){
		//NetworkLobbyManager.singleton.StopClient ();
		NetworkLobbyManager.singleton.StopMatchMaker();
	}
}
