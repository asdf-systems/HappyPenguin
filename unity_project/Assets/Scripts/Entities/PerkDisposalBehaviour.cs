using UnityEngine;
using System.Collections;
using Pux.Entities;

public sealed class PerkDisposalBehaviour : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		var targetable = c.GetComponentInChildren<TargetableEntityBehaviour>();
		if (targetable is PerkBehaviour) {
			targetable.Dispose();	
		}
	}
}
