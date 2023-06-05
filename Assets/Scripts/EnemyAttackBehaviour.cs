using UnityEngine;

public sealed class EnemyAttackBehaviour : MonoBehaviour
{
	Health targetHealth;
	int damage;
	float attackDelay;
	float attackDistance;
	float sqrAttackDistance;
	float timeToAttack;

	public void Setup (int damage, float attackDelay, float attackDistance)
	{
		this.damage = damage;
		this.attackDelay = attackDelay >= 0.0f ? attackDelay : 0.0f;
		this.attackDistance = attackDistance >= 0.0f ? attackDistance : 0.0f;
		sqrAttackDistance = this.attackDistance * this.attackDistance;
		timeToAttack = float.MinValue;
	}

	public void Attack (Player target)
	{
		enabled = targetHealth = target?.transform.GetComponent<Health>();
	}

	void Update ()
	{
		if (targetHealth && targetHealth.IsAlive)
		{
			if (CanAttack(targetHealth.transform.position))//атакуем если можем
			{
				targetHealth.Value += damage;
				timeToAttack = Time.time + attackDelay;
			}
		}
		else enabled = false;
	}

	bool CanAttack (in Vector3 pos)//таймаут атаки кончился и бот достаточно близок к цели
	  => Time.time >= timeToAttack && (transform.position - pos).sqrMagnitude <= sqrAttackDistance;
}
