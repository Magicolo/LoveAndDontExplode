﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

public class LeapModule : ModuleBase
{
	[Header("Uses 'MotionY' action.")]
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
		var input = owner.Input.GetAction("MotionY").GetAxis();
		bool leaping = Mathf.Abs(input) > 0.5f;

		if (leapCounter <= 0f)
		{
			if (leaping && ShowPreview(input.Sign()))
				BeginLeap(input.Sign());
			else
				HidePreview();
		}
		else if (leaping)
			UpdateLeap();
		else
			CancelLeap();
	}

	/// <returns>Is the direction valid.</returns>
	bool ShowPreview(int direction)
	{
		int targetIndex = levelManager.Lanes.IndexOf(currentLane) + direction;

		if (targetIndex >= 0 && targetIndex < levelManager.Lanes.Length)
		{
			LeapPreview.gameObject.SetActive(true);
			LeapPreview.SetLane(levelManager.Lanes[targetIndex]);
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
		leapCounter = LeapDelay;
		ShowPreview(direction);
	}

	void UpdateLeap()
	{
		leapCounter -= Time.FixedDeltaTime;

		if (leapCounter <= 0f)
			EndLeap();
	}

	void CancelLeap()
	{
		leapCounter = 0f;
		HidePreview();
	}

	void EndLeap()
	{
		leapCounter = 0f;
		currentLane = LeapPreview.Lane;
		ShipMotion.MoveTo(LeapPreview.transform.position, true);
		HidePreview();
	}
}
