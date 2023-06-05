using UnityEngine;
using System;

//должен висеть на том же объекте, что и коллайдер, принимающий урон от пуль
public sealed class Health : MonoBehaviour
{
	int health;
	int maxHealth;

	public int Value
	{
		get => health;
		set
		{
			int previousValue = health;
			health = Mathf.Clamp(value, 0, maxHealth);
			if (IsDead && Dead != null) Dead();
			else if (health < previousValue && Damaged != null) Damaged();
			//--else if (health > previousValue && Healed != null) Healed();
		}
	}

	public bool IsDead => health <= 0;
	public bool IsAlive => health > 0;

	public event Action Dead;
	public event Action Damaged;
	//--public event Action Healed;

	public void Setup (int maxHealth, int health)
	{
		this.maxHealth = maxHealth >= 0 ? maxHealth : 0;
		this.health = Mathf.Clamp(health, 0, this.maxHealth);
	}
}
