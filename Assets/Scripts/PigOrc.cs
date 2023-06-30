using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigOrc : MonoBehaviour {

	void Start () {
		GetComponent<Animation>().Play("Idle");
	}

	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "Player"){
			Debug.Log("hmmmp");
			GetComponent<Animation>().Play("BackFlip");
		};
	}

}
