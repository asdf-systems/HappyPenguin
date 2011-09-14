using UnityEngine;
using System.Collections;

public class MenuQ: GUIBaseElement<GUIManager>{
		
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect (positionX,positionY,128,128), "", currentStyle);
	}
	
	protected override void hit(){
		guiStatics.buttonQHit();
		

	}
}
