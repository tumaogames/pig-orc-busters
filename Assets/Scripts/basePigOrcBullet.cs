using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basePigOrcBullet : MonoBehaviour {

	public float speed = 20.0f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position += this.transform.forward * speed * Time.deltaTime;
		if (this.transform.position.z > 60 || this.transform.position.z < 12) 
		{
			GameObject.Destroy (this.gameObject);
		}
	}
}
