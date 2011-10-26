using UnityEngine;
using System.Collections;

public class HighscoreBg: UIElementBehaviour<GUIStatics> {
	
	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,960,960), "", inactiveStyle);
		
	}
	
}
