using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Editor.Internal;

[RequireComponent(typeof(Time))]
public class WorldCollider : MonoBehaviour
{
	void Update()
	{
		if (GetComponents<FreezeMotion>().Length == 0)
		{
			TimeComponent time = GetComponent<TimeComponent>();
			transform.Translate(time.DeltaTime, 0, 0);
		}

	}

	void OnDrawGizmos()
	{
		Vector3 halfScale = new Vector3(0, transform.localScale.y / 2, 0);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position - halfScale, transform.position + halfScale);
	}
}