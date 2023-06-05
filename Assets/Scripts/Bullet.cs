public abstract class Bullet
{
	public readonly int damage;//отрицательные значения - урон, положительные - лечение
	public readonly float lifeTime;
	public readonly float speed;
	public readonly int collisionMask;//маска коллайдеров, с которыми пуля взаимодействует

	public Bullet (int damage, float lifeTime, float speed, int collisionMask)
	{
		this.damage = damage;
		this.lifeTime = lifeTime >= 0.0f ? lifeTime : 0.0f;
		this.speed = speed >= 0.0f ? speed : 0.0f;
		this.collisionMask = collisionMask;
	}
}

//отличий от базы нет, но можно реализовать позже

public sealed class SimpleBullet : Bullet
{
	public SimpleBullet (int damage, float lifeTime, float speed, int collisionMask)
	  : base (damage, lifeTime, speed, collisionMask) {}
}

public sealed class SuperBullet : Bullet
{
	public SuperBullet (int damage, float lifeTime, float speed, int collisionMask)
	  : base (damage, lifeTime, speed, collisionMask) {}
}