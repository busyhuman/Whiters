using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class csMonster : MonoBehaviour {
	public enum MonsterState{idle,walk,attack,run,spawn};
	public MonsterState monsterstate = MonsterState.idle;
	public float walkDist = 2000.0f;
	public float runDist = 20.0f;
	public float runSpeed=18.0f;
	public float attackDist = 2.0f;

	public AudioClip[] clip;
	private float walkSpeed = 10.0f;
	private AudioSource audiosource;
	private NavMeshAgent nvAgent;
	private Transform playerTr;
	private Transform monsterTr;
	private Animator animator;

	void Awake(){
		nvAgent = this.gameObject.GetComponent<NavMeshAgent> ();
		playerTr = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		monsterTr = this.gameObject.GetComponent<Transform> ();
		animator = this.gameObject.GetComponent<Animator> ();
		audiosource = GetComponent<AudioSource> ();
		walkSpeed = this.gameObject.GetComponent<NavMeshAgent> ().speed;
	}

	void OnEnable(){
		StartCoroutine ("CheckMonsterState");
		StartCoroutine ("CheckMonsterAction");
	}

	IEnumerator CheckMonsterState(){
		monsterstate = MonsterState.spawn;
		yield return new WaitForSeconds (2.6f);
		while (true) {
			yield return new WaitForSeconds (0.2f);
			float dist = Vector3.Distance (playerTr.position, monsterTr.position);

			if (dist <= attackDist) {
				audiosource.PlayOneShot (clip[2], 0.6f);
				monsterstate = MonsterState.attack;
				yield return new WaitForSeconds (3.0f);
				animator.SetBool ("isAttack", false);
			} else if (dist <= runDist) {
				monsterstate = MonsterState.run;
			} else if (dist <= walkDist) {
				monsterstate = MonsterState.walk;
			} else
				monsterstate = MonsterState.idle;
		}
	}

	IEnumerator CheckMonsterAction(){
		while (true) {
			if (monsterstate == MonsterState.idle) {
				nvAgent.Stop ();
				animator.SetBool ("isWalk", false);
				animator.SetBool ("isSpawn", false);
			} else if (monsterstate == MonsterState.walk) {
				nvAgent.Resume ();
				nvAgent.speed = walkSpeed;
				nvAgent.destination = playerTr.position;
				animator.SetBool ("isWalk", true);
				animator.SetBool ("isRun", false);
			} else if (monsterstate == MonsterState.run) {
				nvAgent.Resume ();
				nvAgent.speed = runSpeed;
				nvAgent.destination = playerTr.position;
				animator.SetBool ("isRun", true);
				animator.SetBool ("isAttack", false);
			} else if (monsterstate == MonsterState.attack) {
				nvAgent.Stop ();
				animator.SetBool ("isAttack", true);
			} else if (monsterstate == MonsterState.spawn) {
				nvAgent.Stop ();
				animator.SetTrigger ("isSpawn");
			}
			yield return null;
		}
	}
		
}
