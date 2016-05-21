using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LeapTarget : PMonoBehaviour
{
	public SpriteRenderer Renderer;

	public Lane Lane
	{
		get { return lane; }
	}

	Lane lane;

	public void SetLane(Lane lane)
	{
		this.lane = lane;

		transform.SetPosition(lane.transform.position, Axes.Y);
	}
}
