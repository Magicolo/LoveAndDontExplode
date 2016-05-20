using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Editor.Internal;

public class LevelEventActivator : MonoBehaviour
{

	bool activated;

	void OnTriggerEnter2D(Collider2D other)
	{
		var worldCollider = other.GetComponent<WorldCollider>();
		if (worldCollider)
			activateEvents();

	}

	private void activateEvents()
	{
		foreach (var levelEvent in GetComponents<ILevelEvent>())
			levelEvent.Activate();

		activated = true;
	}

	void OnDrawGizmos()
	{
		float x = GetComponentInParent<LevelEvents>().LevelTime;
		if (!transform.localPosition.x.IsBetween(0, x))
		{
			Gizmos.color = Color.red;
			Vector3 halfScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0);
			Gizmos.DrawLine(transform.position - halfScale, transform.position + halfScale);
			DrawUtility.DrawText(transform.position, "It's outside Of Time",Color.red);

		}
		if (activated)
		{
			Gizmos.color = Color.blue;
			Vector3 halfScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0);
			Gizmos.DrawLine(transform.position - halfScale, transform.position + halfScale);
		}
	}
}