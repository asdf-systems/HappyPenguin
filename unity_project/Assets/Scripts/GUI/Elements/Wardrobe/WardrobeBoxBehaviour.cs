using UnityEngine;
using System.Collections;

public class WardrobeBoxBehaviour : UIElementBehaviour<GUIStatics> {
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,128,128), "", currentStyle);
		
	}
	
}
