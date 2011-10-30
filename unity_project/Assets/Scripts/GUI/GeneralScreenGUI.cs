using UnityEngine;
using System.Collections;

public class GeneralScreenGUI : GUI
{
	private Camera mCamera;

	public GeneralScreenGUI() {
		
	}
	
	public static void Box(GUIStatics gui, Rect rect, string text) {
		UnityEngine.GUI.Box(GetRelativePosition(gui, rect), text);
	}

	public static void Box(GUIStatics gui, Rect rect, string text, GUIStyle style) {
		UnityEngine.GUI.Box(GetRelativePosition(gui, rect), text, style);
	}

	public static bool Button(GUIStatics gui, Rect rect, string text) {
		return UnityEngine.GUI.Button(GetRelativePosition(gui, rect), text);
	}

	public static bool Button(GUIStatics gui, Rect rect, string text, GUIStyle style) {
		return UnityEngine.GUI.Button(GetRelativePosition(gui, rect), text, style);
	}

	public static void Label(GUIStatics gui, Rect rect, string text) {
		UnityEngine.GUI.Label(GetRelativePosition(gui, rect), text);	
	}

	public static void Label(GUIStatics gui, Rect rect, string text, GUIStyle style) {
		int size = style.fontSize;
		style.fontSize = GetRelativeFontSize(gui, size);
		UnityEngine.GUI.Label(GetRelativePosition(gui, rect), text, style);
	}


<<<<<<< HEAD
	public static string TextField(GUIStatics gui, Rect rect, string text) {
		return UnityEngine.GUI.TextField(GetRelativePosition(gui, rect), text);
=======
	public static string TextField(GUIStatics gui, Rect rect, string text, int signCount) {
		
		return UnityEngine.GUI.TextField(GetRelativePosition(gui, rect), text,signCount);
		
>>>>>>> origin/feature/wardrobeCleanup
	}

	public static string TextField(GUIStatics gui, Rect rect, string text,int signCount,  GUIStyle style) {
		int size = style.fontSize;
		
		style.fontSize = GetRelativeFontSize(gui, size);
		
		return UnityEngine.GUI.TextField(GetRelativePosition(gui, rect), text, signCount, style);
		
	}

	public static bool Toggle(GUIStatics gui, Rect rect, bool flag, string text) {
		return UnityEngine.GUI.Toggle(GetRelativePosition(gui, rect), flag, text);
	}

	public static bool Toggle(GUIStatics gui, Rect rect, bool flag, string text, GUIStyle style) {
		return UnityEngine.GUI.Toggle(GetRelativePosition(gui, rect), flag, text, style);
	}

	public static Vector2 GetCenter() {
		return new Vector2(Screen.width / 2, Screen.height / 2);
	}

	private static int GetRelativeFontSize(GUIStatics gui, int size) {
		Vector2 factor = GetFactor(gui);
		return (int)(size * factor.y);
	}
	
	private static Vector2 GetFactor(GUIStatics gui) {
		// Get the right Hight and Width proportional to screen
		float factorY = (float)(Screen.height) / (float)(gui.TargetScreenHeight);
		float factorX = (float)(Screen.width) / (float)(gui.TargetScreenWidth);
		return new Vector2(factorX, factorY);
	}

	private static Rect GetRelativePosition(GUIStatics gui, Rect rect) {
		Camera cam = gui.PlayerCam;
		Rect camPosition = cam.pixelRect;
		// Inverse Screenposition on y because GUI (0,0) is on top camera (0,0) is on Bottom 
		if (cam.pixelHeight != Screen.height)
			camPosition.y = cam.pixelHeight - camPosition.y;
		
		Vector2 factor = GetFactor(gui);
		return new Rect((camPosition.x + rect.x) * factor.x, (camPosition.y + rect.y) * factor.y, rect.width * factor.x, rect.height * factor.y);
	}

	public static Vector3 NormalizeMouse(GUIStatics gui, Vector3 vec) {
		float factorY = (float)(Screen.height) / (float)(gui.TargetScreenHeight);
		float factorX = (float)(Screen.width) / (float)(gui.TargetScreenWidth);
		//Debug.Log("Normalize: " + vec.y + " "+ Screen.height + "Factor: " + factorX + " " + factorY);
		vec.y = Screen.height - vec.y;
		vec.x /= factorX;
		vec.y /= factorY;
		return vec;
	}
}

