using UnityEngine;
using System.Collections;
using System;
public class AlertTextBehaviour : UIElementBehaviour<GUIManager> {
	

	//private string text;
	//private bool show;
	

	private string text = "Hallo Welt";
	private bool textShow = true;
	private  int textShowTime = 8;
	
	private int myTextWidth = 600;
	private int myTextHeight = 110;
	private float x;
	private float y;
	private TimeSpan timeSinceTextSpawn = TimeSpan.Zero;
	
	public void Awake(){
		
	}
	
	protected override void showElements(){
		if(textShow){
			timeSinceTextSpawn = timeSinceTextSpawn.Add(TimeSpan.FromSeconds((double)Time.deltaTime));
			if (timeSinceTextSpawn.TotalSeconds >= textShowTime) {
				textShow = false;
				timeSinceTextSpawn = TimeSpan.Zero;
				return;
			}
			// Hier den Text anzeigen und verschwinden lassen	
			GeneralScreenGUI.Box(guiStatics, new Rect (x,y,myTextWidth,myTextHeight), text, inactiveStyle);
		}
	}
	
	public void ShowText(string value, int seconds, Vector2 position){

		text = value;
		textShowTime = seconds;
		x = position.x;
		y = position.y;
		textShow = true;

	}
	
	public void ShowText(string value){
		ShowText(value,8,new Vector2((480-(myTextWidth/2)),(float)(320-myTextHeight/1.25)));
	}
	
	
	

}
