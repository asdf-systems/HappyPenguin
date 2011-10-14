using System;
using UnityEngine;
using HappyPenguin.Entities;

public sealed class DisposePointBehaviour : MonoBehaviour
{
	void OnTriggerEnter(Collider c){
		var targetable = c.GetComponent<TargetableEntityBehaviour>();
		targetable.Dispose();
	}
}


