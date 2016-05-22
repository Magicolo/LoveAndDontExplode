using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

public class BikerCombatAi : ControllerBase
{

	public MinMax AttackingZone;
	public WeaponBase[] Weapons;

	void Update()
	{
		var minX = cam.ViewportToWorldPoint(new Vector3(AttackingZone.Min, 0, -cam.transform.position.z)).x;
		var maxX = cam.ViewportToWorldPoint(new Vector3(AttackingZone.Max, 0, -cam.transform.position.z)).x;

		if (transform.position.x.IsBetween(minX, maxX))
			foreach (var w in Weapons)
				if (w.CanFire())
					w.Fire();

	}
}