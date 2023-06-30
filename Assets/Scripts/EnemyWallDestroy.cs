using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallDestroy : MonoBehaviour {
	GameObject enem;
	private Animator anim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			GameManager.instance.UnregisterEnemy (other.gameObject);
			anim = other.gameObject.GetComponent<Animator> ();
			anim.Play ("Die");
		}
	}
}
