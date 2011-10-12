using UnityEngine;
using System.Collections;

public class toWardrobe : UIElementBehaviour<GUIStatics> {

	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,128,128), "to main Wardrobe", currentStyle);
		
	}
	
	protected override void hit(){
		Debug.Log("Change to main Menu");
		Application.LoadLevel(4);
	}
}
