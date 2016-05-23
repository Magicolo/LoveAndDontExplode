using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class DestroyOnKilled : PMonoBehaviour
{
	public GameObject ToDestroy;

	bool destroy;

	void LateUpdate()
	{
		if (destroy)
			ToDestroy.Destroy();
	}

	void OnKilled()
	{
		destroy = true;
	}
}
