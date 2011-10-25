using System;
using UnityEngine;
using Pux.Effects;
using System.Collections.Generic;
using Pux.Spawning;
using Pux.Entities;


public sealed class NukeBehaviour : PerkBehaviour
{
	protected override void AwakeOverride ()
	{
		base.AwakeOverride ();
		HitEffects.Add (new NukeEffect (this));
	}
}
