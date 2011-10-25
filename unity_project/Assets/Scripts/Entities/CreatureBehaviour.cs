using System;
using System.Collections.Generic;
using Pux.Effects;
using Pux.Entities;
using Pux;

public sealed class CreatureBehaviour : TargetableEntityBehaviour
{
	private Random random;
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
		
		random = new Random();
	}
	
	public List<Effect> AttackEffects { get; private set;}
	
	public bool IsRetreating {
		get;
		set;
	}
	
	public void EquipWithRandomBaddy()
	{
		var value = random.Next(0, 100);
		if (value < 100) {
			AttackEffects.Add(new UIRotationEffect());
		}
	}
}