using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	[SerializeField] int startingHealth = 200;
	[SerializeField] float timeSinceLastHit = 2f;
	private float timer = 0f;
	private int currentHealth;
	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
	}

	void OnTriggerEnter (Collider other){
		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver) {
			if (other.tag == "EnemyWeapon") {
				takeMeleeHit ();
				timer = 0;
				Debug.Log("Punch hit");
			}
		}
	}

	void takeMeleeHit (){
		if (currentHealth > 0) {
			currentHealth -= 40;
		}

		if (currentHealth <= 0) {
			GameObject.Destroy (this.gameObject);
		}
	}
}
