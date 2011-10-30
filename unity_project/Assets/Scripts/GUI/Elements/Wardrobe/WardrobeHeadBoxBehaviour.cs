using UnityEngine;
using System.Collections;

public sealed class WardrobeHeadBoxBehaviour : WardrobeBoxBehaviour {

	protected override void changePlayerCloth(){
		if(GameStatics.PersonalHighscore >=  points)
			GameStatics.savePlayerHat(objectName);
	}
	
	
}
