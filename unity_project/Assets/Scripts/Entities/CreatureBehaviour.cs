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
		AttackEffects = new List<Effect>();
		
		HitEffects.Add (new PointEffect (Points, this));
		HitEffects.Add(new RetreatEffect(this));
		
		AttackEffects.Add (new LifeEffect (-Damage));
		AttackEffects.Add(new AttackEffect(this));
		AttackEffects.Add(new RetreatEffect(this));
	}
	
	public List<Effect> AttackEffects { get; private set;}
	
	public bool IsRetreating {
		get;
		set;
	}
}