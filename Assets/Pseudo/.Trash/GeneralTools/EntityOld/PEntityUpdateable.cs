﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Pseudo.Internal.EntityOld;

namespace Pseudo.Internal.EntityOld
{
	public partial class PEntity : IEntityUpdateable
	{
		readonly List<IStartable> startables = new List<IStartable>();
		readonly List<float> updateCounters = new List<float>();
		readonly List<IUpdateable> updateables = new List<IUpdateable>();
		readonly List<float> lateUpdateCounters = new List<float>();
		readonly List<ILateUpdateable> lateUpdateables = new List<ILateUpdateable>();
		readonly List<IFixedUpdateable> fixedUpdateables = new List<IFixedUpdateable>();

		void ComponentStart()
		{
			for (int i = 0; i < startables.Count; i++)
			{
				var startable = startables[i];

				if (startable.Active)
				{
					startables.RemoveAt(i--);
					startable.Start();
				}
			}
		}

		void RegisterComponentToUpdateCallbacks(IComponentOld component)
		{
			var startable = component as IStartable;
			if (startable != null)
				startables.Add(startable);

			var updateable = component as IUpdateable;
			if (updateable != null && !updateables.Contains(updateable))
			{
				updateables.Add(updateable);
				updateCounters.Add(0f);
			}

			var lateUpdateable = component as ILateUpdateable;
			if (lateUpdateable != null && !lateUpdateables.Contains(lateUpdateable))
			{
				lateUpdateables.Add(lateUpdateable);
				lateUpdateCounters.Add(0f);
			}

			var fixedUpdateable = component as IFixedUpdateable;
			if (fixedUpdateable != null && !fixedUpdateables.Contains(fixedUpdateable))
				fixedUpdateables.Add(fixedUpdateable);
		}

		void UnregisterComponentFromUpdateCallbacks(IComponentOld component)
		{
			var startable = component as IStartable;
			if (startable != null)
				startables.Remove(startable);

			var updateable = component as IUpdateable;
			if (updateable != null)
			{
				int index = updateables.IndexOf(updateable);

				if (index >= 0)
				{
					updateables.RemoveAt(index);
					updateCounters.RemoveAt(index);
				}
			}

			var lateUpdateable = component as ILateUpdateable;
			if (lateUpdateable != null)
			{
				int index = lateUpdateables.IndexOf(lateUpdateable);

				if (index >= 0)
				{
					lateUpdateables.RemoveAt(index);
					lateUpdateCounters.RemoveAt(index);
				}
			}

			var fixedUpdateable = component as IFixedUpdateable;
			if (fixedUpdateable != null)
				fixedUpdateables.Remove(fixedUpdateable);
		}

		void IEntityUpdateable.ComponentUpdate()
		{
			ComponentStart();

			for (int i = 0; i < updateables.Count; i++)
			{
				var updateable = updateables[i];

				if (updateable.Active)
				{
					float updateCounter = (updateCounters[i] += Time.unscaledDeltaTime);

					if (updateCounter >= updateable.UpdateDelay)
					{
						updateCounters[i] -= updateable.UpdateDelay;
						updateable.Update();
					}
				}
			}
		}

		void IEntityUpdateable.ComponentLateUpdate()
		{
			ComponentStart();

			for (int i = 0; i < lateUpdateables.Count; i++)
			{
				var lateUpdateable = lateUpdateables[i];

				if (lateUpdateable.Active)
				{
					float lateUpdateCounter = (lateUpdateCounters[i] += Time.unscaledDeltaTime);

					if (lateUpdateCounter >= lateUpdateable.LateUpdateDelay)
					{
						lateUpdateCounters[i] -= lateUpdateable.LateUpdateDelay;
						lateUpdateable.LateUpdate();
					}
				}
			}
		}

		void IEntityUpdateable.ComponentFixedUpdate()
		{
			ComponentStart();

			for (int i = 0; i < fixedUpdateables.Count; i++)
			{
				var fixedUpdateable = fixedUpdateables[i];

				if (fixedUpdateable.Active)
					fixedUpdateable.FixedUpdate();
			}
		}
	}
}
