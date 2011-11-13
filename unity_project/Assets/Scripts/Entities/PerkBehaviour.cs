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
}
