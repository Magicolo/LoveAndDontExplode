﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pseudo.Internal.Communication
{
	public interface IEventDispatcher
	{
		void Subscribe(Delegate receiver);
		void Unsubscribe(Delegate receiver);
		void Trigger(object argument1, object argument2, object argument3, object argument4);
	}
}
