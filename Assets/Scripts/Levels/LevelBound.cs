using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LevelBound : MonoBehaviour
{


	[Min(0)]
	public float PlayerBulletMaxZone;
	[Min(0)]
	public float EnemisBulletMaxZone;

	[Space()]
	public Camera Cam;
	public BulletBound PlayerBulletBound;
	public BulletBound EnemisBulletBound;


	[Button("Update Bounds", "UpdateBounds")]
	public bool updateBounds;

	void UpdateBounds()
	{
		PlayerBulletBound.Resize(Cam.WorldRectWithExtend(PlayerBulletMaxZone));
		EnemisBulletBound.Resize(Cam.WorldRectWithExtend(EnemisBulletMaxZone));
	}

	void OnDrawGizmos()
	{
		if (Cam == null)
			return;

		Gizmos.color = new Color(1, 0.8f, 0);
		DrawRectangle(Cam.WorldRect());

		Gizmos.color = new Color(1, 0.3f, 0, 0.5f);
		DrawRectangle(Cam.WorldRectWithExtend(PlayerBulletMaxZone));

		Gizmos.color = new Color(1, 0, 0, 0.5f);
		DrawRectangle(Cam.WorldRectWithExtend(EnemisBulletMaxZone));
	}


	void DrawRectangle(Rect rect)
	{
		Gizmos.DrawLine(rect.BottomLeft(), rect.BottomRight());
		Gizmos.DrawLine(rect.BottomLeft(), rect.TopLeft());
		Gizmos.DrawLine(rect.TopLeft(), rect.TopRight());
		Gizmos.DrawLine(rect.TopRight(), rect.BottomRight());
	}

	void DrawRectangle(Vector3 bottomLeft, Vector3 topRight)
	{
		Vector3 x = new Vector3(topRight.x - bottomLeft.x, 0);

		Gizmos.DrawLine(bottomLeft, bottomLeft + x);
		Gizmos.DrawLine(bottomLeft + x, topRight);
		Gizmos.DrawLine(bottomLeft, topRight - x);
		Gizmos.DrawLine(topRight - x, topRight);
	}
}