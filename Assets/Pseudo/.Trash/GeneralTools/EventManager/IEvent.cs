﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pseudo
{
	public interface IEvent
	{
		bool Resolve();
	}
}
