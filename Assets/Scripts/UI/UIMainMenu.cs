using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIMainMenu : UIShield, IUIMainMenuEvents
{
	public event Action PlayTap;
	public event Action QuitTap;

	protected override void Awake ()
	{
		base.Awake();

		Transform buttons = transform.FindEx("Buttons");

		buttons.GetChildComponentEx<Button>("Play").onClick.AddListener(() => PlayTap?.Invoke());
		buttons.GetChildComponentEx<Button>("Quit").onClick.AddListener(() => QuitTap?.Invoke());
	}
}
