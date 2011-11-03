using System;
using UnityEngine;
using Pux.Resources;


public sealed class PlayResourceLoadingBehaviour : ResourceLoadingBehaviour
{
	public PlayResourceLoadingBehaviour() {
	}
	protected override void OnResourcesLoaded(EventArgs e) {
		base.OnResourcesLoaded(e);
		Application.LoadLevel("Arena");
	}

	#region implemented abstract members of Pux.Resources.ResourceLoadingBehaviour
	protected override void LoadResources() {
		ResourceManager.LoadResource("Creatures/Shark");
		ResourceManager.LoadResource("Creatures/Pike");
		ResourceManager.LoadResource("Creatures/Whale");
		ResourceManager.LoadResource("Creatures/Blowfish");
		
		ResourceManager.LoadResource("Environment/Balloon");
		ResourceManager.LoadResource("Environment/Snowball");
		
		ResourceManager.LoadResource("Perks/gift_black");
		ResourceManager.LoadResource("Perks/gift_std");
		ResourceManager.LoadResource("Perks/gift_blue");
		ResourceManager.LoadResource("Perks/gift_green");
		ResourceManager.LoadResource("Perks/gift_yellow");
		ResourceManager.LoadResource("Perks/gift_red");
		ResourceManager.LoadResource("Perks/gift_purple");
		
		ResourceManager.LoadResource("Symbols/TargetableSymbolC");
		ResourceManager.LoadResource("Symbols/TargetableSymbolE");
		ResourceManager.LoadResource("Symbols/TargetableSymbolY");
		ResourceManager.LoadResource("Symbols/TargetableSymbolQ");
		
		ResourceManager.LoadResource("UI/EffectIcons/fast_creature");
		ResourceManager.LoadResource("UI/EffectIcons/minus_symbol_1");
		ResourceManager.LoadResource("UI/EffectIcons/night");
		ResourceManager.LoadResource("UI/EffectIcons/point_loss_100");
		ResourceManager.LoadResource("UI/EffectIcons/points_double");
		ResourceManager.LoadResource("UI/EffectIcons/points_tripple");
		ResourceManager.LoadResource("UI/EffectIcons/rotate_buttons");
		ResourceManager.LoadResource("UI/EffectIcons/slow_creature");
		ResourceManager.LoadResource("UI/EffectIcons/snowball_speed");
		
		ResourceManager.LoadResource("iPhone/UI/arrow_green_bottom_left");
		ResourceManager.LoadResource("iPhone/UI/arrow_green_bottom_left_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_green_bottom_right");
		ResourceManager.LoadResource("iPhone/UI/arrow_green_bottom_right_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_green_top_left");
		ResourceManager.LoadResource("iPhone/UI/arrow_green_top_left_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_green_top_right");
		ResourceManager.LoadResource("iPhone/UI/arrow_green_top_right_hover");
		
		ResourceManager.LoadResource("iPhone/UI/arrow_yellow_bottom_left");
		ResourceManager.LoadResource("iPhone/UI/arrow_yellow_bottom_left_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_yellow_bottom_right");
		ResourceManager.LoadResource("iPhone/UI/arrow_yellow_bottom_right_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_yellow_top_left");
		ResourceManager.LoadResource("iPhone/UI/arrow_yellow_top_left_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_yellow_top_right");
		ResourceManager.LoadResource("iPhone/UI/arrow_yellow_top_right_hover");
		
		ResourceManager.LoadResource("iPhone/UI/arrow_red_bottom_left");
		ResourceManager.LoadResource("iPhone/UI/arrow_red_bottom_left_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_red_bottom_right");
		ResourceManager.LoadResource("iPhone/UI/arrow_red_bottom_right_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_red_top_left");
		ResourceManager.LoadResource("iPhone/UI/arrow_red_top_left_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_red_top_right");
		ResourceManager.LoadResource("iPhone/UI/arrow_red_top_right_hover");
		
		ResourceManager.LoadResource("iPhone/UI/arrow_purple_bottom_left");
		ResourceManager.LoadResource("iPhone/UI/arrow_purple_bottom_left_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_purple_bottom_right");
		ResourceManager.LoadResource("iPhone/UI/arrow_purple_bottom_right_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_purple_top_left");
		ResourceManager.LoadResource("iPhone/UI/arrow_purple_top_left_hover");
		ResourceManager.LoadResource("iPhone/UI/arrow_purple_top_right");
		ResourceManager.LoadResource("iPhone/UI/arrow_purple_top_right_hover");
	}


	protected override void UnloadResources() {
		
	}
	
	#endregion
}


