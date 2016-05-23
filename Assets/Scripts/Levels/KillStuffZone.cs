using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class KillStuffZone : PMonoBehaviour
{

	public string tagTokill;
	public bool killEVERYTHING;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<KillStuffZone>() != null)
			return;


		if (killEVERYTHING || (other.transform.tag == tagTokill))
		{
			//Debug.Log("meur");
			other.gameObject.Destroy();
			return;
		}
		//else
		//Debug.Log("passs" + killEVERYTHING);

	}
}