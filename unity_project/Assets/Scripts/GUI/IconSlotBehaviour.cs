using System;
using Pux.Effects;
using UnityEngine;

public sealed class IconSlotBehaviour : Panel
{
	public IconSlotBehaviour() {
		
	}
	
	void Awake(){
	}

	public void Clear() {
		EditorDebug.LogWarning("Clear IconSlot need to be rewritten for new GUI System");
		//inactiveStyle.normal.background = null;
		ActiveEffect = null;
	}
	
	public void DisplayEffect(Effect effect) {
		ActiveEffect = effect;
		EditorDebug.LogWarning("Disolay Effects need to be rewritten for new GUI System");
		//inactiveStyle.normal.background = Resources.Load(effect.IconResourceKey) as Texture2D;
	}
	
	public Effect ActiveEffect {
		get;
		private set;
	}
	
	
	public bool IsOccupied {
		get { return ActiveEffect != null; }
	}
}


