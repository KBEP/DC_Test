using UnityEngine;

public sealed class BattlefieldSceneRoot : MonoBehaviour
{
	World world;

	public World World => world;

	void Awake ()
	{
		world = transform.GetChildComponentEx<World>("World");
	}
}
