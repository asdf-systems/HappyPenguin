using System;
using Pux.Effects;
using UnityEngine;

public sealed class IconSlotBehaviour : Control
{
	private Rect emptyUV = new Rect(0,0,1,1);
	public IconSlotBehaviour() {
		
	}
	
	
	public void Clear() {
		Uv = emptyUV;
		ActiveEffect = null;
	}
	
	public void DisplayEffect(Effect effect) {
		ActiveEffect = effect;
		Uv = effect.IconResourceUV;
		UpdateElement();
	}
	
	public Effect ActiveEffect {
		get;
		private set;
	}
	
	
	public bool IsOccupied {
		get { return ActiveEffect != null; }
	}
}


