using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SceneController : MonoBehaviour
{
	public BattlefieldSceneRoot Battlefield { get; private set; }//может быть null

	public AsyncOperation LoadWorld ()
	{
		Battlefield = null;
		AsyncOperation operation = LoadSceneAsync("Scenes/Battlefield");
		StartCoroutine(CrtAwaitLoading(operation, true));
		return operation;
	}

	public AsyncOperation UnloadWorld ()
	{
		Battlefield = null;
		AsyncOperation operation = LoadSceneAsync("Scenes/Stub");
		StartCoroutine(CrtAwaitLoading(operation, false));
		return operation;
	}

	IEnumerator CrtAwaitLoading (AsyncOperation operation, bool findBattlefield)//ожидает, что operation не null
	{
		while (!operation.isDone) yield return null;
		if (findBattlefield)
		{
			Battlefield = GameObject.FindObjectOfType<BattlefieldSceneRoot>();
			//загруженая сцена не имеет обязательного корневого элемента BattlefieldSceneRoot
			if (Battlefield == null)
			throw new Exception($"The loading scene root must have component '{nameof(BattlefieldSceneRoot)}'.");
		}
	}

	AsyncOperation LoadSceneAsync (string scenePath)
	{
		try
		{
			return SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Single);
		}
		catch
		{
			return null;
		}
	}
}
