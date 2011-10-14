using System;
using UnityEngine;
using HappyPenguin.Effects;
using System.Collections.Generic;
using HappyPenguin.Spawning;
using HappyPenguin.Entities;


public sealed class NukeBehaviour : PerkBehaviour
{
	protected override void AwakeOverride ()
	{
		base.AwakeOverride ();
		HitEffects.Add (new NukeEffect (this));
	}
}
