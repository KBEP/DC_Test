using System;
using UnityEngine;

public abstract class Weapon
{
	float fireDelay;
	Bullet bullet;
	string shotSound;

	public float FireDelay
	{
		get => fireDelay;
		set => fireDelay = value >= 0.0f ? value : 0.0f;
	}

	public Bullet Bullet
	{
		get => bullet;
		set => bullet = value ?? throw new ArgumentNullException(nameof(value), $"{nameof(Bullet)} cannot be null.");
	}

	public string ShotSound
	{
		get => shotSound;
		set => shotSound = value ?? "";
	}

	protected Weapon (float fireDelay, Bullet bullet, string shotSound)
	{
		FireDelay = fireDelay;
		Bullet = bullet;
		ShotSound = shotSound;
	}

	public abstract bool TryShot (Transform muzzle);
}

public sealed class SimpleWeapon : Weapon
{
	public SimpleWeapon (float fireDelay, Bullet bullet, string shotSound) : base (fireDelay, bullet, shotSound) {}

	public override bool TryShot (Transform muzzle)
	{
		if (!muzzle) return false;

		Transform t = Instancer.InstantiateEx("Prefabs/Bullets/SimpleBullet", "SimpleBullet", muzzle.position, muzzle.rotation, null);
		BulletController bc = t.GetComponentEx<BulletController>();
		bc.Setup(Bullet);

		return true;
	}
}

public sealed class SuperWeapon : Weapon
{
	float dispersionAngle;
	int fractionCount;

	public float DispersionAngle
	{
		get => dispersionAngle;
		set => dispersionAngle = value >= 0.0f ? value : 0.0f;
	}

	public int FractionCount
	{
		get => fractionCount;
		set => fractionCount = value >= 0 ? value : 0;
	}

	public SuperWeapon (float fireDelay, Bullet bullet, string shotSound, int fractionCount, float dispersionAngle)
	  : base (fireDelay, bullet, shotSound)
	{
		DispersionAngle = dispersionAngle;
		FractionCount = fractionCount;
	}

	public override bool TryShot (Transform muzzle)//стрельба веером
	{
		if (!muzzle) return false;

		int count = fractionCount;
		float angle = dispersionAngle * (count - 1) / 2.0f;

		while (--count >= 0)
		{
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up) * muzzle.rotation;
			Transform t = Instancer.InstantiateEx("Prefabs/Bullets/SuperBullet", "SuperBullet", muzzle.position, rotation, null);
			BulletController bc = t.GetComponentEx<BulletController>();
			bc.Setup(Bullet);
			angle -= dispersionAngle;
		}

		return true;
	}
}
