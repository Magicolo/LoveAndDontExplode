using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class Damageable : PMonoBehaviour
{
	[Header("Sends 'OnDamaged' when damaged.")]
	[Header("Sends 'OnKilled' on death.")]
	public float MaxHealth = 100;

	public bool Alive { get { return Health > 0; } }
	public float Health { get; set; }

	public bool Damage(float damage)
	{
		// If already dead, skip.
		if (!Alive)
			return false;

		Health -= damage;

		if (Alive)
			SendMessage("OnDamaged");
		else
			SendMessage("OnKilled");

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
