using UnityEngine;
using System.Collections;
using System;

public class GameEndState : MonoBehaviour {
	public AlertTextPanel alertElement;
	public GetNameAlert nameAlert;
	public okayButton okayButton;
	public Button nextButton;

	private bool firstCheck = true;

	// Use this for initialization
	void Start () {
		EditorDebug.LogWarning("Points: " + GameStatics.Points);
		okayButton.Visibility = false;
	}

	// Update is called once per frame
	void Update () {
		if(firstCheck){
			checkHighscore();
			firstCheck = false;
		}
	}

	public void usernameInputFinished(){
		nextButton.Visibility = true;
		int points = Convert.ToInt32(GameStatics.Points);
		string username = GameStatics.Username;
		addEntry(username, points);
	}
	private void getUsernameFromUser(){
		string username = GameStatics.Username;
		EditorDebug.LogError("Username: " + username);
		nameAlert.ShowText(username);
		okayButton.Visibility = true;
	}

	private void checkHighscore(){
		checkPersonalHighscore();
		StartCoroutine(
			HighscoreServer.GetHighscore((data, success) => {
				if(success) {
					checkForNewHighscore(data);
				}
			}));
	}

	private void checkForNewHighscore(Entry[] data){

	 	int position = 1;
		foreach(var e in data){
			if(e.Points < Convert.ToInt32(GameStatics.Points))
				break;
			position++;
		}
		if(position < 4){
			nextButton.Visibility = false;
			alertElement.ShowText("New Highscore!!\n Position: " + position); //, 8, new Vector2(alertElement.positionX, alertElement.positionY));
			GameStatics.PersonalHighscore = GameStatics.Points;
			alertElement.timer1.TimerFinished += OnHighscoreTimerFinished;

		}


	}

	void OnHighscoreTimerFinished(object sender, EventArgs e){
		getUsernameFromUser();
	}

	private void checkPersonalHighscore(){

		if( GameStatics.PersonalHighscore < GameStatics.Points){
			alertNewPersonalHighscore();
		}

	}

	private void alertNewPersonalHighscore(){
		alertElement.ShowText("New Personal Highscore!\n"+GameStatics.Points);
		GameStatics.PersonalHighscore = GameStatics.Points;
	}

	private void addEntry(string name, int points) {

		StartCoroutine(
			HighscoreServer.AddEntry(name, points)
		);
	}
}
