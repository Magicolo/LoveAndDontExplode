using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class BulletBound : MonoBehaviour
{

	public float Weight = 3;

	public BoxCollider2D Top;
	public BoxCollider2D Bottom;
	public BoxCollider2D Left;
	public BoxCollider2D Right;


	public void Resize(Rect rect)
	{
		float w = rect.width;
		float h = rect.height;

		Top.size = new Vector2(w, Weight);
		Bottom.size = new Vector2(w, Weight);
		Left.size = new Vector2(Weight, h);
		Right.size = new Vector2(Weight, h);

		Top.transform.position = new Vector3(rect.TopLeft().x + w / 2, rect.TopLeft().y);
		Bottom.transform.position = new Vector3(rect.TopLeft().x + w / 2, rect.BottomLeft().y);

		Left.transform.position = new Vector3(rect.BottomLeft().x, rect.TopLeft().y + h / 2);
		Right.transform.position = new Vector3(rect.BottomRight().x, rect.TopLeft().y + h / 2);
	}
}