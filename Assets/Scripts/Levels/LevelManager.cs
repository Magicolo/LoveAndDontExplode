using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LevelManager : PMonoBehaviour
{
	public Damageable[] Ships;
	public Lane[] Lanes;

	void Update()
	{
		bool allDead = Array.TrueForAll(Ships, s => s == null || !s.Alive);
	}
}
