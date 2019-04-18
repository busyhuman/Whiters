using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csLobbySpawn : MonoBehaviour {
	public GameObject[] Characters;
	private int len;

	// Use this for initialization
	void Start () {
		len = Characters.Length;

		for (int i = 0; i < len; i++)
			Characters [i].SetActive (false);
		Characters [csLobbyMgr.currentCharacter].SetActive (true);
	}
}
