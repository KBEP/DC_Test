public class EnemyData
{
	public readonly string type;
	public readonly int hp;
	public readonly int damage;
	public readonly float attackDelay;
	public readonly float attackDistance;
	public readonly float speed;
	public readonly float rotSpeed;

	public EnemyData (string type, int hp, int damage, float attackDelay, float attackDistance, float speed, float rotSpeed)
	{
		this.type = type ?? "";
		this.hp = hp >= 1 ? hp : 1;
		this.damage = damage;
		this.attackDelay = attackDelay >= 0.0f ? attackDelay : 0.0f;
		this.attackDistance = attackDistance >= 0.0f ? attackDistance : 0.0f;
		this.speed = speed >= 0.0f ? speed : 0.0f;
		this.rotSpeed = rotSpeed >= 0.0f ? rotSpeed : 0.0f;
	}
}
