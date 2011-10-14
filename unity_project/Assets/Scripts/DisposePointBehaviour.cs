using System;
using UnityEngine;
using HappyPenguin.Entities;

public sealed class DisposePointBehaviour : MonoBehaviour
{
	void OnTriggerEnter(Collider c){
		var perk = c.GetComponent<TargetableEntityBehaviour>();
		perk.Dispose();
	}
}


