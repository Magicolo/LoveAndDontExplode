﻿using UnityEngine;
using System.Collections.Generic;


namespace Pseudo.Architect
{
	[System.Serializable]
	public abstract class ToolCommandBase : ArchitectCommand
	{
		protected ArchitectTilePositionGetter tilePositionGetter;

		protected LayerData Layer { get { return tilePositionGetter.Layer; } }
		protected Point2 TilePosition { get { return tilePositionGetter.TilePosition; } }
		protected Vector3 TileWorldPosition { get { return tilePositionGetter.TileWorldPosition; } }

		protected ToolCommandBase(ArchitectTilePositionGetter tilePositionGetter)
		{
			this.tilePositionGetter = tilePositionGetter;
		}
	}

}
