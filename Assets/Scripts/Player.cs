using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof(Health))]
[RequireComponent (typeof(ShootingBehaviour))]
[RequireComponent (typeof(MovementBehaviour))]

public sealed class Player : MonoBehaviour
{
	List<Weapon> weapons;
	int currentWeaponIdx = -1;

	Health health;
	ShootingBehaviour shooting;
	MovementBehaviour movement;

	public event Action Damaged;
	public event Action Dead;

	void Awake ()
	{
		health   = transform.GetComponentEx<Health>();
		shooting = transform.GetComponentEx<ShootingBehaviour>();
		movement = transform.GetComponentEx<MovementBehaviour>();
	}

	public void Setup (int health, IList<Weapon> weapons, float motionSpeed, ISoundPlayerAccessor soundPlayer)
	{
		this.health.Setup(health, health);
		this.health.Damaged += () => Damaged?.Invoke();
		this.health.Dead += () => Dead?.Invoke();

		if (weapons == null) throw new ArgumentNullException(nameof(weapons));
		this.weapons = weapons.Where(w => w != null).ToList();
		if (this.weapons.Count <= 0) throw new Exception("Weapon count must be greater than 0.");
		shooting.Setup(soundPlayer);
		currentWeaponIdx = 0;
		shooting.SetWeapon(this.weapons[currentWeaponIdx]);

		movement.MotionSpeed = motionSpeed >= 0.0f ? motionSpeed : 0.0f;
	}

	public void TryShot ()
	{
		shooting.TryShot();
	}

	public void TryMove (Vector2 moveDirection)
	{
		movement.TryMove(moveDirection);
	}

	public void LookAt (Vector2 targetPoint)
	{
		Vector3 targetPoint3d = new Vector3(targetPoint.x, transform.position.y, targetPoint.y);
		transform.LookAt(targetPoint3d);
	}

	public void SwitchWeapon ()
	{
		int nextWeaponIdx = currentWeaponIdx + 1;
		if (nextWeaponIdx >= weapons.Count) currentWeaponIdx = 0;
		else currentWeaponIdx = nextWeaponIdx;
		shooting.SetWeapon(weapons[currentWeaponIdx]);
	}
}
