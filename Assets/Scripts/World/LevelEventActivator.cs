using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class LevelEventActivator : MonoBehaviour
{

	

	void OnTriggerEnter2D(Collider2D other)
	{
		var worldCollider = other.GetComponent<WorldCollider>();
		if (worldCollider)
		{
			
		}
	}
}