using UnityEngine;
using System.Collections;

public class AlertTextBehaviour : UIElementBehaviour<GUIManager> {
	
	private string text;
	private bool show;
	
	protected override void showElements(){
		if(show){
			// Hier den Text anzeigen und verschwinden lassen	
		}
	}
	
	public void showText(string value){
		text = value;
		show = true;
	}
	
	

}
