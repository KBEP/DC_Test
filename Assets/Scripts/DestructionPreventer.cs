using UnityEngine;

public sealed class DestructionPreventer : MonoBehaviour
{
	void Awake ()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
