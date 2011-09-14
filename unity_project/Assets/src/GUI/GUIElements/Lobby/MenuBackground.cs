using UnityEngine;
using System.Collections;

public class MenuBackground : GUIBaseElement<GUIStatics> {
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,512,512), "", currentStyle);
		
	}
	
}
