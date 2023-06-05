using UnityEngine;

[RequireComponent (typeof(CharacterController))]

public sealed class MovementBehaviour : MonoBehaviour
{
	CharacterController cc;
	float motionSpeed;
	Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);

	public float MotionSpeed
	{
		get => motionSpeed;
		set => motionSpeed = value >= 0.0f ? value : 0.0f;
	}

	void Awake ()
	{
		cc = transform.GetComponentEx<CharacterController>();
	}

	void Update ()
	{
		cc.Move(gravity * Time.deltaTime);
	}

	public void TryMove (Vector2 moveDirection)
	{
		Vector2 motion = moveDirection * motionSpeed * Time.deltaTime;
		Vector3 motion3d = new Vector3(motion.x, 0.0f, motion.y);
		cc.Move(motion3d);
	}
}
