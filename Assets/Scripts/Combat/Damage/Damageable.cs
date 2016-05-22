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
