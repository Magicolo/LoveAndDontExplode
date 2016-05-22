using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LevelBound : MonoBehaviour
{
	public Camera Cam;

	void OnDrawGizmos()
	{
		if (Cam == null)
			return;

		Vector3 bl = Cam.WorldBottomLeft();
		Vector3 br = Cam.WorldBottomRight();
		Vector3 tl = Cam.WorldTopLeft();
		Vector3 tr = Cam.WorldTopRight();

		Gizmos.color = new Color(1, 0.8f, 0);

		Gizmos.DrawLine(bl, br);
		Gizmos.DrawLine(tl, tr);
		Gizmos.DrawLine(tl, bl);
		Gizmos.DrawLine(tr, br);
	}

}