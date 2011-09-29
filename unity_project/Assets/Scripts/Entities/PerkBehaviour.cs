using System;
using UnityEngine;
using HappyPenguin.Effects;
using System.Collections.Generic;
using HappyPenguin.Spawning;
using HappyPenguin.Entities;


public sealed class PerkBehaviour : TargetableEntityBehaviour
{

	
	protected override void AwakeOverride ()
	{
		base.AwakeOverride ();
		KillEffects = new List<Effect> ();
		//MoveEffect = new List<Effect>();
		Init ();
	}

	private void Init ()
	{
		KillEffects.Add (new PerkKillEffect (this));
	}
	
}


