using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour {

	[SerializeField] int startingHealth = 100;
	[SerializeField] float timeSinceLastHit = 2f;
	[SerializeField] Slider healthSlider;

	private float timer = 0f;
	private CharacterController characterController;
	private CharacterMovement characterMovement;
	private Animator anim;
	private int currentHealth;

	void Awake(){
		Assert.IsNotNull (healthSlider);
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		characterController = GetComponent<CharacterController> ();
		characterMovement = GetComponent<CharacterMovement> ();
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
	}

	void OnTriggerEnter (Collider other){
		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver) {
			if (other.tag == "EnemyBullet") {
				takeHit ();
				timer = 0;
				Destroy (other.gameObject);
			}
			if (other.tag == "EnemyWeapon") {
				takeMeleeHit ();
				timer = 0;
			}
		}
	}

	void takeHit(){
		if (currentHealth > 0) {
			GameManager.instance.PlayerHit (currentHealth);
			//anim.Play("die");
			currentHealth -= 30;
			healthSlider.value = currentHealth;
		}

		if (currentHealth <= 0) {
			killPlayer ();
		}
	}

	void takeMeleeHit (){
		if (currentHealth > 0) {
			GameManager.instance.PlayerHit (currentHealth);
			//anim.Play("die");
			currentHealth -= 40;
			healthSlider.value = currentHealth;
		}

		if (currentHealth <= 0) {
			killPlayer ();
		}
	}

	void killPlayer(){
		GameManager.instance.PlayerHit (currentHealth);
		anim.SetTrigger ("Dead");
		anim.SetInteger ("State", 6);
		characterController.enabled = false;
		characterMovement.enabled = false;
	}
}
