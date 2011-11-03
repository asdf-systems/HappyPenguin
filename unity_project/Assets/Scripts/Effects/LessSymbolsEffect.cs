using System;

namespace Pux.Effects
{
	public sealed class LessSymbolsEffect : Effect
	{
		#region implemented abstract members of Pux.Effects.Effect
		public override void Start (GameWorldBehaviour world)
		{
			world.SymbolCountModifier = -1;
		}
		
		
		public override void Update (GameWorldBehaviour world)
		{
			// nada
		}
		
		
		public override void Stop (GameWorldBehaviour world)
		{
			world.SymbolCountModifier = 0;
		}
		
		#endregion
		public LessSymbolsEffect() {
			Duration = TimeSpan.FromSeconds(6);
			IconResourceKey = "UI/EffectIcons/minus_symbol_1";
		}
	}
}
