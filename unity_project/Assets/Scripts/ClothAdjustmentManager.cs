using System;
using System.Collections.Generic;

namespace Pux
{
	public static class ClothAdjustmentManager
	{
		private static readonly Dictionary<string, Action<GameWorldBehaviour>> _adjustments;
		
		static ClothAdjustmentManager() {
			_adjustments = new Dictionary<string, Action<GameWorldBehaviour>>();
			_adjustments.Add("pux_hexer_skin", (x) => { x.CreatureSpeedModifier = 0.9f; });
			_adjustments.Add("pux_kenny_skin", (x) => { x.MaxLife = 6; });
			_adjustments.Add("pux_normal_skin", (x) => { });
			_adjustments.Add("pux_priester_skin", (x) => {
				x.PositiveEffectDurationModifier = 1.2f;
				x.PerkSpawnTimeModifier = 0.9f;
			});
			_adjustments.Add("Cowboy_Hat", (x) => { x.SnowballSpeedModifier = 1.3f; });
			_adjustments.Add("Hexer_Cap", (x) => {x.NegativeEffectDurationModifier = 0.9f; });
			_adjustments.Add("Kenny_Hat", (x) => { });
			_adjustments.Add("None_Hat", (x) => { });
			_adjustments.Add("Red_Hat", (x) => { });
			_adjustments.Add("Papst_Hat", (x) => { });
		}
		
		public static void ApplyAdjustments(GameWorldBehaviour world){
			var hat = GameStatics.getPlayerHat();
			ApplyEffect(hat, world);
			var skin = GameStatics.PlayerSkin;
			ApplyEffect(skin, world);
		}
		
		private static void ApplyEffect(string clothName, GameWorldBehaviour behaviour){
			if (!_adjustments.ContainsKey(clothName)) {
				var message = string.Format("unknown cloth name '0}'", clothName);
				throw new ApplicationException(message);
			}	
			_adjustments[clothName](behaviour);
		}
	}
}

