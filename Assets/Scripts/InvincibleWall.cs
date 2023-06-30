using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleWall : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider other){
			if (other.tag == "Enemy") {
				other.GetComponent<EnemyInRangeAttack> ().wallInRange = true;
			}
	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Enemy") {
			other.GetComponent<EnemyInRangeAttack> ().wallInRange = false;
		}
	}
}
