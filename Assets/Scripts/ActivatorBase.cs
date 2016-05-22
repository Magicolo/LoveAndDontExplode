using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class ActivatorBase : PMonoBehaviour
{
	[Header("Uses 'Activate' action.")]
	public InputComponent Input;

	public abstract bool Activate();
	public abstract bool Deactivate();
}