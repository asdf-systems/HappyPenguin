using UnityEngine;
using System.Collections;
using System;

public class WardrobeBoxBehaviour : UIElementBehaviour<GUIStatics> {
	
	public string objectName;
	public infoTafel infoTafel;
	public int points;
	public string infoText;
	
	public Texture2D locked;
	public Texture2D locked_active;
	
	private string currentText;
	
	public event EventHandler PlayerClothChanged;
	
	void Start(){
		init();
	}
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,128,128), "", currentStyle);
		
	}
	
	protected virtual void init(){
		if((int)GameStatics.PersonalHighscore < points){
			currentText = "you need " + points + "to unlock this items";
			inactiveStyle.normal.background = locked;
			activeStyle.normal.background = locked_active;
			hoverStyle.normal.background = locked_active;
		} else 
			currentText = infoText;
	}
	protected override void hit(){
		
		changePlayerCloth();
		InvokePlayerClothChanged();
		infoTafel.infoText = currentText;
	}
	
	
	
	protected virtual void changePlayerCloth(){
		//! overwritten in childclasses
	}
	private void InvokePlayerClothChanged(){
		var handler = PlayerClothChanged;
		if (handler == null) {
				return;
		}
		handler(this, EventArgs.Empty);
	}
}
