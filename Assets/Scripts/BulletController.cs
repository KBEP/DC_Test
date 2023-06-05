using System;
using UnityEngine;

public sealed class BulletController : MonoBehaviour
{
	Bullet bullet;
	float timeToDestroy;

	public void Setup (Bullet bullet)
	{
		this.bullet = bullet ?? throw new ArgumentNullException(nameof(bullet));
		timeToDestroy = Time.time + this.bullet.lifeTime;
	}

	void Update ()
	{
		if (bullet == null) return;

		if (Time.time >= timeToDestroy) Destroy(gameObject);
		else//летим вперёд
		{
			Vector3 prevPos = transform.position;
			transform.Translate(Vector3.forward * Time.deltaTime * bullet.speed);
			//проверка на столкновение
			RaycastHit hit;
			if (Physics.Linecast(prevPos, transform.position, out hit, bullet.collisionMask, QueryTriggerInteraction.Collide))
			{
				//наносим урон, если есть чему
				Health health = hit.transform.GetComponent<Health>();
				if (health != null) health.Value += bullet.damage;
				Destroy(gameObject);
			}
		}
	}
}
