using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Input;
using Pseudo.Injection;

public class InputTriggerActivator : ActivatorBase
{
	Activateable activateable;
	FreezeMotion freezer;

	void Update()
	{
		if (Input.GetAction("Activate").GetKeyDown() && activateable != null)
		{
			if (!Activate())
				Deactivate();
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		activateable = collision.GetComponent<Activateable>(HierarchyScopes.Self | HierarchyScopes.Parent);

		if (activateable != null)
			activateable.EnterRange(this);
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		activateable = collision.GetComponent<Activateable>(HierarchyScopes.Self | HierarchyScopes.Parent);

		if (activateable != null)
			activateable.ExitRange(this);
	}

	public override bool Activate()
	{
		// Activation is successful. Move to activateable and deactivate motion.
		if (!activateable.Active && activateable.Activate(this))
		{
			freezer = gameObject.AddComponent<FreezeMotion>();
			return true;
		}
		else
			return false;
	}

	public override bool Deactivate()
	{
		// In use by self or other player.
		if (activateable.Active)
		{
			// Deactivation is successful. Reactivate motion.
			if (activateable.Deactivate(this))
			{
				freezer.Destroy();
				return true;
			}
		}

		return false;
	}
}