using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Input;
using Pseudo.Injection;

[AddComponentMenu("Input")]
public class InputComponent : PMonoBehaviour
{
	public Players Player;

	[Inject]
	IInputManager inputManager = null;

	public InputAction GetAction(string name)
	{
		return inputManager.GetAssignedInput(Player).GetAction(name);
	}
}
