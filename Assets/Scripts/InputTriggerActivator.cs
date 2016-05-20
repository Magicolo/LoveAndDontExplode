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
	[Header("Uses 'Activate' action.")]
	public Players Player;

	Activateable activateable;
	[Inject]
	IInputManager inputManager = null;
	PlayerInput input;
	FreezeMotion freezer;

	protected override void Start()
	{
		base.Start();

		input = inputManager.GetAssignedInput(Player);
	}

	void Update()
	{
		if (input.GetAction("Activate").GetKeyDown() && activateable != null)
		{
			// In use by self or other player.
			if (activateable.InUse)
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