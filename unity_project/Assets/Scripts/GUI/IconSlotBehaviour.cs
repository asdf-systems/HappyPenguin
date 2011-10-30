using System;
using Pux.Effects;
using UnityEngine;

public sealed class IconSlotBehaviour : UIElementBehaviour<GUIManager>
{
	public IconSlotBehaviour() {
		
	}
	
	void Awake()
	{
		Height = 35;
		Width = 35;
	}

	public void Clear() {
		inactiveStyle.normal.background = null;
		ActiveEffect = null;
	}
	
	public void DisplayEffect(Effect effect) {
		ActiveEffect = effect;
		inactiveStyle.normal.background = Resources.Load(effect.IconResourceKey) as Texture2D;
	}
	
	public Effect ActiveEffect {
		get;
		private set;
	}
	
	protected override void showElements ()
	{
		base.showElements();
		GeneralScreenGUI.Box(guiStatics, new Rect(Position.x, Position.y,Width,Height),string.Empty, inactiveStyle);
	}
	
	public bool IsOccupied {
		get { return ActiveEffect != null; }
	}
}


