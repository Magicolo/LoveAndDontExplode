using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class BoxCastDOTDamager : DamagerBase
{
	public Vector2 BoxSize;
	public TimeComponent Time;
	public LayerMask Mask;

	void Update()
	{
		var hits = Physics2D.BoxCastAll(transform.position.ToVector2() + BoxSize * 0.5f, BoxSize, 0f, transform.right, 1f, Mask);

		for (int i = 0; i < hits.Length; i++)
		{
			var damageable = hits[i].collider.GetComponent<DamageableBase>(HierarchyScopes.Self | HierarchyScopes.Ancestors);

			if (damageable != null)
			{
				Damage(damageable, new DamageInfo
				{
					Damage = DamageToCause.Damage * Time.DeltaTime,
					Sources = DamageToCause.Sources,
					Types = DamageToCause.Types
				});
			}
		}
	}
}
