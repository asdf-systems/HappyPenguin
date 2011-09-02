using UnityEngine;
using System.Collections;

public class menu_background : GUIElement {
	
	
	public GUIStyle menuTop_style;
	public GUIStyle menuRight_style;
	void OnGUI(){
		GUI.depth = 100;
		GeneralScreenGUI.Box(guiManager, new Rect(448,0,512,512), "", menuTop_style);
		GeneralScreenGUI.Box(guiManager, new Rect(852,132,512,512), "", menuRight_style);
		
	}
}
