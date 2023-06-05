using UnityEngine;

[RequireComponent (typeof(Camera))]

public sealed class CameraController : MonoBehaviour
{
	Transform target;//за кем следовать
	float followSpeed;
	Vector3 offset;//задаёт точку куда стремится прилететь камера относительно положения цели

	public Camera Camera { get; private set; }

	void Awake ()
	{
		Camera = transform.GetComponentEx<Camera>();
	}

	void Update ()
	{
		if (target)
		{
			Vector3 wishPos = target.position + offset;
			transform.position = Vector3.MoveTowards(transform.position, wishPos, followSpeed * Time.deltaTime);
		}
	}

	public void SmoothFollow (Transform target, float followSpeed)
	{
		this.target = target;
		this.followSpeed = followSpeed >= 0.0f ? followSpeed : 0.0f;
		this.offset = this.target ? transform.position - this.target.position : default;

		enabled = this.target && this.followSpeed != 0.0f;
	}
}
