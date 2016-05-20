﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

namespace Pseudo.Injection
{
	public interface IConstructorSelector
	{
		IInjectableConstructor Select(InjectionContext context, IInjectableConstructor[] constructors);
	}
}
