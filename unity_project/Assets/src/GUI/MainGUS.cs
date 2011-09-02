using UnityEngine;
using System.Collections;

public class MainGUS : GUIState {

	private int newGame_buttonWidth; 
	private int newGame_buttonHeight; 
	
	public MainGUS(GUIManager gm, Camera cam) 
		: base(gm, cam){
		
		init();
	}
	
	private void init(){
		newGame_buttonHeight = 90;
		newGame_buttonWidth = 228;
	}
	
	protected override void loadMenu(){
		
		//GUI.depth = 0;
		
		/*if (GeneralScreenGUI.Button((mGUIManager, new Rect (595,132,256,256), "", mGUIManager.newGame_btn_style)) {
			//mGUIManager.pushGUIState(new StartGUS(mGUIManager, mCamera));
		}
		GUI.depth = 1;
		if (GeneralScreenGUI.Button (mGUIManager, new Rect (639,221,256,256), "", mGUIManager.options_btn_style)) {
			//mGUIManager.pushGUIState(new StartGUS(mGUIManager, mCamera));
		}
		if (GeneralScreenGUI.Button (mGUIManager, new Rect (529,333,256,256), "", mGUIManager.highscore_btn_style)) {
			//mGUIManager.pushGUIState(new StartGUS(mGUIManager, mCamera));
		}
		if (GeneralScreenGUI.Button (mGUIManager, new Rect (602,413,256,256), "", mGUIManager.credits_btn_style)) {
			//mGUIManager.pushGUIState(new StartGUS(mGUIManager, mCamera));
		}*/
		
	}
	
	
}
