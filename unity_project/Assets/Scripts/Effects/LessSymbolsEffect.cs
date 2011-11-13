using System;

namespace Pux.Effects
{
	public sealed class LessSymbolsEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			world.SymbolRangeModifer = new Range(0, -1);
		}
		
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.SymbolRangeModifer = new Range(0, -1);
		}
		
		#endregion
		public LessSymbolsEffect() {
			Duration = TimeSpan.FromSeconds(10);
			IconResourceKey = "UI/EffectIcons/minus_symbol_1";
		}
	}
}

