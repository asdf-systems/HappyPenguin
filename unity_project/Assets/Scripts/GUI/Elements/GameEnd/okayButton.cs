using UnityEngine;
using System.Collections;

public class okayButton : UIElementBehaviour<GUIStatics> {
	public GameEndState gameState;
	public GetNameAlert alert;
	public bool showButton{get;set;}
	
	protected override void showElements(){
		if(showButton)
			GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,128,128), "", inactiveStyle);
	
	}
	
	void Start(){
		showButton = false;
	}
	protected override void hit(){
		alert.showText = false;
		gameState.usernameInputFinished();
		showButton = false;
		
	}
}
