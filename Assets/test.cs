using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {


    public GameObject prefab;
	public GameObject wall;
    Collider collider;
    bool activated = false;
    Vector3 endPoint;
    float duration = 1.0f;
    Vector3 startPoint;
    float startTime;
    bool objectHolding = false;
	Ray ray;
	public bool Lerping = false;
	float alpha;



	// Use this for initialization
	void Start () {
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
		if (Lerping) {
			AnimateLerping ();
		}
        
    }



	void AnimateLerping()
	{
			RaycastHit hit;
			float movetarget;
			//startPoint = hit.point;
			ray = GetComponent<Camera> ().ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
			if (Physics.Raycast(ray, out hit))
			{
				movetarget = hit.distance;
				if (activated == false)
				{
					wall = Instantiate(prefab, hit.point, prefab.transform.rotation);
					activated = true;
				}
				startPoint = hit.point;
			}
			wall.transform.position = hit.point;
			Animation animationObj = prefab.gameObject.GetComponent<Animation>();
			animationObj.Play("wall_rotation");
			alpha = prefab.GetComponent<Renderer> ().sharedMaterial.color.a;
			alpha = 0.5f;
			
	}
}

