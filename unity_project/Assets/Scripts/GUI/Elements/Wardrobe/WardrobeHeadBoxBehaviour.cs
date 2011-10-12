using UnityEngine;
using System.Collections;

public sealed class WardrobeHeadBoxBehaviour : WardrobeBoxBehaviour {

	protected override void changePlayerCloth(){
		GameStatics.savePlayerHat(objectName);
	}
}
