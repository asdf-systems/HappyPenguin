using UnityEngine;
using System.Collections;

public class Control : Panel {

	// Show Active Region is an Debug Option that makes the active array visible
	public bool ShowActiveRegion = false;
	public Rect ActiveRegion;
	private Rect realActiveRegion;
	
	void Awake(){
		AwakeOverride();
	}
	protected override void AwakeOverride(){
		base.AwakeOverride();
		initActiveRegion();
	}
	
	void Start () {
		
	}
	
	public override void createGUIElement(){
		base.createGUIElement();
	}
	
	void OnGUI(){
#if UNITY_EDITOR
		if(ShowActiveRegion){
			initActiveRegion();
			UnityEngine.GUI.Box(realActiveRegion, "");	
		}
#endif 
	}
	
	public override bool checkMouseOverElement(){
		if(ShowActiveRegion)
			initActiveRegion();
		Rect t = new Rect(VirtualRegionOnScreen.x+ ActiveRegion.x, 
		                          VirtualRegionOnScreen.y + ActiveRegion.y, ActiveRegion.width, ActiveRegion.height);
		return CameraScreen.cursorInside(t);
	}
	
	// Caclulate the Absolute Values on the physical screen - because ActiveRegion is virtual an relative to the Control Position
	private void initActiveRegion(){
		realActiveRegion = new Rect(VirtualRegionOnScreen.x+ ActiveRegion.x, 
		                            VirtualRegionOnScreen.y + ActiveRegion.y, ActiveRegion.width, ActiveRegion.height);
		realActiveRegion = activeScreen.GetPhysicalRegionFromRect(realActiveRegion);
	}

}
