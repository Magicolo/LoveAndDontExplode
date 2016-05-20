using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class ILevelEvent : PMonoBehaviour
{

	protected float LevelHeight { get { return GetComponentInParent<LevelEvents>().LevelHeight; } }
	protected LevelEvents LevelEvents { get { return GetComponentInParent<LevelEvents>(); } }

	internal abstract void Activate();
}