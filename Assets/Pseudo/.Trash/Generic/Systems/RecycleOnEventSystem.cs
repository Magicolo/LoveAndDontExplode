﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

namespace Pseudo
{
	public class RecycleOnEventSystem : SystemBase
	{
		public override IEntityGroup GetEntities()
		{
			return EntityManager.Entities.Filter(typeof(RecycleOnEventComponent));
		}

		public override void OnActivate()
		{
			base.OnActivate();

			EventManager.SubscribeAll((Action<Events, IEntity>)OnEvent);
		}

		public override void OnDeactivate()
		{
			base.OnDeactivate();

			EventManager.UnsubscribeAll((Action<Events, IEntity>)OnEvent);
		}

		void OnEvent(Events identifier, IEntity entity)
		{
			if (!Entities.Contains(entity))
				return;

			var recycle = entity.GetComponent<RecycleOnEventComponent>();

			for (int i = 0; i < recycle.Events.Length; i++)
			{
				var recycleEvent = recycle.Events[i];

				if (recycleEvent.Event == identifier)
				{
					EntityManager.RecycleEntity(recycleEvent.Recycle);
					break;
				}
			}
		}
	}
}