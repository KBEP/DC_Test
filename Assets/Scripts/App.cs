using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class App
{
	bool isStarted;//флаг, чтобы приложение могло стартовать только 1 раз

	//постоянные компоненты, существуют всё время на сцене
	UI ui;//отвечает за весь пользовательский интерфейс кроме боевого HUD'а (которого нет)
	SoundPlayer soundPlayer;//весь звук проигрывается через этот компонент
	SceneController sceneCtr;//отвечает за загрузку (замену) сцен
	InputController inputCtr;//отвечает за перехват пользовательского ввода

	public void Start ()
	{
		if (isStarted)
		{
			Debug.Log("Application already started.");
			return;
		}

		//создание и инициализация необходимых компонентов
		ui = Instancer.InstantiateEx("Prefabs/UI/UI", "UI").GetComponentEx<UI>();
		ui.MainMenu.PlayTap += OnPlayTapped;
		ui.MainMenu.QuitTap += OnQuitTapped;

		soundPlayer = Instancer.InstantiateEx("Prefabs/SoundPlayer", "SoundPlayer").GetComponentEx<SoundPlayer>();
		sceneCtr = Instancer.InstantiateEx("Prefabs/SceneController", "SceneController").GetComponentEx<SceneController>();
		inputCtr = Instancer.InstantiateEx("Prefabs/InputController", "InputController").GetComponentEx<InputController>();

		ui.OpenMainMenu();

		isStarted = true;
	}

	void OnWorldLoaded (ValueType data)
	{
		ui.GoBack();
		sceneCtr.Battlefield.World.CmdSpawnPlayer();
		//данные должны браться из сценария или сэйва, для теста сформируем прямо здесь
		int bulletCollisionMask;
		int solidIdx = LayerMask.NameToLayer("Solid");
		if (solidIdx == -1) throw new Exception("Required layer 'Solid' is not defined in the project.");
		else bulletCollisionMask = 1 << solidIdx;
		Bullet bullet0 = new SimpleBullet(-50, 10.0f, 30.0f, bulletCollisionMask);
		Bullet bullet1 = new SuperBullet(-25, 10.0f, 20.0f, bulletCollisionMask);
		Weapon[] weapons = new Weapon[2];
		weapons[0] = new SimpleWeapon(0.2f, bullet0, "Sounds/Weapons/pm_shoot");
		weapons[1] = new SuperWeapon(0.5f, bullet1, "Sounds/Weapons/tm_toz34_shot", 5, 10.0f);
		HashSet<EnemyData> enemies = new HashSet<EnemyData>();
		enemies.Add(new EnemyData("Enemy0", 100,  -5, 1.0f, 1.5f, 5.0f, 10.0f));//быстрый и подвижный
		enemies.Add(new EnemyData("Enemy1", 200, -20, 2.0f, 1.5f, 3.0f, 5.0f));//медленный и неповоротливый
		//
		//инициализация и связывание данных игрока
		sceneCtr.Battlefield.World.Player.Setup(100, weapons, 10.0f, soundPlayer);
		sceneCtr.Battlefield.World.Player.Damaged += OnPlayerDamaged;
		sceneCtr.Battlefield.World.Player.Dead += OnPlayerDead;
		//командуем камере следить за игроком
		sceneCtr.Battlefield.World.CmdCameraFollowPlayer(10.0f);
		//командуем начать спаунить врагов
		sceneCtr.Battlefield.World.CmdStartSpawnEnemies(2.0f, enemies);
		//указываем кого контроллировать
		inputCtr.Control(sceneCtr.Battlefield.World.CameraCtr.Camera, sceneCtr.Battlefield.World.Player);
	}

	void OnWorldUnloaded (ValueType data)
	{
		ui.GoBack();
		ui.OpenMainMenu();
	}

	//события UI

	void OnPlayTapped ()
	{
		ui.GoBack();
		//стартуем загрузку сцены
		AsyncOperation op = sceneCtr.LoadWorld();
		if (op != null) ui.OpenLoading(op, OnWorldLoaded, default);
	}

	void OnQuitTapped ()
	{
		Application.Quit();
	}

	//события Player

	void OnPlayerDamaged ()
	{
		if (Debug.isDebugBuild) Debug.Log("Player take damage!");
	}

	void OnPlayerDead ()
	{
		AsyncOperation op = sceneCtr.UnloadWorld();
		if (op != null) ui.OpenLoading(op, OnWorldUnloaded, default);
	}
}
