using System;
using UnityEngine;
using Pux.Effects;
using System.Collections.Generic;
using Pux.Spawning;
using Pux.Entities;


public sealed class HealthBehaviour : PerkBehaviour
{
	public float LifeGain = 1;
	
	protected override void AwakeOverride ()
	{
		base.AwakeOverride ();
		HitEffects.Add (new LifeEffect(LifeGain));
	}
}