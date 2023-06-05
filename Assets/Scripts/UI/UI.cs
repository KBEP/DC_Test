using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public sealed class UI : MonoBehaviour
{
	Stack<UIShield> history = new Stack<UIShield>();
	UIShield current;

	UIMainMenu uiMainMenu;
	UILoading uiLoadinng;

	public IUIMainMenuEvents MainMenu => uiMainMenu;

	void Awake ()
	{
		uiMainMenu = transform.GetChildComponentEx<UIMainMenu>("MainMenu");
		uiLoadinng = transform.GetChildComponentEx<UILoading>("Loading");
	}

	public void OpenMainMenu ()
	{
		CloseCurrent();
		current = uiMainMenu;
		uiMainMenu.Open();
	}

	public void OpenLoading (AsyncOperation operation, Action<ValueType> callback, ValueType data)
	{
		CloseCurrent();
		current = uiLoadinng;
		uiLoadinng.Open(operation, callback, data);
	}

	public void GoBack (int step = 1)
	{
		if (current != null) current.Close();
		current = PopShield(step);
		if (current != null) current.Open();
	}

	void CloseCurrent ()
	{
		if (current == null) return;
		current.Close();
		if (current.SaveInHistory) history.Push(current);
	}

	UIShield PopShield (int step = 1)
	{
		UIShield result = null;
		while (step-- > 0 && history.Count > 0) result = history.Pop();
		return result;
	}
}
