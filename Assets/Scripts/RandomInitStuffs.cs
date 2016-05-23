using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class RandomInitStuffs : MonoBehaviour
{

	public Sprite[] PossibleSprites;
	public SpriteRenderer Renderer;

	public MinMax Rotation;

	void Start()
	{
		gameObject.transform.RotateTowards(Rotation.GetRandom(), 1, Axes.Z);
		Renderer.sprite = PossibleSprites.GetRandom();
	}
}