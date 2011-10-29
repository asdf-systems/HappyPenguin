using UnityEngine;
using System.Collections;

public class TextFirst : UIElementBehaviour<GUIStatics> {

	public int targetTextSize;
	public int position;
	private string username;
	private int points;

	public void Start() {
		username = "";
		points = 0;
		StartCoroutine(
			HighscoreServer.GetHighscore(hs => {
				username = hs[position-1].Name;
				points = hs[position-1].Points;
			})
		);
	}

	protected override void showElements(){
		// Highscore name
		inactiveStyle.fontSize = targetTextSize;
		GeneralScreenGUI.Label(guiStatics, new Rect(positionX,positionY,128,128), username, inactiveStyle);
		this.GetComponent<Numbers>().points = points.ToString("D7");
	}



	protected override void hit(){
	}
}
