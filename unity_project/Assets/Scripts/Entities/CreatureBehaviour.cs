using System;
using System.Collections.Generic;
using HappyPenguin.Effects;
using HappyPenguin.Entities;
using HappyPenguin;

public sealed class CreatureBehaviour : TargetableEntityBehaviour
{
	protected override void AwakeOverride()
	{
		base.AwakeOverride();
		AttackEffects = new List<Effect>();
		KillEffects = new List<Effect>();
	}

	public IEnumerable<Effect> AttackEffects { get; private set; }

	public IEnumerable<Effect> KillEffects { get; private set; }
}


