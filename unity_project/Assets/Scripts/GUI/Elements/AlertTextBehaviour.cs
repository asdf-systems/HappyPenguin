using UnityEngine;
using System.Collections;
using System;
public class AlertTextBehaviour : UIElementBehaviour<GUIManager> {
	

	//private string text;
	//private bool show;
	

	private string text = "Hallo Welt";
	private bool textShow = true;

	
	private int myTextWidth = 600;
	private int myTextHeight = 90;
	private TimeSpan timeSinceTextSpawn = TimeSpan.Zero;
	
	protected override void showElements(){
		if(textShow){
			timeSinceTextSpawn = timeSinceTextSpawn.Add(TimeSpan.FromSeconds((double)Time.deltaTime));
			if (timeSinceTextSpawn.TotalSeconds >= 8) {
				textShow = false;
				timeSinceTextSpawn = TimeSpan.Zero;
				return;
			}
			// Hier den Text anzeigen und verschwinden lassen	
			GeneralScreenGUI.Box(guiStatics, new Rect (480-myTextWidth/2,(float)(320-myTextHeight/1.25),myTextWidth,myTextHeight), text, inactiveStyle);
		}
	}
	
	public void showText(string value){

		text = value;
		textShow = true;

	}
	
	

}
