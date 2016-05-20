using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(Time))]
public class WorldCollider : MonoBehaviour
{
	public float Speed;

	void Update() 
	{
		TimeComponent time = GetComponent<TimeComponent>();
		transform.Translate(Speed * time.DeltaTime,0,0);
	}
}