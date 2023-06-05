using UnityEngine;

[RequireComponent (typeof(CharacterController))]

public sealed class GravityApplyer : MonoBehaviour
{
	Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);
	CharacterController cc;

	void Awake ()
	{
		cc = transform.GetComponentEx<CharacterController>();
	}

	void Update ()
	{
		cc.Move(gravity * Time.deltaTime);
	}
}
