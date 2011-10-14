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
		Init ();
	}

	private void Init ()
	{
		CollectedEffects.Add (new PointEffect (Points,this));
		CollectedEffects.Add (new DeathEffect (this));
		NotCollectedEffects.Add (new LifeEffect (-Damage));
		NotCollectedEffects.Add (new AttackAnimationEffect(this));
		NotCollectedEffects.Add (new DeathEffect(this));
	}
}