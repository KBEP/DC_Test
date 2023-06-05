using UnityEngine;

public static class CameraHelper
{
	const int VIEWPORT_CORNER_COUNT = 4;

	// парные углы вьюпорта, координаты углов слегка выходят за рамки вьюпорта,
	// чтобы генерируемая точка гарантированно не попадала в зону видимости вьюпорта
	//
	// 1--------2
	// |        |
	// 0--------3
	//
	// углы составляют пары 0-1, 1-2, 2-3, 3-0 (перебор по часовой стрелке)
	static (Vector2, Vector2)[] twins = new (Vector2, Vector2)[VIEWPORT_CORNER_COUNT]
	{
		( new Vector2(-0.1f, -0.1f), new Vector2(-0.1f,  1.1f) ),
		( new Vector2(-0.1f,  1.1f), new Vector2( 1.1f,  1.1f) ),
		( new Vector2( 1.1f,  1.1f), new Vector2( 1.1f, -0.1f) ),
		( new Vector2( 1.1f, -0.1f), new Vector2(-0.1f, -0.1f) )
	};

	//генерирует на поверхности точку, невидимую заданной камерой
	public static bool TryGenGroundPointOutside (Camera camera, out Vector3 result,
	  int groundLayerMask = Physics.DefaultRaycastLayers)
	{
		if (camera)
		{
			//индекс угла, стартовое значение выбирается случайно для обеспечения равномерной выборки
			int idx = Random.Range(0, VIEWPORT_CORNER_COUNT);
			int count = 0;

			while (++count <= VIEWPORT_CORNER_COUNT)//перебираем все углы вьюпорта
			{
				Vector2 point1 = twins[idx].Item1;//угол вьюпорта
				Vector2 point2 = twins[idx].Item2;//его парный угол

				//случайная точка между парными углами вьюпорта
				Vector2 lerped = Vector2.Lerp(point1, point2, Random.value);
				Ray ray = camera.ViewportPointToRay(lerped);

				if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
				{
					result = hit.point;
					return true;
				}

				//переключаемся на следующую пару углов
				if (++idx >= VIEWPORT_CORNER_COUNT) idx = 0;
			}
		}

		result = default;
		return false;
	}
}
