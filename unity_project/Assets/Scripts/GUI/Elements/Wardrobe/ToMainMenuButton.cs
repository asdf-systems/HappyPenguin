using UnityEngine;
using System.Collections;

public class ToMainMenuButton : UIElementBehaviour<GUIStatics> {

	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,128,128), "to main Menu", currentStyle);
		
	}
	
	protected override void hit(){
		Debug.Log("Change to main Menu");
		Application.LoadLevel(0);
	}
}
