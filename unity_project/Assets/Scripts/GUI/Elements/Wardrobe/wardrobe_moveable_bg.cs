using UnityEngine;
using System.Collections;

public class wardrobe_moveable_bg : UIElementBehaviour<GUIStatics> {

	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,1024,1024), "", currentStyle);
		
	}
	
	protected override void hit(){

	}
}
