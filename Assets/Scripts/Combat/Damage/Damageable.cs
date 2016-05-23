using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class Damageable : DamageableBase
{
	[Header("Sends 'OnDamaged' when damaged.")]
	[Header("Sends 'OnKilled' on death.")]
	public float MaxHealth = 100;

	public override bool Alive { get { return Health > 0; } }
	public float Health { get; set; }

	[Button]
	public bool damageOne;
	void DamageOne()
	{
		Damage(new DamageInfo { Damage = 1f, Sources = DamageableBy.Sources, Types = DamageableBy.Types });
	}

	[Button]
	public bool kill;
	void Kill()
	{
		Damage(new DamageInfo { Damage = Health, Sources = DamageableBy.Sources, Types = DamageableBy.Types });
	}

	public override bool Damage(DamageInfo info)
	{
		// If already dead, skip.
		if (!Alive || !CanBeDamagedBy(info))
			return false;

		Health -= info.Damage;

		if (Alive)
			SendMessage("OnDamaged", SendMessageOptions.DontRequireReceiver);
		else
			SendMessage("OnKilled", SendMessageOptions.DontRequireReceiver);

		return Alive;
	}

	void Awake()
	{
		OnCreate();
	}

	void OnCreate()
	{
		Health = MaxHealth;
	}
}
