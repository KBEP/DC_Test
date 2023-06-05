using UnityEngine;

public sealed class ShootingBehaviour : MonoBehaviour
{
	WeaponController weaponCtr;

	void Awake ()
	{
		weaponCtr = transform.GetChildComponentEx<WeaponController>("Weapon");
	}

	public void Setup (ISoundPlayerAccessor soundPlayer)
	{
		weaponCtr.Setup(soundPlayer);
	}

	public void SetWeapon (Weapon weapon)
	{
		weaponCtr.SetWeapon(weapon);
	}

	public void TryShot ()
	{
		weaponCtr.TryShot();
	}
}
