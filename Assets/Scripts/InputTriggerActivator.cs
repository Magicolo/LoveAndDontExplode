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
			// In use by self or other player.
			if (activateable.Active)
			{
				// Deactivation is successful. Reactivate motion.
				if (activateable.Deactivate(this))
					freezer.Destroy();
			}
			// Activation is successful. Move to activateable and deactivate motion.
			else if (activateable.Activate(this))
			{
				transform.position = activateable.transform.position;
				transform.rotation = activateable.transform.rotation;
				freezer = gameObject.AddComponent<FreezeMotion>();
			}
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
}