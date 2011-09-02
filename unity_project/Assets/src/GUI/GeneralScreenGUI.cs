using UnityEngine;
using System.Collections;

public class GeneralScreenGUI : GUI
{

	private Camera mCamera;
	
	public GeneralScreenGUI(){	
		
	}
	
	public static void Box(GUIManager gui, Rect rect, string text){
		UnityEngine.GUI.Box (getRelativePosition(gui,rect), text);
		
	}
	
	public static void Box(GUIManager gui, Rect rect, string text, GUIStyle style){
		UnityEngine.GUI.Box (getRelativePosition(gui,rect), text, style);
		
	}
	
	public static bool Button(GUIManager gui, Rect rect, string text){
		return UnityEngine.GUI.Button(getRelativePosition(gui,rect), text);
		
	}
	
	public static bool Button(GUIManager gui, Rect rect, string text, GUIStyle style){
		return UnityEngine.GUI.Button(getRelativePosition(gui,rect), text, style);
		
	}
	
	public static void Label(GUIManager gui, Rect rect, string text){
		UnityEngine.GUI.Label(getRelativePosition(gui,rect), text);
		
	}
	
	public static void Label(GUIManager gui, Rect rect, string text, GUIStyle style ){
		UnityEngine.GUI.Label(getRelativePosition(gui,rect), text, style);
		
	}
	
	
	public static string TextField(GUIManager gui, Rect rect, string text){
		return UnityEngine.GUI.TextField(getRelativePosition(gui,rect), text);
		
	}
	
	public static string TextField(GUIManager gui, Rect rect, string text, GUIStyle style){
		return UnityEngine.GUI.TextField(getRelativePosition(gui,rect), text, style );
		
	}
	
	public static bool Toggle(GUIManager gui, Rect rect, bool flag, string text){
		return UnityEngine.GUI.Toggle(getRelativePosition(gui,rect), flag, text);
		
	}
	
	public static bool Toggle(GUIManager gui, Rect rect, bool flag, string text, GUIStyle style){
		return UnityEngine.GUI.Toggle(getRelativePosition(gui,rect), flag, text, style );
		
	}
	
	private static Rect getRelativePosition(GUIManager gui, Rect rect){
		Camera cam = gui.PlayerCam;
		Rect camPosition = cam.pixelRect;
		// Inverse Screenposition on y because GUI (0,0) is on top camera (0,0) is on Bottom 
		if(cam.pixelHeight != Screen.height)
			camPosition.y = cam.pixelHeight - camPosition.y;
		
		// Get the right Hight and Width proportional to screen
		float factorY = (float)(Screen.height) / (float)(gui.TargetScreenHeight); 
		float factorX = (float)(Screen.width) / (float)(gui.TargetScreenWidth);
		//Debug.Log("FactorX: " + factorX + " FactorY: " + factorY);
		//Debug.Log("Screen:" + Screen.height + " " + Screen.width);
		//Debug.Log("Camera: " + cam.name + ":" + cam.transform.parent.name + " Rect: " + rect + " cam Position: " + camPosition.x + " " + camPosition.y);
		//Debug.Log("Camera: " + cam.name + ":" + cam.transform.parent.name + " CAmSize: " + cam.pixelHeight + " " + cam.pixelWidth);
		return new Rect ((camPosition.x+rect.x)*factorX  ,(camPosition.y +  rect.y)*factorY ,rect.width*factorX,rect.height*factorY);
		//return new Rect (camPosition.x ,camPosition.y ,rect.width,rect.height);
	}
}

