using UnityEngine;
using System.Collections;
using System;

public class PostOnFB : UIElementBehaviour<GUIStatics> {
	private const string MESSAGE = "I dressed up my Pux!";
	public AlertTextBehaviour alert; 
	
	protected override void showElements(){
		//alert.ShowText("Highscore Postets - test");
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,512,512), "", currentStyle);
	}

	protected override void hit(){
		int perms = Facebook.compilePermissions(Facebook.Permission.PUBLISH_STREAM);
		var fb = Facebook.getInstance();
		fb.authorize(perms, success => {
			if(success == Facebook.LOGGED_OUT) {
				Debug.Log("Login failed");
				alert.ShowText("Login failed");
				return;
			}
			var imglink = generateImageLink();
			fb.postToStream(
				MESSAGE, // Message
				imglink, // Image URL
				"", // Link?
				"Pux the Glaciator", // Title
				"Fighting for his life", // Subtitle
				"Get the game for iPhone and iPad on the AppStore.", // Description
				(success2, data) => {
					if(success2 == Facebook.REQUEST_FAIL) {
						alert.ShowText("Post failed");
						Debug.Log("Post failed: "+data);
						return;
					}
			});
			alert.ShowText("Highscore Postet");	
		});
		
	}

	private string generateImageLink() {
		string ret = "http://glaciator.asdf-systems.de/penguins/";

		ret += GameStatics.getPlayerHat();
		ret += ".";
		ret += "none"; //GameStatics.getPlayerBelt()
		ret += ".";
		ret += "none"; //GameStatics.getPlayerShoes()
		ret += ".png";
		return ret;
	}
}
