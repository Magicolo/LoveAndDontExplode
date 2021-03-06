﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

namespace Pseudo.Grouping
{
	public interface IGameObjectElement : IElement<GameObject>
	{
		bool Validate(Type type);
	}
}
