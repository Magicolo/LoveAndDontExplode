using UnityEngine;
using System.Collections;
using Pseudo;

public abstract class MotionBase : PMonoBehaviour
{
	public abstract void Move(Vector2 motion);
	public abstract void MoveTo(Vector2 motion);
	public abstract void Rotate(float angle);
	public abstract void RotateTo(float angle);
}
