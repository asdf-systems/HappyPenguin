using UnityEngine;
using System.Collections;

public class toMainMenu : UIElementBehaviour<GUIStatics> {

	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,128,128), "to main", currentStyle);
		
	}
	
	protected override void hit(){
		Application.LoadLevel(0);
	}
}
