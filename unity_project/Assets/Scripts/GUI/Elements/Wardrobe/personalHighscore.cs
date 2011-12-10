using UnityEngine;
using System.Collections;
using System;

public class personalHighscore : InteractionBehaviour {
	
	
	
	private string username;
	private TextPanel[]  textPanels;
	
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
		textPanels = gameObject.GetComponents<TextPanel>() as TextPanel[];
		username = GameStatics.Username;
		Debug.LogWarning("Start Username: " + username);
		if(textPanels != null && textPanels.Length >= 2){
			textPanels[0].Text = username; 
			textPanels[1].Text = "SCORE: " + GameStatics.FormatPoints(GameStatics.PersonalHighscore);
		} else{
			EditorDebug.LogWarning("Personal Highscore need Two no TextPanels Attached, second is the name");
		}
	}
	
	public override void TextChanged(string text){
		if(textPanels != null && textPanels.Length > 1){
			Debug.LogWarning("TextChanged: " + text);
			string value = textPanels[0].Text;
			if(value.Trim() != string.Empty)
				GameStatics.Username = value;
			Debug.LogWarning("Username: " + GameStatics.Username + "XX");	
		}
		
	}
	
	/*private string extractUsername(string text){
		string tmp = string.Empty;
		for(int i=0; i < text.Length-5; i++){
			tmp += text[i];
		}
		return tmp;
	}*/
	
	
	



	
}
