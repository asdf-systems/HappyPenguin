using System;
using UnityEngine;
using Pux.Entities;

public sealed class DisposePointBehaviour : MonoBehaviour
{
	void OnTriggerEnter(Collider c){
		var targetable = c.GetComponentInChildren<TargetableEntityBehaviour>();
		targetable.Dispose();
	}
}


