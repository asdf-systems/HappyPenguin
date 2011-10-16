using UnityEngine;
using System.Collections;
using HappyPenguin;
using HappyPenguin.Entities;
using System;

public sealed class AttackZoneBehaviour : MonoBehaviour {

	public event EventHandler<BehaviourEventArgs<CreatureBehaviour>> AttackZoneEntered;
	
	void OnTriggerEnter(Collider c){
		var creature = c.GetComponent<CreatureBehaviour>();
		if (creature == null) {
			return;
		}
		if (creature.IsRetreating) {
			return;
		}
		InvokeAttackZoneEntered(creature);
	}
	
	private void InvokeAttackZoneEntered(CreatureBehaviour creature){
		var handler = AttackZoneEntered;
		if (handler == null) {
				return;
		}
		var e = new BehaviourEventArgs<CreatureBehaviour>(creature);
		AttackZoneEntered(this, e);
	}
}
