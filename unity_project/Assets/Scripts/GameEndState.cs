using UnityEngine;
using System.Collections;
using System;

public class GameEndState : MonoBehaviour {

	private float time;
	public AlertTextBehaviour alertElement;
	public GetNameAlert nameAlert;
	public okayButton okayButton;
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
			
			//Debug.LogWarning("Points fix for testing!!!");
			//	GameStatics.Points = 50;
			
			checkHighscore();
			firstCheck = false;
			
		}
		if(!timeFreeze)
			time+= Time.deltaTime;
		
		if(time > 10){
			Application.LoadLevel(0);
		}
	}
	
	public void usernameInputFinished(){
		timeFreeze = false;
		int points = Convert.ToInt32(GameStatics.Points);
		addEntry(getUsername(), points);
	}
	private string getUsername(){
		string username = GameStatics.username;
		
		if(username == string.Empty){
			nameAlert.showText = true;
			okayButton.showButton = true;
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
			writeNewPersonalHighscore(FormatPoints(GameStatics.Points));
		} else{ 
			checkPersonalHighscore();
		}
		
		
		
	}
	
	private void checkPersonalHighscore(){
		string spoints = "0000000";
		try{
			spoints = LocalStorage.ReadUTF8File("personal_highscore");
			if( Convert.ToInt32(spoints) < GameStatics.Points ) {
				alertNewPersonalHighscore(FormatPoints(GameStatics.Points));
			}
			
		}catch(Exception e){
			Debug.LogWarning(e.Message);
			alertNewPersonalHighscore(FormatPoints(GameStatics.Points));
			
		}
	}
	
	private string FormatPoints(float points){
		int iPoints = (int)points;
		string sPoints = iPoints.ToString();
		while(sPoints.Length < 7)
			sPoints = "0" + sPoints;
		return sPoints;
		
	}
	private void alertNewPersonalHighscore(string spoints){
		alertElement.ShowText("New Personal Highscore!\n"+GameStatics.Points);
		writeNewPersonalHighscore(spoints);
	}
	
	private void writeNewPersonalHighscore(string spoints){
		try{
			LocalStorage.WriteUTF8File("personal_highscore", spoints);
		}catch(Exception e){
				Debug.LogWarning(e.Message);
		}
	}
	private void addEntry(string name, int points) {
		
		StartCoroutine(
			HighscoreServer.AddEntry(name, points)
		);
		
	}
}
