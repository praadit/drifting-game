using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController_AutoAcc : MonoBehaviour {
	[SerializeField] float turnSpeed = 5f;
	[SerializeField] Camera mainCam;
	[SerializeField] float acceleration = 8f;
	[SerializeField] float maxSpeed = 120f;
	
	Quaternion targetRotation;
	Rigidbody _rb;
	Vector3 lastPos;
	float _sideSlipAmount;
	float accInput = 0f;
	float _speed;
	float simSpeed;

	float limit;

	public float SideSlipAmount
	{
		get{
			return _sideSlipAmount;
		}
	}

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		limit = maxSpeed / (acceleration * 1.5f);
	}

	private void SetRotationPoint(){
		Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		float distance;

		if(plane.Raycast(ray, out distance)){
			Vector3 target =  ray.GetPoint(distance);
			Vector3 direction = target - transform.position;
			float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			targetRotation = Quaternion.Euler(0, rotationAngle, 0);
		}
	}

	private void FixedUpdate() {
		_speed = _rb.velocity.magnitude / 1000;
		simSpeed = acceleration * _speed * 1.5f;

        //Accelerate with Mouse
        if (_speed < limit)
            accInput = acceleration * Time.fixedDeltaTime;//acceleration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;
        else
        {
            accInput = 0;
        }
		//Accelerate with Keyboard
		// accInput =  acceleration * (Input.GetKey(KeyCode.W)? 1 : Input.GetKey(KeyCode.S)? -1 : 0) * Time.fixedDeltaTime;

		_rb.AddRelativeForce(Vector3.forward * accInput);
				
		SetRotationPoint();
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Mathf.Clamp(_speed, -1, 1) * Time.fixedDeltaTime);

		SetSideSlip();
	}

	private void SetSideSlip(){
		Vector3 dir = transform.position - lastPos;
		Vector3 movement = transform.InverseTransformDirection(dir);
		lastPos = transform.position;
		
		_sideSlipAmount = movement.x;
	}
}
