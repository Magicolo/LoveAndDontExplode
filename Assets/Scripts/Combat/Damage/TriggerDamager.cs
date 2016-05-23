using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TriggerDamager : DamagerBase
{
	void OnTriggerEnter2D(Collider2D collision)
	{
		var damageable = collision.GetComponent<DamageableBase>(HierarchyScopes.Self | HierarchyScopes.Ancestors);

		if (damageable != null)
			Damage(damageable, DamageToCause);
	}
}
