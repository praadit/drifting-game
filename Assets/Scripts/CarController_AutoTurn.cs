using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController_AutoTurn : MonoBehaviour {
	[SerializeField] float turnSpeed = 5f;
	[SerializeField] Camera mainCam;
	[SerializeField] float acceleration = 8f;
	[SerializeField] float maxSpeed = 120f;
    [SerializeField] Transform rightPoint;
    [SerializeField] Transform leftPoint;

    Quaternion targetRotation;
	Rigidbody _rb;
	Vector3 lastPos;
	float _sideSlipAmount;
	float accInput = 0f;
	float _speed;
	float simSpeed;

	float limit;
    Vector3 target;


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

	private void FixedUpdate() {
		_speed = _rb.velocity.magnitude / 1000;
		simSpeed = acceleration * _speed * 1.5f;

        if (_speed < limit)
            accInput = acceleration * Time.fixedDeltaTime;
        else
        {
            accInput = 0;
        }

		_rb.AddRelativeForce(Vector3.forward * accInput);
        
        if (Input.GetMouseButton(0))
        {
            target = rightPoint.position;
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
        else if (Input.GetMouseButton(1))
        {
            target = leftPoint.position;
            Vector3 direction = target - transform.position;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);
        }

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
