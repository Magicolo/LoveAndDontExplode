﻿using UnityEngine;
using System.Collections.Generic;
using System;

namespace Pseudo
{
	[System.Serializable]
	public class LayerData
	{
		public int id;

		public float TileWidth = 1;
		public float TileHeight = 1;

		public readonly int LayerWidth;
		public readonly int LayerHeight;

		private TileData[] tiles;

		public Transform LayerTransform;

		public string Name
		{
			get { return LayerTransform.name; }
			set { this.LayerTransform.name = value; }
		}

		public bool IsActive { get { return LayerTransform.gameObject.activeInHierarchy; } }

		public LayerData(Transform parent, string name, int layerWidth, int layerheight)
		{
			LayerWidth = layerWidth;
			LayerHeight = layerheight;
			tiles = new TileData[LayerWidth * LayerHeight];
			tiles.Fill(TileData.Empty);

			GameObject gameObject = new GameObject(name);
			gameObject.transform.parent = parent;
			LayerTransform = gameObject.transform;
		}

		public TileData this[int x, int y]
		{
			get { return getTile(x, y); }
			set { tiles[x + y * LayerWidth] = value; }
		}

		private TileData getTile(int x, int y)
		{
			int index = x + y * LayerWidth;
			if (index >= tiles.Length)
				return null;
			else
				return tiles[index];
		}

		public TileData this[Point2 point]
		{
			get
			{
				return tiles[point.X + point.Y * LayerWidth];
			}
			set { tiles[point.X + point.Y * LayerWidth] = value; }
		}



		/// <summary>
		/// After a Destroy, this instance is unusable, a new instance must be instanciated.
		/// </summary>
		public void DestroyAllAndClear()
		{
			if (tiles == null) return;
			for (int i = 0; i < tiles.Length; i++)
			{
				tiles[i].GameObject.Destroy();
			}
			LayerTransform.gameObject.Destroy();
		}

		public void SetVisible(bool visible)
		{
			LayerTransform.gameObject.SetActive(visible);
		}

		public bool IsInLayerBound(int x, int y)
		{
			return x.IsBetween(0, LayerWidth - 1) && y.IsBetween(0, LayerHeight - 1);
		}

		public bool IsInArrayBound(Vector2 vector2)
		{
			return IsInLayerBound((int)vector2.x, (int)vector2.y);
		}

		public int Count
		{
			get { return tiles.Length; }
		}

		public LayerData Clone()
		{
			LayerData newLayer = (LayerData)MemberwiseClone();
			newLayer.tiles = new TileData[LayerWidth * LayerHeight];
			newLayer.tiles.Fill(TileData.Empty);

			GameObject newLayerGameObject = new GameObject(LayerTransform.name + " Copie");
			newLayerGameObject.transform.parent = LayerTransform.parent;
			newLayer.LayerTransform = newLayerGameObject.transform;

			for (int y = 0; y < LayerHeight; y++)
			{
				for (int x = 0; x < LayerWidth; x++)
				{
					newLayer.AddTile(new Point2(x, y), this[x, y]);
				}
			}
			return newLayer;
		}

		public void AddTile(Point2 tilePoint, TileData prefab)
		{
			if (this[tilePoint.X, tilePoint.Y] == null) return;
			AddTile(tilePoint, prefab.TileType);
			this[tilePoint.X, tilePoint.Y].Transform.localScale = prefab.Transform.localScale;
			this[tilePoint.X, tilePoint.Y].Transform.rotation = prefab.Transform.rotation;
		}
		public void AddTile(Point2 tilePoint, TileType tileType, int rotationFlags)
		{
			if (this[tilePoint.X, tilePoint.Y] == null) return;
			AddTile(tilePoint, tileType);
			ArchitectRotationHandler.ApplyFlipFlags(this[tilePoint.X, tilePoint.Y].Transform, rotationFlags);
		}
		public void AddTile(Point2 tilePoint, TileType tileType, ArchitectRotationFlip rotationFlip)
		{
			if (this[tilePoint.X, tilePoint.Y] == null) return;
			AddTile(tilePoint, tileType);
			rotationFlip.ApplyTo(this[tilePoint.X, tilePoint.Y].Transform);
		}
		public void AddTile(Point2 tilePoint, TileType tileType, float angle, bool horizontal, bool vertical)
		{
			if (this[tilePoint.X, tilePoint.Y] == null) return;
			AddTile(tilePoint, tileType);
			ArchitectRotationHandler.ApplyRotationFlip(this[tilePoint.X, tilePoint.Y].Transform, angle, horizontal, vertical);
		}
		public void AddTile(Point2 tilePoint, TileType tileType)
		{
			if (!InRange(tilePoint)) return;
			if (tileType == null || tileType.Prefab == null) return;
			GameObject newTile = UnityEngine.Object.Instantiate(tileType.Prefab);

			newTile.transform.SetPosition(new Vector3(tilePoint.X, tilePoint.Y, 0));
			newTile.transform.parent = LayerTransform;
			newTile.name = tileType.Prefab.name;

			TileData tileData = new TileData(tileType, newTile);
			this[tilePoint.X, tilePoint.Y] = tileData;
		}

		private bool InRange(Point2 tilePoint)
		{
			return getTile(tilePoint.X, tilePoint.Y) != null;
		}

		public bool IsTileEmpty(Point2 tilePoint)
		{
			TileData tileData = this[tilePoint.X, tilePoint.Y];
			return (tileData == null || tileData.TileType.IsNullOrIdZero());
		}

		public void EmptyTile(Point2 tilePoint)
		{
			this[tilePoint.X, tilePoint.Y].GameObject.Destroy();
			this[tilePoint.X, tilePoint.Y] = TileData.Empty;
		}

	}
}


