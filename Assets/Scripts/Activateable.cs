using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class Activateable : PMonoBehaviour
{
	public SelectorBase Selector;
	public ActivatorBase Owner
	{
		get { return owner; }
	}
	public bool InUse
	{
		get { return owner != null; }
	}

	ActivatorBase owner;
	int inRangeCounter;

	public bool Activate(ActivatorBase owner)
	{
		if (InUse)
			return false;

		this.owner = owner;
		UpdateSelector();

		return true;
	}

	public bool Deactivate(ActivatorBase owner)
	{
		if (this.owner == owner)
		{
			this.owner = null;
			UpdateSelector();

			return true;
		}

		return false;
	}

	public void EnterRange(ActivatorBase activator)
	{
		inRangeCounter++;
		UpdateSelector();
	}

	public void ExitRange(ActivatorBase activator)
	{
		inRangeCounter--;
		UpdateSelector();
	}

	void UpdateSelector()
	{
		if (Selector != null)
		{
			Selector.Showing = inRangeCounter > 0;
			Selector.InUse = InUse;
		}
	}
}
