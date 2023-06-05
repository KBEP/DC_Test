using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]

public sealed class EnemyMovementBehaviour : MonoBehaviour
{
	float rotSpeed;
	float speed;
	float sqrKeepDistance;
	CharacterController cc;
	Transform target;

	void Awake ()
	{
		cc = transform.GetComponentEx<CharacterController>();
	}

	public void Setup (float speed, float rotSpeed)
	{
		this.speed    = speed    >= 0.0f ? speed    : 0.0f;
		this.rotSpeed = rotSpeed >= 0.0f ? rotSpeed : 0.0f;
	}

	public void Follow (Transform target, float keepDistance)
	{
		sqrKeepDistance = keepDistance > 0.0f ? keepDistance * keepDistance : 0.0f;
		enabled = this.target = target;
	}

	void Update ()
	{
		if (target)//process following
		{
			//looking at target
			Vector3 direction = new Vector3(target.position.x - transform.position.x, 0.0f, target.position.z - transform.position.z);
			if (direction != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

			//moving to target
			if (cc.isGrounded && Vector3.SqrMagnitude(target.transform.position - transform.position) > sqrKeepDistance)
			{
				Vector3 dest = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
				Vector3 finalMotion = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime) - transform.position;
				cc.Move(finalMotion);
			}
		}
		else enabled = false;
	}
}
