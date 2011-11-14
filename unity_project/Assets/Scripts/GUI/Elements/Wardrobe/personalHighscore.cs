using UnityEngine;
using System.Collections;
using System;

public class personalHighscore : InteractionBehaviour {
	
	
	
	private string username;
	
	//protected override void showElements(){
		/*textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,350,350), "", inactiveStyle);

		string oldname = username;
		username = GeneralScreenGUI.TextField(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY,350,350), username,10, textStyle);
		textStyle.fontSize = targetTextSize;
		if(username != oldname)	
			GameStatics.username = username;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX+170,positionY+textOffsetY,350,350), "Score", textStyle);
		textStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX+textOffsetX,positionY+textOffsetY+targetTextSize,300,300), GameStatics.FormatPoints(GameStatics.PersonalHighscore), textStyle);*/
		

	//}
	
	void Start(){
		username = GameStatics.username;
		if(username == string.Empty)
			username = "Your name";
		TextPanel[] text = gameObject.GetComponents<TextPanel>() as TextPanel[];
		if(text != null && text.Length >= 2){
			text[0].Text = username; 
			text[1].Text = "               SCORE\n " + GameStatics.FormatPoints(GameStatics.PersonalHighscore);
		} else{
			Debug.LogWarning("Personal Highscore need Two no TextPanels Attached, second is the name");
		}
	}
	
	public override void TextChanged(string text){
		if(!text.StartsWith("         "))
			GameStatics.username = text;
	}
	
	private string extractUsername(string text){
		string tmp = string.Empty;
		for(int i=0; i < text.Length-5; i++){
			tmp += text[i];
		}
		return tmp;
	}
	
	
	



	
}
