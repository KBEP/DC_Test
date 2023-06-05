using UnityEngine;

public sealed class InputController : MonoBehaviour
{
#pragma warning disable CS0108
	Camera camera;//can be null
#pragma warning restore CS0108
	Player player;//can be null

	public void Control (Camera camera, Player player)
	{
		this.camera = camera;
		this.player = player;
	}

	void Update ()
	{
		if (player == null) return;

		//looking at mouse pointer
		if (camera != null)
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				Vector2 targetPoint =  new Vector2(hit.point.x, hit.point.z);
				player.LookAt(targetPoint);
			}
		}

		//moving
		Vector2 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		player.TryMove(moveDirection);

		//firing
		if (Input.GetAxis("Fire1") != 0.0f)
		{
			player.TryShot();
		}

		//weapon switching
		if (Input.GetKeyDown(KeyCode.Q))
		{
			player.SwitchWeapon();
		}
	}
}
