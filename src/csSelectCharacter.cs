using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSelectCharacter : MonoBehaviour {
	public int characterNumber;
	public GameObject character;
	public GameObject beacon;
	public GameObject name;

	public GameObject[] others;
	public GameObject[] otherbeacons;
	public GameObject[] othernames;

	void Start(){
		if (characterNumber == csLobbyMgr.currentCharacter)
			ChangeCharacterAndBeacon ();
	}

	void ChangeCharacterAndBeacon(){
		character.SetActive (false);
		beacon.SetActive (false);
		name.SetActive (false);
		for (int i = 0; i < others.Length; i++) {
			others [i].SetActive (true);
			otherbeacons [i].SetActive (true);
			othernames [i].SetActive (true);
		}
	}
}
