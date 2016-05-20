﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

namespace Pseudo.Internal.Schema
{
	public interface IFunctionNode : IExecutionNode
	{
		IFunctionDefinition FunctionDefinition { get; }
	}
}
