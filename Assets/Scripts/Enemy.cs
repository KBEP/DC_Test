using System;
using UnityEngine;

[RequireComponent (typeof(Health))]
[RequireComponent (typeof(EnemyAttackBehaviour))]
[RequireComponent (typeof(EnemyMovementBehaviour))]

public sealed class Enemy : MonoBehaviour
{
	Health health;
	EnemyAttackBehaviour attacking;
	EnemyMovementBehaviour movement;

	void Awake ()
	{
		health = transform.GetComponent<Health>();
		attacking = transform.GetComponent<EnemyAttackBehaviour>();
		movement = transform.GetComponentEx<EnemyMovementBehaviour>();
	}

	public void Setup (EnemyData enemyData)
	{
		if (enemyData == null) throw new ArgumentNullException(nameof(enemyData));

		health.Setup(enemyData.hp, enemyData.hp);
		health.Dead += OnDead;
		attacking.Setup(enemyData.damage, enemyData.attackDelay, enemyData.attackDistance);
		movement.Setup(enemyData.speed, enemyData.rotSpeed);
	}

	public void Attack (Player player, float keepDistance)
	{
		movement.Follow(player?.transform, keepDistance);
		attacking.Attack(player);
	}

	void OnDead ()
	{
		Destroy(gameObject);
	}
}
