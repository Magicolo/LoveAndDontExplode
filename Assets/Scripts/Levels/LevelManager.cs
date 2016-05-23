using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

public class LevelManager : PMonoBehaviour
{
	public Damageable[] Ships;
	public Lane[] Lanes;

	[Inject]
	IGameManager gameManager = null;

	void Update()
	{
		bool allDead = Array.TrueForAll(Ships, s => s == null || !s.Alive);

		if (allDead)
			gameManager.ReloadScene();
	}
}
