using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
#pragma warning disable CS0108
	Camera camera;
#pragma warning restore CS0108
	float spawnDelay;
	float timeToSpawn;
	Player target;
	List<EnemyData> enemyData = new List<EnemyData>();

	public void Setup (Camera camera, Player target, ISet<EnemyData> enemyData, float spawnDelay)
	{
		this.camera = camera;
		this.target = target;
		this.enemyData.Clear();
		foreach (var data in enemyData) if (data != null) this.enemyData.Add(data);
		this.spawnDelay = spawnDelay >= 0.0f ? spawnDelay : 0.0f;
		this.timeToSpawn = Time.time + this.spawnDelay;
	}

	void Update ()
	{
		if (camera && enemyData != null && enemyData.Count > 0)
		{
			if (Time.time >= timeToSpawn)
			{
				if (CameraHelper.TryGenGroundPointOutside(camera, out Vector3 groundSpawnPoint))
				{
					Spawn(groundSpawnPoint);
				}
				else
				{
					if (Debug.isDebugBuild) Debug.Log("NO SPAWN POINT");
				}
				timeToSpawn = Time.time + spawnDelay;
			}
		}
		else enabled = false;
	}

	void Spawn (Vector3 groundSpawnPoint)
	{
		//поднимаем точку спауна над землёй на 2 (захардкожено, чтобы не городить код для выяснения какой высоты враг)
		Vector3 spawnPoint = groundSpawnPoint + new Vector3(0.0f, 2.0f, 0.0f);
		//выбираем случайного врага
		int idx = Random.Range(0, enemyData.Count);
		EnemyData data = enemyData[idx];
		//спаун, настройка и команда атаковать цель
		Enemy enemy = Instancer.InstantiateEx("Prefabs/Enemies/" + data.type, data.type, spawnPoint, null).GetComponentEx<Enemy>();
		enemy.Setup(data);
		enemy.Attack(target, data.attackDistance);
	}
}
