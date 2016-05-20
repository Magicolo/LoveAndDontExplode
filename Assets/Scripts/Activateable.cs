﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class Activateable : PMonoBehaviour
{
	public ActivatorBase Owner
	{
		get { return owner; }
	}
	public bool InUse
	{
		get { return owner != null; }
	}

	ActivatorBase owner;

	public bool Activate(ActivatorBase owner)
	{
		if (InUse)
			return false;

		this.owner = owner;

		return true;
	}

	public bool Deactivate(ActivatorBase owner)
	{
		if (this.owner == owner)
		{
			this.owner = null;
			return true;
		}

		return false;
	}
}