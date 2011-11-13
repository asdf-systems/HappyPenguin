using UnityEngine;
using System.Collections;

public class TextPanel : Panel {

	public int targetFontSize;
	public int textOffsetX;
	public int textOffsetY;
	public string Text;
	public GUIStyle textStyle;
	
	private Rect textRegion;
	private Rect realTextRegion;
	
	protected override void AwakeOverride(){
		base.AwakeOverride();
		initTextRegion();
	}
	
	void Start(){
		
	}
	
	void OnGUI(){
		
		//textStyle.fontSize = targetFontSize;
		//int size = textStyle.fontSize;
		//textStyle.fontSize = CameraScreen.GetPhysicalTextSize(size);
		//UnityEngine.GUI.Label(textRegion, Text, textStyle);
		//Debug.Log("textRegion: " + )
		UnityEngine.GUI.Label(new Rect(0,0,200,200), "TRALALALA", textStyle);
	}

	
	// Caclulate the Absolute Values on the physical screen - because TextRegion is virtual an relative to the Panel Position
	private void initTextRegion(){
		realTextRegion = new Rect(RealRegionOnScreen.x+ textRegion.x, 
		                            RealRegionOnScreen.y + textRegion.y, textRegion.width, textRegion.height);
		Rect t = activeScreen.GetPhysicalRegionFromRect(realTextRegion);
		realTextRegion = new Rect(realTextRegion.x, realTextRegion.y, t.width, t.height);
	}
}
