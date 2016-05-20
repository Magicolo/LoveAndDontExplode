﻿using UnityEngine;
using System.Collections;
using System;
using Pseudo;
using Pseudo.Injection;
using Pseudo.EntityFramework;
using Pseudo.Communication;
using Pseudo.Groupingz;

namespace Pseudo
{
	public enum Cameras
	{
		Main,
		UI
	}

	public class SceneInstaller : InstallerBehaviourBase
	{
		public Camera MainCamera;
		public Camera UICamera;

		public override void Install(IContainer container)
		{
			container.Binder.Bind<EntityManager, IEntityManager>().ToSelf().AsSingleton();
			container.Binder.Bind<Messager, IMessager>().ToSelf().AsSingleton();
			container.Binder.Bind<Camera>().ToInstance(MainCamera).WhenHas(Cameras.Main);
			container.Binder.Bind<Camera>().ToInstance(UICamera).WhenHas(Cameras.UI);
		}
	}
}