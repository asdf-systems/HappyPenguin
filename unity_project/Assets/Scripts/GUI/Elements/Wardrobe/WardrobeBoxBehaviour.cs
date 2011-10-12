using UnityEngine;
using System.Collections;
using System;

public class WardrobeBoxBehaviour : UIElementBehaviour<GUIStatics> {
	
	public string objectName;
	
	public event EventHandler PlayerClothChanged;
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,128,128), "", currentStyle);
		
	}
	
	protected override void hit(){
		Debug.Log("Button Hit" + DateTime.Now.ToString("T"));
		Debug.Log("Object Name: " + objectName);
		changePlayerCloth();
		InvokePlayerClothChanged();
		
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
