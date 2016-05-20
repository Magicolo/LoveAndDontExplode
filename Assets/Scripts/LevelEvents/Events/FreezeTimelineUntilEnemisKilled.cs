using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Editor.Internal;

[RequireComponent(typeof(LevelEventActivator), typeof(Rigidbody2D))]
public class FreezeTimelineUntilEnemisKilled : ILevelEvent
{

	FreezeMotion freeze;
	public int UntilRemaingXEnemis;
	bool allDead { get { return GameObject.FindGameObjectsWithTag("Enemis").Length <= UntilRemaingXEnemis; } }

	internal override void Activate()
	{
		freeze = LevelEvents.WorldCollider.GetOrAddComponent<FreezeMotion>();
	}

	void Update()
	{
		if (allDead)
		{
			freeze.Destroy();
		}
	}

	void OnDrawGizmos()
	{
		float y = LevelEvents.transform.position.y;
		Vector3 down = new Vector3(transform.position.x, y);

		Gizmos.color = Color.red;
		Gizmos.DrawLine(down, down + new Vector3(0, LevelHeight));
		Gizmos.DrawSphere(down, 1);
		Gizmos.DrawSphere(down + new Vector3(0, LevelHeight), 1);

		if (freeze != null)
		{
			int nbEnemis = GameObject.FindGameObjectsWithTag("Enemis").Length;
			DrawUtility.DrawText(down + new Vector3(0, -2), nbEnemis + " > " + UntilRemaingXEnemis, Color.red);
		}

	}
}