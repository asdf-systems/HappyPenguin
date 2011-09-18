using UnityEngine;
using System.Collections;

public class AlertTextBehaviour : UIElementBehaviour<GUIManager> {
	
	//private string text;
	private bool show;
	
	
	
	protected override void showElements(){
		if(show){
			// Hier den Text anzeigen und verschwinden lassen	
			//textElement = GeneralScreenGUI.Text(guiStatics, new Rect (positionX,positionY,128,128), "", currentStyle);
		}
	}
	
	public void showText(string value){
		//text = value; 
		show = true;
	}
	
	

}
