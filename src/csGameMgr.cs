using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class csGameMgr : MonoBehaviour {
	public int stageCnt;
	public Text stage;
	public Text money;
	public Text time;
	public int timeCnt;
	public AudioClip clip;
	public GameObject btn1;
	public GameObject btn2;
	public GameObject txt;

	private AudioSource audio;
	private GameObject player;
	private int stageMoney;
	private bool isDie=false;
	private const int defaultSeconds = 60;

	void Awake(){
		audio = GetComponent<AudioSource> ();
		player = GameObject.Find ("Player");
	}

	void Start(){
		stageCnt = 1;
		stageMoney = 0;
		timeInit ();
		showStage ();
		showMoney ();
		StartCoroutine (this.stageTime ());
	}

	IEnumerator stageTime(){
		int tick = 1;
		while (!isDie) {
			showTime ();
			if (timeCnt == 10) {
				audio.PlayOneShot (clip, 0.7f);
			}
			yield return new WaitForSeconds (1.2f);
			if (timeCnt == 0) {
				player.SendMessage ("humanDie", SendMessageOptions.DontRequireReceiver);
				stopTicking ();
				break;
			}
			timeCnt -= tick;
		}

	}

	void stopTicking(){
		isDie = true;
		audio.Stop ();
		StopCoroutine (this.stageTime ());
	//	Debug.Log ("stop ticking");
	}

	void playerDie(){
		PlayerPrefs.SetInt ("Money", PlayerPrefs.GetInt("Money") + stageMoney);
	//	Debug.Log (PlayerPrefs.GetInt("Money").ToString());
		btn1.SetActive (true);
		btn2.SetActive (true);
		txt.SetActive (true);
	}

	public void OnClickPlayBtn(){
		Application.LoadLevel ("Play");
	}

	public void OnClickMainBtn(){
		Application.LoadLevel ("Main");
	}

	void timeInit(){
		timeCnt = defaultSeconds;
		audio.Stop ();
	}

	void changeMoney(int m){
		stageMoney += m;
	}

	void changeTimeMoney(int m){
		stageMoney += defaultSeconds - m;
	}
		
	void changeStage(int s){
		stageCnt += s;
	}
		
	void showStage(){
	//	Debug.Log ("stage : " + stageCnt);
		stage.text = stageCnt.ToString ();
	}

	void showMoney(){
	//	Debug.Log ("money : " + money);
		money.text = stageMoney.ToString ();
	}

	void showTime(){
	//	Debug.Log ("time : " + timeCnt);
		time.text = timeCnt.ToString ();
	}

}
