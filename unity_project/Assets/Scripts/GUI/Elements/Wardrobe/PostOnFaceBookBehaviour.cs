using UnityEngine;
using System.Collections;

public class PostOnFaceBookBehaviour : InteractionBehaviour {
	
	private const string MESSAGE = "I dressed up my Pux!";
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
			fb.postToStream(
				MESSAGE, // Message
				imglink, // Image URL
				"", // Link?
				"Pux the Glaciator", // Title
				"Fighting for his life", // Subtitle
				"Get the game for iPhone and iPad on the AppStore.", // Description
				(success2, data) => {
					if(success2 == Facebook.REQUEST_FAIL) {
						Alert.ShowText("Post failed");
						EditorDebug.Log("Post failed: "+data);
						return;
					}
			});
			Alert.ShowText("Highscore Postet");	
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
