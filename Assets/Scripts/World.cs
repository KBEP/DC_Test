using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(EnemySpawner))]

public sealed class World : MonoBehaviour
{
	Transform playerSpawn;
	EnemySpawner enemySpawner;

	public CameraController CameraCtr { get; private set; }
	public Player Player { get; private set; }

	void Awake ()
	{
		playerSpawn = transform.FindEx("PlayerSpawn");
		enemySpawner = transform.GetComponentEx<EnemySpawner>();
		CameraCtr = transform.GetChildComponentEx<CameraController>("Camera");
	}

	public void CmdSpawnPlayer ()
	{
		if (Player) return;//уже создан

		Player = Instancer.InstantiateEx("Prefabs/Player", "Player", playerSpawn.position, Quaternion.identity, transform).GetComponentEx<Player>();
	}

	public void CmdCameraFollowPlayer (float followSpeed)
	{
		if (CameraCtr) CameraCtr.SmoothFollow(Player?.transform, followSpeed);
	}

	public void CmdStartSpawnEnemies (float spawnDelay, ISet<EnemyData> enemyData)
	{
		enemySpawner.Setup(CameraCtr.Camera, Player, enemyData, spawnDelay);
		enemySpawner.enabled = true;
	}
}
