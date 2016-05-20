using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Editor.Internal;

public class LevelEvents : MonoBehaviour
{
	[Min(0)]
	public float LevelHeight;

	[Min(0)]
	public float LevelTime;

	[Min(0)]
	public float TrainLines;

	public WorldCollider WorldCollider;


	void OnDrawGizmos()
	{
		Vector3 downLeft = transform.position;

		Gizmos.color = Color.green;
		Gizmos.DrawLine(downLeft, transform.position + new Vector3(0, LevelHeight));
		Gizmos.DrawLine(downLeft, transform.position + new Vector3(LevelTime, 0));
		Gizmos.DrawLine(downLeft + new Vector3(0, LevelHeight), transform.position + new Vector3(LevelTime, LevelHeight));

		for (int i = 0; i < LevelTime; i += 10)
		{
			Vector3 v = downLeft + new Vector3(i, 0);
			Gizmos.DrawLine(v, v + new Vector3(0, 2));
			DrawUtility.DrawText(v, i + "s");
		}

		float spacing = LevelHeight / (TrainLines + 1);
		for (float i = 1; i <= TrainLines; i++)
		{
			Vector3 v = downLeft + new Vector3(0, spacing * i);
			Gizmos.color = new Color(0.75f, 1, 0.75f, 0.3f);
			Gizmos.DrawLine(v, v + new Vector3(LevelTime, 0));

		}
	}
}