using UnityEngine;
using System.Collections;

public class wardrobe_fg : UIElementBehaviour<GUIStatics> {

	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,1024,1024), "t", currentStyle);
		
	}
	
	protected override void hit(){
		
	}
}
