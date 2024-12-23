using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 30.0f;
	Enemy enem;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += this.transform.forward * speed * Time.deltaTime;
		if (this.transform.position.z > 60 || this.transform.position.z < -60) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			enem = other.GetComponent<Enemy> ();
			enem.EnemyHit (enem.gameObject);
			GameObject.Destroy (this.gameObject);
		}
	}

}
