using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGateMgr : MonoBehaviour {
	public GameObject[] characterForSpawn;
	public GameObject Dahee;
	public GameObject spawnPoint;
	public GameObject spawnPoint2;

	public GameObject pad1;
	public GameObject pad2;

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.transform.position = spawnPoint.transform.position;
			coll.gameObject.SetActive (false);
			Dahee.transform.position = spawnPoint2.transform.position;
			characterForSpawn [csLobbyMgr.currentCharacter].SetActive (true);
		}

		if (gameObject.name == "gate1") {
			pad1.SetActive (false);
			pad2.SetActive (true);
		} else if (gameObject.name == "gate2") {
			pad1.SetActive (true);
			pad2.SetActive (false);
		}

	}
}
