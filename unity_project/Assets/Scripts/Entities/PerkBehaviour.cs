using System;
using UnityEngine;
using Pux.Effects;
using System.Collections.Generic;
using Pux.Spawning;
using Pux.Entities;

namespace Pux.Entities
{
	public abstract class PerkBehaviour : TargetableEntityBehaviour
	{		
		protected override void AwakeOverride ()
		{
			base.AwakeOverride ();
			HitEffects.Add(new HideSymbolsEffect(this));
		}
	}
}