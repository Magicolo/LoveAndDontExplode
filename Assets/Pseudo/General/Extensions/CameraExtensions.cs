using UnityEngine;
using System.Collections;

namespace Pseudo
{
	public static class CameraExtensions
	{
		public static Vector3 GetMouseWorldPosition(this Camera camera, float depth = 0f)
		{
			var mousePosition = UnityEngine.Input.mousePosition;
			mousePosition.z = depth - camera.transform.position.z;

			return camera.ScreenToWorldPoint(mousePosition);
		}

		public static bool WorldPointInView(this Camera camera, Vector3 worldPoint)
		{
			var viewPoint = camera.WorldToViewportPoint(worldPoint);

			return viewPoint.x >= 0f && viewPoint.x <= 1f && viewPoint.y >= 0f && viewPoint.y <= 1f;
		}

		public static bool ScreenPointInView(this Camera camera, Vector2 screenPoint)
		{
			var viewPoint = camera.ScreenToViewportPoint(screenPoint);

			return viewPoint.x >= 0f && viewPoint.x <= 1f && viewPoint.y >= 0f && viewPoint.y <= 1f;
		}

		public static bool WorldRectInView(this Camera camera, Rect worldRect, float depth = 0f)
		{
			return worldRect.Overlaps(camera.WorldRect(depth));
		}

		public static Rect WorldRect(this Camera camera, float depth = 0f)
		{
			float distance = depth - camera.transform.position.z;

			var min = camera.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
			var max = camera.ViewportToWorldPoint(new Vector3(1f, 1f, distance));

			return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
		}

		public static Rect WorldRectWithExtend(this Camera camera, float extend, float depth = 0f)
		{
			float distance = depth - camera.transform.position.z;

			var min = camera.ViewportToWorldPoint(new Vector3(0f - extend, 0f - extend, distance));
			var max = camera.ViewportToWorldPoint(new Vector3(1f + extend, 1f + extend, distance));

			return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
		}

		public static Vector3 ClampToScreen(this Camera camera, Vector3 worldPoint, Vector2 margin = default(Vector2))
		{
			var rect = camera.WorldRect(worldPoint.z);

			worldPoint.x = Mathf.Clamp(worldPoint.x, rect.xMin + margin.x, rect.xMax - margin.x);
			worldPoint.y = Mathf.Clamp(worldPoint.y, rect.yMin + margin.y, rect.yMax - margin.y);

			return worldPoint;
		}

		public static Vector3 WorldLeft(this Camera camera, float depth = 0f)
		{
			return camera.ViewportToWorldPoint(new Vector3(0f, 0.5f, depth - camera.transform.position.z));
		}

		public static Vector3 WorldRight(this Camera camera, float depth = 0f)
		{
			return camera.ViewportToWorldPoint(new Vector3(1f, 0.5f, depth - camera.transform.position.z));
		}

		public static Vector3 WorldTop(this Camera camera, float depth = 0f)
		{
			return camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, depth - camera.transform.position.z));
		}

		public static Vector3 WorldBottom(this Camera camera, float depth = 0f)
		{
			return camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, depth - camera.transform.position.z));
		}

		public static Vector3 WorldTopLeft(this Camera camera, float depth = 0f)
		{
			return camera.ViewportToWorldPoint(new Vector3(0f, 1f, depth - camera.transform.position.z));
		}

		public static Vector3 WorldTopRight(this Camera camera, float depth = 0f)
		{
			return camera.ViewportToWorldPoint(new Vector3(1f, 1f, depth - camera.transform.position.z));
		}

		public static Vector3 WorldBottomLeft(this Camera camera, float depth = 0f)
		{
			return camera.ViewportToWorldPoint(new Vector3(0f, 0f, depth - camera.transform.position.z));
		}

		public static Vector3 WorldBottomRight(this Camera camera, float depth = 0f)
		{
			return camera.ViewportToWorldPoint(new Vector3(1f, 0f, depth - camera.transform.position.z));
		}
	}
}
