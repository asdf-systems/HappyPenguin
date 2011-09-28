using System;
using UnityEngine;
using HappyPenguin.Effects;
using System.Collections.Generic;
using HappyPenguin.Entities;


public sealed class PerkBehaviour : TargetableEntityBehaviour
{
	//public List<Effect> MoveEffect{get; private set;}
	
	protected override void AwakeOverride ()
	{
		base.AwakeOverride ();
		KillEffects = new List<Effect> ();
		//MoveEffect = new List<Effect>();
		Init ();
	}

	private void Init ()
	{
		//MoveEffect.Add(new PerkMoveEffect(this,new Vector3(0,200,0)));
		KillEffects.Add (new PerkKillEffect (this));
	}
	
}


