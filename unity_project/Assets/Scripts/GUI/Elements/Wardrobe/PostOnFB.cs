using UnityEngine;
using System.Collections;

public class PostOnFB : UIElementBehaviour<GUIStatics> {
	private const string MESSAGE = "I dressed up my Pux!";

	protected override void showElements(){
		GeneralScreenGUI.Box(guiStatics, new Rect(positionX,positionY,128,128), "Post on Facebook", currentStyle);

	}

	protected override void hit(){
		int perms = Facebook.compilePermissions(Facebook.Permission.PUBLISH_STREAM);
		var fb = Facebook.getInstance();
		fb.authorize(perms, success => {
			if(success == Facebook.LOGGED_OUT) {
				Debug.Log("Login failed");
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
						Debug.Log("Post failed");
						return;
					}
			});
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
