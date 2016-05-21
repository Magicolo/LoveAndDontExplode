using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public abstract class TargetBase : MonoBehaviour
{

	public Vector3 Target { get { return GetTarget(); } }

	public abstract Vector3 GetTarget();
}