using UnityEngine;
using System.Collections;

public class menu_background : GUIElement {
	
	
	/*void OnGUI(){
		GUI.depth = 100;
		GeneralScreenGUI.Box(guiManager, new Rect(448,0,512,512), "", currentStyle);
		GeneralScreenGUI.Box(guiManager, new Rect(852,132,512,512), "", currentStyle);
		
	}*/
	

	protected override void showElements(){
		GeneralScreenGUI.Box(guiManager, new Rect(448,0,512,512), "", currentStyle);
		
	}
	protected override void hit(){
		
	}
}
