using UnityEngine;
using System.Collections;

public class MenuOptionsButtonBehaviour : UIElementBehaviour<GUIStatics> {
		
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,256,256), "", currentStyle);
	}
	
	protected override void hit(){
		// start Game
		//Debug.Log("Options");
		Application.LoadLevel(4);
	}
}

