using UnityEngine;
using System.Collections;

public class menu_background : GUIBaseElement {
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiManager, new Rect(positionX,positionY,512,512), "", currentStyle);
		
	}
	
}
