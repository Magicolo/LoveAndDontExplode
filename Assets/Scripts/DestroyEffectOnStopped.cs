using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class DestroyEffectOnStopped : PMonoBehaviour
{
	public ParticleSystem Effect;

	void LateUpdate()
	{
		if (Effect.isStopped)
			Effect.gameObject.Destroy();
	}
}
