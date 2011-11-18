using UnityEngine;
using System.Collections;
using System;

public class GameEndState : MonoBehaviour {

	private float time;

	public AlertTextPanel alertElement;
	public GetNameAlert nameAlert;
	public okayButton okayButton;
	public forward_toWardrobe nextButton;
	
	private bool firstCheck = true;
	private bool timeFreeze = false;
	

	// Use this for initialization
	void Start () {
		//Debug.Log("Data Path: " + Application.persistentDataPath);
		time = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		if(firstCheck){
			
			//Debug.LogWarning("Points fix for testing!!!");	GameStatics.Points = 300;
			
			checkHighscore();
			firstCheck = false;
		}
		if(!timeFreeze)
			time+= Time.deltaTime;
		

		if(time > 10){
			Application.LoadLevel(4);
		}
	}
	
	public void usernameInputFinished(){
		timeFreeze = false;
		nextButton.showElement = true;
		int points = Convert.ToInt32(GameStatics.Points);
		addEntry(getUsername(), points);
	}
	private string getUsername(){
		string username = GameStatics.Username;
		
		if(username == string.Empty){
			nameAlert.showText = true;
			okayButton.showButton = true;
			nextButton.showElement = false;
			timeFreeze = true;
			
		} 
		return username;
		
	}
	
	private void checkHighscore(){
		StartCoroutine(
			HighscoreServer.GetHighscore(data=>{
				checkForNewHighscore(data);
			})  );
		
		string uname = getUsername();
		int points = Convert.ToInt32(GameStatics.Points);
		if(uname != string.Empty){
			addEntry(uname, points);
		}
	}
	
	
	private void checkForNewHighscore(Entry[] data){
		
	 	int position = 1;
		foreach(var e in data){
			if(e.Points < Convert.ToInt32(GameStatics.Points))
				break;
			position++;
		}
		if(position < 4){
			alertElement.ShowText("New Highscore!!\n Position: " + position); //, 8, new Vector2(alertElement.positionX, alertElement.positionY));
			GameStatics.PersonalHighscore = GameStatics.Points;
		} else{ 
			checkPersonalHighscore();
		}
		
		
		
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
