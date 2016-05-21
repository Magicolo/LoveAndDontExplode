using UnityEngine;
using System.Collections;
using Pseudo;

public abstract class MotionBase : PMonoBehaviour
{
	public abstract void Move(Vector2 motion, bool instant = false);
	public abstract void MoveTo(Vector2 motion, bool instant = false);
	public abstract void Rotate(float angle, bool instant = false);
	public abstract void RotateTo(float angle, bool instant = false);
}
