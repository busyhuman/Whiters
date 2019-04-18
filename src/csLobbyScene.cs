using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class csLobbyScene : MonoBehaviour {

	void loadvsCom(){
		SceneManager.LoadScene ("vsCom");
	}

	void loadvsZombies(){
		SceneManager.LoadScene ("vsZombies");
	}

	void loadQuit(){
		Application.Quit ();
	}
}
