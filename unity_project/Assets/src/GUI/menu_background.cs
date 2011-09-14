using UnityEngine;
using System.Collections;

public class menu_background : GUIBaseElement {
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,512,512), "", currentStyle);
		
	}
	
}
