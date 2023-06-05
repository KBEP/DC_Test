using UnityEngine;

//просто стартует приложение, обеспечивая единственную точку входа в программу
public sealed class AppStarter : MonoBehaviour
{
	void Awake ()
	{
		new App().Start();
		Destroy(this.gameObject);
	}
}
