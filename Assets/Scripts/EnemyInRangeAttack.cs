using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInRangeAttack : MonoBehaviour {

	[SerializeField] private float range = 3f;
	[SerializeField] private float timeBetweenAttacks = 2f;
	public bool wallInRange = false;
	private Animator anim;
	private GameObject player;
	private bool playerInRange;
	public Enemy enemy;
	private BoxCollider[] weaponColliders;


	// Use this for initialization
	void Start () {
		weaponColliders = GetComponentsInChildren<BoxCollider> ();
		player = GameManager.instance.Player;
		anim = GetComponent<Animator> ();
		StartCoroutine (attack ());
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, player.transform.position) < range) {
			playerInRange = true;
		} else {
			playerInRange = false;
		}
	}
		

	IEnumerator attack(){
		if (playerInRange && !GameManager.instance.GameOver && !enemy.Died || wallInRange && !enemy.Died && !GameManager.instance.GameOver) {
			anim.SetInteger ("State", 3);
			//anim.Play ("Punch");
			yield return new WaitForSeconds (timeBetweenAttacks);
		}
		yield return null;
		StartCoroutine (attack ());
	}

	public void EnemyBeginAttack(){
		foreach (var weapon in weaponColliders) {
			weapon.enabled = true;
		}
	}

	public void EnemyEndAttack(){
		foreach (var weapon in weaponColliders) {
			weapon.enabled = false;
		}
	}
}
