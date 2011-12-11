using UnityEngine;
using System.Collections;

public class PostOnFaceBookBehaviour : InteractionBehaviour {
	
	private string MESSAGE = "My personal best is {0} points!";
	public AlertTextPanel Alert;
	
	public override void Click(MouseEventArgs e){
		int perms = Facebook.compilePermissions(Facebook.Permission.PUBLISH_STREAM);
		var fb = Facebook.getInstance();
		fb.authorize(perms, success => {
			if(success == Facebook.LOGGED_OUT) {
				EditorDebug.Log("Login failed");
				Alert.ShowText("Login failed");
				return;
			}
			var imglink = generateImageLink();
			MESSAGE = string.Format(MESSAGE, GameStatics.PersonalHighscore);
			fb.postToStream(
				MESSAGE, // Message
				imglink, // Image URL
				"", // Link?
				"Pux the Glaciator", // Title
				"Fighting for his life", // Subtitle
				"AAvailable for iPhone and iPad. See for more Informations: http://www.asdf-systems.de", // Description
				(success2, data) => {
					if(success2 == Facebook.REQUEST_FAIL) {
						Alert.ShowText("Post failed");
						EditorDebug.Log("Post failed: "+data);
						return;
					}
			});
			Alert.ShowText("Highscore Posted");	
		});
	}
	
	private string generateImageLink() {
		string ret = "http://pux.asdf-systems.de/img/";

		ret += GameStatics.PlayerSkin;
		ret += ".";
		ret += GameStatics.getPlayerHat();
		ret += ".png";
		return ret;
	}
}
