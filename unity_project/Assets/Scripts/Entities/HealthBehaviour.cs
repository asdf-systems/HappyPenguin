using System;
using UnityEngine;
using HappyPenguin.Effects;
using System.Collections.Generic;
using HappyPenguin.Spawning;
using HappyPenguin.Entities;


public sealed class HealthBehaviour : PerkBehaviour
{
	public float LifeGain = 1;
	
	protected override void AwakeOverride ()
	{
		base.AwakeOverride ();
		Init ();
	}

	private void Init ()
	{
		CollectedEffects.Add (new HealthKillEffect (this , LifeGain));
		NotCollectedEffects.Add (new PerkVanishEffect (this));
	}
	
}