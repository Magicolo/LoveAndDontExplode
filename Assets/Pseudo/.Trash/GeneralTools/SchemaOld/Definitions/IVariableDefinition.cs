﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using System.Reflection;
using Pseudo.Internal.Schema;

namespace Pseudo
{
	public interface IVariableDefinition<TValue> : IVariableDefinition
	{
		FunctionOut<TValue> Getter { get; }
		FunctionIn<TValue> Setter { get; }
	}

	public interface IVariableDefinition
	{
		string Name { get; }

		IVariableGetNode CreateGetNode();
		IVariableSetNode CreateSetNode();
	}
}
