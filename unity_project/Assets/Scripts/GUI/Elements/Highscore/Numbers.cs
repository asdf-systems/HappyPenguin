using UnityEngine;
using System.Collections;

public class Numbers : UIElementBehaviour<GUIStatics> {

	public string points{
		get;
		set;
	}

	public int textureSize;
	public GUIStyle zero;
	public GUIStyle one;
	public GUIStyle two;
	public GUIStyle three;
	public GUIStyle four;
	public GUIStyle five;
	public GUIStyle six;
	public GUIStyle seven;
	public GUIStyle eight;
	public GUIStyle nine;

	// Use this for initialization
	void Start () {
		points = "";
	}

	void OnGUI(){
		GUIStyle[] numberStyles = {zero,one,two,three,four,five,six,seven,eight,nine};

		int number =0;
		int xOff = 0;
		for(int i=0; i < 7  && i < points.Length; i++){
			number = int.Parse(""+points[i]);
			GeneralScreenGUI.Box(guiStatics, new Rect(positionX+xOff,positionY,textureSize,textureSize), "", numberStyles[number]);
			xOff += textureSize - 15;
		}


	}
}
