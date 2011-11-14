using UnityEngine;
using System.Collections;

public class TextPanel : Panel {

	public int targetFontSize;
	public int textOffsetX;
	public int textOffsetY;
	public string Text;
	public GUIStyle textStyle;
	
	public Rect TextRegion;
	private Rect realTextRegion;
	
	protected override void AwakeOverride(){
		base.AwakeOverride();
		initTextRegion();
		
	}
	
	void Start(){
		
	}
	
	public override void LayoutElement(){
		base.LayoutElement();
		initTextRegion();
	}
	
	void OnGUI(){
		if(activeScreen.DebugModus)
			initTextRegion();
		textStyle.fontSize = targetFontSize;
		int size = textStyle.fontSize;
		textStyle.fontSize = CameraScreen.GetPhysicalTextSize(size);
		UnityEngine.GUI.Label(realTextRegion, Text, textStyle);
	}

	
	// Caclulate the Absolute Values on the physical screen - because TextRegion is virtual an relative to the Panel Position
	private void initTextRegion(){
		realTextRegion = new Rect(VirtualRegionOnScreen.x+ TextRegion.x, 
		                            VirtualRegionOnScreen.y + TextRegion.y, TextRegion.width, TextRegion.height);
		realTextRegion = activeScreen.GetPhysicalRegionFromRect(realTextRegion);
	}
}
