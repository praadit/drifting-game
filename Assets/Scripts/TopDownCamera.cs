using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour {

	[SerializeField] Transform target;
	[SerializeField] float aheadSpeed;
	[SerializeField] float followDamping;
	[SerializeField] float cameraHeight;

	Rigidbody _obsRb;

	void Start () {
		_obsRb = target.GetComponent<Rigidbody>();
	}
	
	void Update () {
		if(target == null)
			return;
		
		Vector3 targetPos = target.position + Vector3.up * cameraHeight + _obsRb.velocity * aheadSpeed;
		transform.position = Vector3.Lerp(transform.position, targetPos, followDamping * Time.deltaTime);
	}
}
