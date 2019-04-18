using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPlayerGameMgr : MonoBehaviour {

	public GameObject[] Characters;
	private int CharacterNumber;
	private int Clen;
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
}
