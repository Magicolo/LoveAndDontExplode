using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class Activateable : PMonoBehaviour
{
	[Header("Sends 'OnActivated' on activation.")]
	[Header("Sends 'OnDeactivated' on deactivation.")]
	public SelectorBase Selector;
	public ActivatorBase Owner
	{
		get { return owner; }
	}
	public bool Active
	{
		get { return owner != null; }
	}

	ActivatorBase owner;
	int inRangeCounter;

	public bool Activate(ActivatorBase owner)
	{
		if (Active)
			return false;

		this.owner = owner;
		UpdateSelector();
		SendMessage("OnActivated", owner);

		return true;
	}

	public bool Deactivate(ActivatorBase owner)
	{
		if (this.owner == owner)
		{
			this.owner = null;
			UpdateSelector();
			SendMessage("OnDeactivated", owner);

			return true;
		}

		return false;
	}

	public void EnterRange(ActivatorBase owner)
	{
		inRangeCounter++;
		UpdateSelector();
	}

	public void ExitRange(ActivatorBase owner)
	{
		inRangeCounter--;
		UpdateSelector();
	}

	void UpdateSelector()
	{
		if (Selector != null)
		{
			Selector.Showing = inRangeCounter > 0;
			Selector.InUse = Active;
		}
	}
}
