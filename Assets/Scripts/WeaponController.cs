using System;
using UnityEngine;

public sealed class WeaponController : MonoBehaviour
{
	ISoundPlayerAccessor soundPlayer;
	Transform muzzle;
	Weapon weapon;
	float lastShotTime;

	public void Setup (ISoundPlayerAccessor soundPlayer)
	{
		this.soundPlayer = soundPlayer ?? throw new ArgumentNullException(nameof(soundPlayer));
	}

	public void SetWeapon (Weapon weapon)
	{
		this.weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));

		//здесь должна быть подмена модели оружия, в модели должна быть кость muzzle, а пока сделаем так
		muzzle = transform;
	}

	public void TryShot ()
	{
		if (CanShoot()) DoShot();
	}

	bool CanShoot ()
	{
		return weapon != null && muzzle && lastShotTime + weapon.FireDelay < Time.time;
	}

	void DoShot ()
	{
		if (weapon.TryShot(muzzle))
		{
			soundPlayer.PlayOneShot(weapon.ShotSound);
			lastShotTime = Time.time;
		}
	}
}
