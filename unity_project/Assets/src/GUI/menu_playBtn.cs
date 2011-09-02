using UnityEngine;
using System.Collections;

public class menu_playBtn : GUIElement {
	
	public GUIStyle play_btn_style;
	
	void OnGUI(){
		if(GeneralScreenGUI.Button(mGUIManager, new Rect (595,132,256,256), "", play_btn_style)){
			//mGUIManager.pushGUIState(new StartGUS(mGUIManager, mCamera));
		}
	}
}
