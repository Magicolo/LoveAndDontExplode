using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

public class NavigationModule : ModuleBase
{
	[Header("Uses 'MotionX' and 'MotionY' axes.")]
	[Header("Uses 'Use' action.")]
	public MotionBase ShipMotion;
	public Lane StartLane;
	public LeapTarget LeapPreview;
	public float LeapDelay = 0.5f;
	public TimeComponent Time;

	[Inject]
	LevelManager levelManager = null;
	Lane currentLane;
	float leapCounter;

	protected override void Start()
	{
		base.Start();

		currentLane = StartLane;
	}

	public override void UpdateModule(ActivatorBase owner)
	{
		var input = new Vector2(owner.Input.GetAction("MotionX").GetAxis(), owner.Input.GetAction("MotionY").GetAxis());
		var use = owner.Input.GetAction("Use").GetKeyDown();

		if (leapCounter <= 0f)
		{
			if (Mathf.Abs(input.y) > 0.5f)
			{
				if (ShowPreview(input.y.Sign()) && use)
					BeginLeap(input.y.Sign());
			}
			else
				HidePreview();
		}
		else
			UpdateLeap();
	}

	/// <returns>Is the direction valid.</returns>
	bool ShowPreview(int direction)
	{
		int targetIndex = levelManager.Lanes.IndexOf(currentLane) + direction;

		if (targetIndex > 0 && targetIndex < levelManager.Lanes.Length)
		{
			LeapPreview.gameObject.SetActive(true);
			LeapPreview.Lane = levelManager.Lanes[targetIndex];
			return true;
		}
		else
			return false;
	}

	void HidePreview()
	{
		LeapPreview.gameObject.SetActive(false);
	}

	void BeginLeap(int direction)
	{
		ShowPreview(direction);
		leapCounter = LeapDelay;
	}

	void UpdateLeap()
	{
		leapCounter -= Time.DeltaTime;

		if (leapCounter <= 0f)
			EndLeap();
	}

	void EndLeap()
	{
		HidePreview();
		ShipMotion.MoveTo(LeapPreview.transform.position, true);
		leapCounter = 0f;
	}
}
