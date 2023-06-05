using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class UILoading : UIShield
{
	Text progress;
	float cashedProgress;
	Action<ValueType> internal_callback;
	ValueType internal_data;

	public override bool SaveInHistory => false;

	protected override void Awake ()
	{
		base.Awake();
		progress = transform.GetChildComponentEx<Text>("Progress");
	}

	public void Open (AsyncOperation operation, Action<ValueType> callback, ValueType data)
	{
		internal_callback = callback;
		internal_data = data;
		base.Open();
		StartCoroutine(CrtWaitLoadingComplite(operation));
	}

	void Internal_Callback ()
	{
		var tmp_callback = internal_callback;
		var tmp_data = internal_data;
		//clear data
		internal_callback = null;
		internal_data = default;
		//call
		tmp_callback?.Invoke(tmp_data);
	}

	IEnumerator CrtWaitLoadingComplite (AsyncOperation operation)
	{
		if (operation == null) yield break;
		while (!operation.isDone)
		{
			if (operation.progress != cashedProgress)
			{
				cashedProgress = operation.progress;
				progress.text = cashedProgress.ToString("p0");
			}
			yield return null;
		}
		progress.text = "100%";
		Internal_Callback();
	}
}
