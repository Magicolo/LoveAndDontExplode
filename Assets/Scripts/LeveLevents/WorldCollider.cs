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
	public float Speed;

	public float TravelTimePerScreen = 2;

	void Update() 
	{
		TimeComponent time = GetComponent<TimeComponent>();
		transform.Translate(Speed * time.DeltaTime,0,0);
	}

	void OnDrawGizmos() 
	{
		Vector3 halfScale = new Vector3(0, transform.localScale.y / 2, 0);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position - halfScale, transform.position + halfScale);
	}
}