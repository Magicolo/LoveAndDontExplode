﻿using UnityEngine;
using System.Collections.Generic;
using System;

namespace Pseudo
{
	[System.Serializable]
	public class TileData
	{
		public TileType TileType;
		public GameObject GameObject;

		public Transform Transform { get { return GameObject.transform; } }

		public TileData(TileType tileType, GameObject gameObject)
		{
			this.TileType = tileType;
			this.GameObject = gameObject;
		}

		public static TileData Empty = new TileData(new TileType(0), null);

		public TileData Clone()
		{
			TileData newTile = (TileData)MemberwiseClone();
			return newTile;
		}
	}

}
