using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csZonbieGameMgr : MonoBehaviour {

	public GameObject[] Characters;
	private int CharacterNumber;
	private int Clen;
	public int AIDeadCnt;
	private GameObject UIMgr;
	void Awake(){
		UIMgr = GameObject.FindGameObjectWithTag ("UIMgr");
		CharacterNumber = csLobbyMgr.currentCharacter;
		Clen = Characters.Length;
		for (int i = 0; i < Clen; i++) {
			if (i == CharacterNumber) {
				Characters [i].SetActive (true);
				continue;
			}
			Characters [i].SetActive (false);
		}
	}

	void Start(){
		Destroy (GameObject.FindGameObjectWithTag ("LobbyManager"));
		AIDeadCnt = 0;
		UIMgr.SendMessage ("showAIKilled", AIDeadCnt, SendMessageOptions.DontRequireReceiver);
	}

	void changeAIcount(){
		AIDeadCnt++;
	}


}
