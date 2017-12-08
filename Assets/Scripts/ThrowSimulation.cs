﻿using UnityEngine;
using System.Collections;

public class ThrowSimulation : MonoBehaviour
{
	//public BallManager bm;
	public Transform Target;
	public float firingAngle = 45.0f;
	public float gravity = 9.8f, duration, torque;

	public Transform Projectile;      
	private Transform myTransform;

	void Awake()
	{
	//	bm = GameObject.Find("BallManager").GetComponent<BallManager>();
		Target = GameObject.FindGameObjectWithTag("BounceTarget").transform;
		myTransform = transform;      
	}

	void Start()
	{         
		

		//Target = bm.target;

		this.GetComponent<Rigidbody>().useGravity = false;

		ThrowBall(Target);
		Invoke("DestroyBall", 7f);
	}

	void Update(){
		//Move();
		//transform.Rotate(new Vector3(15,15,0));
		//this.GetComponent<Rigidbody>().AddTorque(new Vector3(1, 1, 0) * torque);
	}


	public void ThrowBall(Transform target){
		
		this.GetComponent<Rigidbody>().useGravity = true;
		Physics.gravity = new Vector3(0, -gravity, 0);
		this.GetComponent<Rigidbody>().velocity = CalculateThrowSpeed(this.transform.position, target.position, duration);
	}

	Vector3 CalculateThrowSpeed(Vector3 start, Vector3 target, float duration){

		Vector3 totarget = target - start;
		Vector3 toTargetXZ = totarget;
		toTargetXZ.y = 0;

		float y = totarget.y;
		float xz = toTargetXZ.magnitude;

		float t = duration;

		//s = u*t + (1/2)*a*t*t* : for y plane : a = -g.
		//for xz plane : a = 0; 

		float vy = (y/t) + (0.5f)*(Physics.gravity.magnitude)*t;			
		float vxz = xz/t;

		Vector3 result = toTargetXZ.normalized;
		result *= vxz;
		result.y  = vy;

		return result;
	}

	void DestroyBall(){
		Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision collision){
		//print("collided with : " + collision.gameObject.name); 
	}
}
