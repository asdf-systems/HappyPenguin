using System;
using UnityEngine;
using Pux.Entities;

public sealed class ObjectDisposalBehaviour : MonoBehaviour
{
	void OnTriggerEnter(Collider c){
		if(c == null)
			return;
		var targetable = c.GetComponentInChildren<TargetableEntityBehaviour>();
		if(targetable == null)
			return;
		targetable.Dispose();
	}
}


