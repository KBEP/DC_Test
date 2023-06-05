using UnityEngine;

public static class EM_Component
{
	public static T GetComponent<T> (this Component c, bool log) where T : Component
	{
		T result = c.GetComponent<T>();
		if (result == null && log)
		{
			string fullPath = EM_Transform.TransformPathGenerator.GenFullPath(c.transform, null);
			Debug.LogError(string.Format("Component '{0}' not found in '{1}'.", nameof(T), fullPath));
		}
		return result;
	}

	public static T GetComponentEx<T> (this Component c) where T : Component
	{
		T result = c.GetComponent<T>();
		if (result == null) throw new NoRequiredComponentException(typeof(T));
		return result;
	}
}
