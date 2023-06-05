using UnityEngine;
using System.Collections.Generic;

public static class EM_Transform
{
	public static Transform Find (this Transform t, string path, bool log)
	{
		Transform result = t.Find(path);
		if (result == null && log) Debug.LogError(string.Format("Transform '{0}' not found.", path));
		return result;
	}

	public static Transform FindEx (this Transform t, string path)
	{
		Transform result = t.Find(path, false);
		if (result == null) throw new TransformNotFoundException(path);
		return result;
	}
	
	public static T GetChildComponent<T> (this Transform t, string path, bool log = true) where T : Component
	{
		Transform child = t.Find(path);

		if (child == null)
		{
			if (log)
			{
				string fullPath = TransformPathGenerator.GenFullPath(child, path);
				Debug.LogError(string.Format("Transform '{0}' not found.", fullPath));
			}
			return null;
		}

		if (!child.TryGetComponent<T>(out T component))
		{
			if (log)
			{
				string fullPath = TransformPathGenerator.GenFullPath(child, path);
				Debug.LogError(string.Format("Component '{0}' not found in '{1}'.", typeof(T).FullName, fullPath));
			}
			return null;
		}
		
		return component;
	}

	public static T GetChildComponentEx<T> (this Transform t, string path) where T : Component
	{
		T result = GetChildComponent<T>(t, path, false);
		if (result == null) throw new NoRequiredComponentException(typeof(T), path);
		return result;
	}

	public static void DestroyChildren (this Transform t)
	{
		int i = t.childCount;
		while (i != 0) GameObject.Destroy(t.GetChild(--i).gameObject);
		t.DetachChildren();
	}

	public static void ActivateChildren (this Transform t)
	{
		int childCount = t.childCount;
		for (int i = 0; i < childCount; i++) t.GetChild(i).gameObject.SetActive(true);
	}

	public static void DeactivateChildren (this Transform t)
	{
		int childCount = t.childCount;
		for (int i = 0; i < childCount; i++) t.GetChild(i).gameObject.SetActive(false);
	}

	public static List<T> GetChildrenComponents<T> (this Transform t) where T : Component
	{
		List<T> result = new List<T>();
		int childCount = t.childCount;
		for (int i = 0; i < childCount; i++)
		{
			T c = t.GetChild(i).GetComponent<T>();
			if (c != null) result.Add(c);
		}
		return result;
	}

	public static class TransformPathGenerator
	{
		static readonly System.Text.StringBuilder builder = new System.Text.StringBuilder();
		
		public static string GenFullPath (Transform transform, string addPath)
		{
			builder.Clear();
			
			while (transform != null)
			{
				builder.Insert(0, transform.name);
				builder.Insert(0, '/');
				transform = transform.parent;
			}

			if (addPath != null)
			{
				if (!addPath.StartsWith('/')) builder.Append('/');
				builder.Append(addPath);
			}

			string result = builder.ToString();

			builder.Clear();

			return result;
		}
	}
}
