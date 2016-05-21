using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Pseudo.Injection;

public class LevelInstaller : InstallerBehaviourBase
{
	public LevelManager LevelManager;

	public override void Install(IContainer container)
	{
		container.Binder.Bind<LevelManager>().ToMethod(c => InstantiateOrFind(LevelManager)).AsSingleton();
	}
}
