using UnityEngine;
using System.Collections;

public class TextFirst : TextPanel {

	public int position;
	private string username;
	private int points;
	

	void Start() {
		username = "";
		points = 0;
		StartCoroutine(
			HighscoreServer.GetHighscore(hs => {
				username = hs[position-1].Name;
				points = hs[position-1].Points;
				this.GetComponent<Numbers>().Points = this.points.ToString("D7");
				Text = username;
				
			})
		);
	}
	
}
