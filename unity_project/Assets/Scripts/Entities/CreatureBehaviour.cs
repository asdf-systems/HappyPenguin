using System;
using System.Collections.Generic;
using HappyPenguin.Effects;
using HappyPenguin.Entities;
using HappyPenguin;

public sealed class CreatureBehaviour : TargetableEntityBehaviour
{

	public float Points;
	public float Damage;

	protected override void AwakeOverride ()
	{
		base.AwakeOverride ();
		AttackEffects = new List<Effect> ();
		KillEffects = new List<Effect> ();
		Init ();
	}

	private void Init ()
	{
		KillEffects.Add (new PointEffect (Points));
		KillEffects.Add (new DeathEffect (this));
		AttackEffects.Add (new LifeEffect (-Damage));
	}

	public List<Effect> AttackEffects { get; private set; }
	
}


