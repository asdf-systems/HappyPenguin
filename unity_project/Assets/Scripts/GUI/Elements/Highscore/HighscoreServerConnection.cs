using UnityEngine;
using System.Collections;

public class HighscoreServerConnection : MonoBehaviour {

	public string getNameFromHighscoreList(int place){
		// TODO get actula name and points from HighscoreList
		
		// THIS IS A DUMMY
		return "the master tester";
	}
	
	public string getpointsFromHighscoreList(int place){
		// TODO get actula name and points from HighscoreList
		
		// THIS IS A DUMMY 
		// Number has to be filled with 00 up to seven digits
		return "0000789";
	}
	
	public void postNewHighscore(string name, int points){
		
	}

}
