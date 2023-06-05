using UnityEngine;

[RequireComponent (typeof(Canvas))]

public class UIShield : MonoBehaviour
{
	Canvas canvas;

	public virtual bool SaveInHistory => true;

	protected virtual void Awake ()//не забывай вызывать этот метод в классах-наследниках если будешь переопределять!
	{
		canvas = transform.GetComponentEx<Canvas>();
	}

	public virtual void Open ()
	{
		if (canvas) canvas.enabled = true;
	}

	public virtual void Close ()
	{
		if (canvas) canvas.enabled = false;
	}
}
