using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csAIPool : MonoBehaviour {
	public GameObject[] AI;
	public GameObject[] spawnPosition;
	public GameObject[] MadGirl;
	public int maximumAI = 10;
	public float delay = 6.0f;
	private int AIlen;

	void Awake(){
		AIlen = AI.Length - 1;
	}

	void Start(){
		StartCoroutine (generateAI ());
		StartCoroutine (generateMadGirl ());
	}

	int checkAICount(){
		int cnt = 0;
		for (int i = 0; i < AIlen; i++)
			if (AI [i].activeSelf)
				cnt++;

		return cnt;
	}

	IEnumerator generateAI(){
		while (true) {
			int randomNumber = Random.Range (0, AIlen);
			if (checkAICount () < maximumAI && !AI [randomNumber].activeSelf) {
				AI [randomNumber].transform.position = spawnPosition [Random.Range (0, spawnPosition.Length-1)].transform.position;
				AI [randomNumber].SetActive (true);
			}
			yield return new WaitForSeconds (delay);
		}
	}

	IEnumerator generateMadGirl(){
		while (true) {
			yield return new WaitForSeconds (delay*delay);
			bool madB = false;
			for (int i = 0; i < MadGirl.Length; i++) {
				if (MadGirl [i].activeSelf) {
					madB = true;
					break;
				}
			}

			if (!madB) {
				int randomNumber = Random.Range (0, MadGirl.Length);
				MadGirl [randomNumber].transform.position = spawnPosition [Random.Range (0, spawnPosition.Length - 1)].transform.position;
				MadGirl [randomNumber].SetActive (true);
			}
		}
	}
}
