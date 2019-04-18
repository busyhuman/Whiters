using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csAIgameMgr : MonoBehaviour {
	public GameObject[] AI;
	private int AIlen;
	private GameObject activeAI;
	private GameObject UIMgr;

	void Awake(){
		UIMgr = GameObject.FindGameObjectWithTag ("UIMgr");
	}

	// Use this for initialization
	void Start () {
		AIlen = AI.Length;
		int randomNumber = Random.Range (0, AIlen);
		AI [randomNumber].SetActive (true);
		activeAI = GameObject.FindGameObjectWithTag ("Com");
		StartCoroutine (checkGameOver ());
	}

	IEnumerator checkGameOver(){
		while (true) {
			yield return new WaitForSeconds (1.0f);
			if (!activeAI.activeSelf) {
				UIMgr.SendMessage ("showGameOverMenu", SendMessageOptions.DontRequireReceiver);
				break;
			}
		}
	}

}
