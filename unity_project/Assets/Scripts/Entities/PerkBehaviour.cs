using System;
using UnityEngine;
using Pux.Effects;
using System.Collections.Generic;
using Pux.Spawning;
using Pux.Entities;

public sealed class PerkBehaviour : TargetableEntityBehaviour
{
	protected override void AwakeOverride() {
		base.AwakeOverride();
		HitEffects.Add(new SinkEffect(this));
	}
	
	public void SetMaterial(PerkTypes type){
		var component = gameObject.GetComponent<UVMoveBehaviour>();
		if (component == null) {
			throw new MissingComponentException("UVMoveBehaviour not found");
		}
		
		switch (type) {
		case PerkTypes.Health: // Red
				component.newUvs = new Rect(128, 0, 1, 1);
			break;
		case PerkTypes.IncreasedBallSpeed: // yellow
				component.newUvs = new Rect(0, 0, 1, 1);
			break;
		case PerkTypes.TripplePoints:
				component.newUvs = new Rect(0, -128, 1, 1);
			break;
		case PerkTypes.DoublePoints:
				component.newUvs = new Rect(0,-256, 1, 1);
			break;
		case PerkTypes.CreatureSlowdown:
				component.newUvs = new Rect(128, -256, 1, 1);
			break;
		case PerkTypes.LessSymbols:
				component.newUvs = new Rect(128, -128, 1, 1);
			break;
		}
	}
}
