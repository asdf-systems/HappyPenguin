using UnityEngine;
using System.Collections;
using System;
public class AlertTextBehaviour : UIElementBehaviour<GUIManager> {
	
	private string text = "Let's Go!";
	private bool textShow = true;
	private  int textShowTime = 8;
	
	private int myTextWidth = 250;
	private int myTextHeight = 100;
	public int myPlateSize = 400;
	public int targetFontSize = 50;
	private float x = 960/2;
	private float y = 640/2;
	private TimeSpan timeSinceTextSpawn = TimeSpan.Zero;
	

	public GUIStyle textStyle;
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
			textStyle.fontSize = targetFontSize;
			
			GeneralScreenGUI.Box(guiStatics, new Rect (x - myPlateSize/2,y - myPlateSize/2,myPlateSize,myPlateSize), "", inactiveStyle);
			GeneralScreenGUI.Label(guiStatics, new Rect (x - myTextWidth/2,y - myTextHeight/2+10,myTextWidth,myTextHeight), text, textStyle);
			
		}
	}
	
	public void ShowText(string value, int seconds, Vector2 position){

		timeSinceTextSpawn = TimeSpan.Zero;
		text = value;
		textShowTime = seconds;
		x = position.x;
		y = position.y;
		textShow = true;

	}
	
	public void ShowText(string value){
		ShowText(value,8,new Vector2(x,y));
	}
}
