using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class ModuleBase : PMonoBehaviour
{
	ActivatorBase owner;

	void Update()
	{
		if (owner == null)
			return;

		UpdateModule(owner);
	}

	protected virtual void OnActivated(ActivatorBase owner)
	{
		this.owner = owner;
	}

	protected virtual void OnDeactivated(ActivatorBase owner)
	{
		if (this.owner == owner)
			this.owner = null;
	}

	public abstract void UpdateModule(ActivatorBase owner);
}
