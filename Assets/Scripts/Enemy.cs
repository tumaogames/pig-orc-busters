using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public float movementSpeed = 2f;
	private Animator anim;
	private Animation animation;
	private string PARAMETER_STATE = "State";
	private string PARAMETER_BOOL = "HitActive";
	private float RandomFireCoolDown = 0f;
	private float DoubleFlipCoolDown = 0f;
	public float DieCoolDown = 0f;
	public float DieCoolDownTimer = 2f;
	public GameObject EnemyBulletPrefab;
	public Transform EnemyGun;
	private bool InsideCastle = false;
	private NavMeshAgent nav;
	public bool Died = false;
	public float EnemyHealth = 100;
	public float EnemyHealthDefense;
	public bool archer = false;

	// Use this for initialization


	void Awake () {
		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		animation = GetComponent<Animation> ();
	}

	void Start () {
		RandomFireCoolDown = Random.Range (2f, 5f);
		DoubleFlipCoolDown = Random.Range (4f, 10f);
		DieCoolDown = DieCoolDownTimer;
	}
	
	// Update is called once per frame
	void Update () {
		RandomFireCoolDown -= Time.deltaTime;
		DoubleFlipCoolDown -= Time.deltaTime;
		if (!Died) {
			if (RandomFireCoolDown <= 0 && !InsideCastle && !GameManager.instance.GameOver && !anim.GetCurrentAnimatorStateInfo (0).IsName ("DoubleBackFlip")) {
				RandomFire ();
				RandomFireCoolDown = Random.Range (2f, 5f);
			} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Punch")) {
			} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("DoubleBackFlip")) {
			} else if (DoubleFlipCoolDown <= 0 && !InsideCastle && !GameManager.instance.GameOver) {
				DoubleBackFlip ();
				DoubleFlipCoolDown = Random.Range (2f, 10f);
			} else {
				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Die")) {
					anim.SetInteger (PARAMETER_STATE, 5);
					Destroy (GetComponent<SphereCollider> ());
					Destroy (GetComponentInChildren<BoxCollider> ());
					Destroy (GetComponent<NavMeshAgent> ());
					Died = true;
				} else {
					if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Fire")) {
						anim.SetInteger (PARAMETER_STATE, 0);
					} else {
						anim.SetInteger (PARAMETER_STATE, 1);	
					}
				}
			}
			if (InsideCastle && !GameManager.instance.GameOver && !Died) {
				nav.SetDestination (GameManager.instance.Player.transform.position);
			} else if (!GameManager.instance.GameOver) {
				transform.position += transform.forward * Time.deltaTime * movementSpeed;
			} else {
				Destroy (nav);
				anim.SetInteger (PARAMETER_STATE, 2);
			}
			if (this.transform.position.z < 12) {
				GameManager.instance.UnregisterEnemy (this.gameObject);
				GameObject.Destroy (this.gameObject);
			}
		} else {
			DieCoolDown -= Time.deltaTime;
			if(DieCoolDown <= 0f){
				transform.position -= transform.up * Time.deltaTime * movementSpeed;
				if (this.transform.position.y < -3) {
					GameObject.Destroy (this.gameObject);
				}
			}
		}
	}

	void RandomFire(){
		anim.SetInteger (PARAMETER_STATE, 1);
		if (!archer) {
			GameObject bulletObject = GameObject.Instantiate<GameObject> (EnemyBulletPrefab);
			//AudioManager.Instance.PlaySound(1, 0.5f);
			bulletObject.transform.position = EnemyGun.transform.position;
			bulletObject.transform.rotation = EnemyGun.transform.rotation;
		}
	}

	void DoubleBackFlip(){
		anim.SetInteger (PARAMETER_STATE, 4);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "InsideWall") {
			InsideCastle = true;
		}
	}

	public void EnemyHit(GameObject other){
		EnemyHealth -= EnemyHealthDefense;
		//AudioManager.Instance.PlaySound(2, 0.4f);
		if (EnemyHealth <= 0) {
			anim.Play ("Die");
            AudioManager.Instance.PlaySound(2, 0.4f);
            GameManager.instance.UnregisterEnemy (other);
		} else {
			anim.Play ("Hit");
		}
	}

	public void ArcherBeginAttack(){
		GameObject bulletObject = GameObject.Instantiate<GameObject> (EnemyBulletPrefab);
        AudioManager.Instance.PlaySound(1, 0.5f);
        bulletObject.transform.position = EnemyGun.transform.position;
		bulletObject.transform.rotation = EnemyGun.transform.rotation;
	}

	public void ArmyBeginAttack(){
		GameObject bulletObject = GameObject.Instantiate<GameObject> (EnemyBulletPrefab);
        AudioManager.Instance.PlaySound(1, 0.5f);
        bulletObject.transform.position = EnemyGun.transform.position;
		bulletObject.transform.rotation = EnemyGun.transform.rotation;
	}
}
