using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	private MovementMotor motor;

	public float move_Magnitude = 0.05f;
	public float speed = 0.7f;
	public float speed_Move_WhileAttack = 0.1f;
	public float speedAttack = 1.5f;
	public float turnSpeed = 10f;
	public float speed_Jump = 20f;
	public GameObject bulletPrefab;
	public GameObject gun;
	public float cooldownTimer = 0f;
	public float resetCoolDown = 0.4f;
	public Vector3 mousePos;
	public SimpleTouchController mcontroller;
    public SimpleTouchController1 mcontroller1;
    public bool fire = false;

    private float speed_Move_Multiplier = 1f;

	private Vector3 direction;

	private Animator anim;
	public Camera mainCamera;

	private string PARAMETER_STATE = "State";
	private string PARAMETER_ATTACK = "Attack";

	void Awake () {
		motor = GetComponent<MovementMotor> ();
		anim = GetComponent<Animator> ();

	}


	void Start () {
		anim.applyRootMotion = false; 

		//mainCamera = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		MovementAndJumping ();
		if(fire)
		AttackFire ();
	}

	private Vector3 MoveDirection {
		get	{ return direction; }

		set { 
			direction = value * speed_Move_Multiplier;
			/*
			if (direction.magnitude > 0.1f) {
				var newRotation = Quaternion.LookRotation (direction);
				transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * turnSpeed);
			}*/

			//direction *= speed * (Vector3.Dot (transform.forward, direction) + 1f) * 5f;
			motor.Move (direction);

			AnimationMove (motor.charController.velocity.magnitude * 0.1f);
		}
	}

	void Moving(Vector3 dir, float mult){
		speed_Move_Multiplier = 1 * mult;
		MoveDirection = dir;
	}

	void AnimationMove(float magnitude){
		if (magnitude > move_Magnitude && !GameManager.instance.GameOver && anim.GetInteger(PARAMETER_STATE) != 3)
		{
			float speed_Animation = magnitude * 3f;
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack1") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
			{
				anim.SetInteger(PARAMETER_STATE, 0);
			}

			if (speed_Animation < 1f)
				speed_Animation = 1f;
			if (anim.GetInteger(PARAMETER_STATE) != 3 && motor.isGrounded)
			{
				anim.SetInteger(PARAMETER_STATE, 1);
				anim.SetInteger(PARAMETER_ATTACK, 0);
				anim.speed = speed_Animation;
			}
		}
		else if (anim.GetInteger(PARAMETER_STATE) != 3 && motor.isGrounded && !GameManager.instance.GameOver)
		{
			anim.SetInteger(PARAMETER_STATE, 0);
			anim.SetInteger(PARAMETER_ATTACK, 0);
			anim.speed = 1f;
		}
        else if (anim.GetInteger(PARAMETER_STATE) == 3 && motor.isGrounded && !GameManager.instance.GameOver)
        {
            anim.SetInteger(PARAMETER_STATE, 0);
        }
    }


	public void Jump(){
		motor.Jump (speed_Jump);
		if (motor.isGrounded) {
			anim.SetInteger (PARAMETER_STATE, 3);
			anim.SetInteger (PARAMETER_ATTACK, 0);
			anim.speed = 1f;
		}
        AudioManager.Instance.PlaySound(5, 0.5f);
    }

	public void AttackFire(){
		cooldownTimer -= Time.deltaTime;
		if (cooldownTimer <= 0) {
			//if (motor.isGrounded) {
				anim.SetInteger (PARAMETER_STATE, 4);
				anim.SetInteger (PARAMETER_ATTACK, 1);
				anim.speed = 1f;
				cooldownTimer = resetCoolDown;
				GameObject bulletObject = GameObject.Instantiate<GameObject> (bulletPrefab);
				AudioManager.Instance.PlaySound(0, 0.5f);
				bulletObject.transform.position = gun.transform.position;
				bulletObject.transform.rotation = gun.transform.rotation;
			//}
		}
	}

	void MovementAndJumping(){
		RaycastHit hit;
		Vector3 moveInput = Vector3.zero;
		Vector3 forward = Quaternion.AngleAxis (-90, Vector3.up) * mainCamera.transform.right;

        moveInput += mainCamera.transform.forward * mcontroller.movementVector.y;
        moveInput += mainCamera.transform.right * mcontroller.movementVector.x;

        Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100) && !GameManager.instance.GameOver)
        {
            // Calculate the direction to look at
            Vector3 lookDirection = new Vector3(mcontroller1.movementVector1.x, 0, mcontroller1.movementVector1.y);

            // Ensure the direction is normalized to avoid unintended behavior
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }

        //moveInput.Normalize ();
        Moving (moveInput, 8f);
		if (Input.GetKey (KeyCode.Space)) {
			Jump ();
		}


	}
}
