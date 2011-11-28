using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScreen : Frame {

	// If EditorDebugModus is checked, ScreenPosition is update every OnGUI call this is usefull for positioning elements
	// but not good for the framerate
	public bool DebugModus;
	public int TextureSize = 512;
	public Material GUIMaterial;
	
	// Public Member - init in the inspector
	public Camera ScreenCamera;
	
	
	//  Propertys
	public static Vector2 mousePosition{
		get{
			return PhysicalToVirtualScreenPosition(Input.mousePosition); 
		}
	}
	
	private Panel[] allChildren{
		get{
			return (gameObject.GetComponentsInChildren<Panel>() as Panel[]);
		}
	}
	
	// DONT USE THIS
	void Awake(){
		AwakeOverride();
	}
	
	protected override void AwakeOverride(){
		base.AwakeOverride();
	}
	
	void Start(){
		CreateElement();
		//CalculatePhysicalRegion();
		initEvents();
		//LayoutElement();
		
		
	}
	
	private void initEvents(){
		InputEvents.Instance.ClickEvent += OnClick;	
		InputEvents.Instance.MoveEvent += OnMove;
		InputEvents.Instance.MoveEvent += OnHover;
		InputEvents.Instance.DownEvent += OnDown;
		InputEvents.Instance.UpEvent += OnUp;
		InputEvents.Instance.SwipeEvent += OnSwipe;
	}
	
	
#if UNITY_EDITOR
	void Update(){
		UpdateOverride();
	}
	
	protected override void UpdateOverride(){
		base.UpdateOverride();

		if(DebugModus)
			UpdateElement();

	}
#endif	
	
	
	private static Vector2 getFactor(){
		// Get the right Hight and Width proportional to screen
		float rightAspectHeight = Screen.height;
		float rightAspectWidth = Screen.width;
		float aspectRatio = (float)(Screen.width) / Screen.height;
		if(aspectRatio < ScreenConfig.Instance.ScreenAspect){
			rightAspectHeight = (float)(Screen.width) / ScreenConfig.Instance.ScreenAspect;	
		} else{

			rightAspectWidth = (float)(Screen.height) * ScreenConfig.Instance.ScreenAspect;
		}
		
		//EditorDebug.Log("Aspect: " + ScreenConfig.Instance.ScreenAspect);
		//EditorDebug.Log("RightAspectHeight: " + rightAspectHeight);
		//float factorY = (float)(Screen.height) / (float)(ScreenConfig.Instance.TargetScreenHeight); 
		float factorY = rightAspectHeight / (float)(ScreenConfig.Instance.TargetScreenHeight);
		//float factorX = (float)(Screen.width) / (float)(ScreenConfig.Instance.TargetScreenWidth);
		float factorX = rightAspectWidth / (float)(ScreenConfig.Instance.TargetScreenWidth);
		//float factorY = factorX * ScreenConfig.Instance.ScreenAspect;
		return new Vector2(factorX, factorY);
	}
	
	
	public Rect GetPhysicalRegionFromRect(Rect rect){
		Rect camPosition = ScreenCamera.pixelRect;
		// Move Camera is needed for Splitscreen
		if(((int)ScreenCamera.pixelHeight) != Screen.height){
			//EditorDebug.Log("ScreenCamera Height: " + ScreenCamera.pixelHeight +  "\n Screen Height: " + Screen.height);
			camPosition.y = ScreenCamera.pixelHeight - camPosition.y;
		}
		
		Vector2 factor = getFactor();
		Vector2 newPosition = new Vector2((camPosition.x+rect.x)*factor.x, (camPosition.y +  rect.y)*factor.y);
		Vector2 newSize = new Vector2(rect.width*factor.x,rect.height*factor.y);
		
		return new Rect (  newPosition.x, newPosition.y, newSize.x, newSize.y );
	} 
	
	public Vector2 GetFloatingPosition(Panel panel){
		var ret = new Vector2(0,0);
		var horizontalFloat = panel.horizontalFloat;
		var verticalFloat = panel.verticalFloat;
		ret.y = getVerticalFloatPosition(verticalFloat, panel);
		ret.x = getHorizontalFloatPosition(horizontalFloat, panel);
		
		return ret;
	}
	
	private float getVerticalFloatPosition(Panel.VerticalFloatPositions floatValue, Panel panel){
		float ret = panel.RealRegionOnScreen.y;
		switch(floatValue){
			case Panel.VerticalFloatPositions.none:
			break;
			case Panel.VerticalFloatPositions.top:
				ret =  0.0f;
			break;
			case Panel.VerticalFloatPositions.bottom:
				ret =  (Screen.height - panel.RealRegionOnScreen.height);
			break;
			case Panel.VerticalFloatPositions.center:
				ret =  (Screen.height/2 - panel.RealRegionOnScreen.height/2);
			break;
			default:
				EditorDebug.LogError("Unknown VerticalPosition: " + floatValue);
			break;
		}
		return ret;
	}
	
	private float getHorizontalFloatPosition(Panel.HorizontalFloatPositions floatValue, Panel panel){
		float ret = panel.RealRegionOnScreen.x;
		switch(floatValue){
			case Panel.HorizontalFloatPositions.none:
			break;
			case Panel.HorizontalFloatPositions.left:
				ret = 0.0f;
			break;
			case Panel.HorizontalFloatPositions.right:
				ret = (Screen.width - panel.RealRegionOnScreen.width);
			break;
			case Panel.HorizontalFloatPositions.center:
				ret = (Screen.width/2 - panel.RealRegionOnScreen.width/2);
			break;
			default:
				EditorDebug.LogError("Unknown HorizontalPosition: " + floatValue);
			break;
			
		}
		return ret;
	}
	
	public static int GetPhysicalTextSize(int size) {
		Vector2 factor = getFactor();
		return (int)(size * factor.y);
	}
	
	protected override void callHandler(InteractionEvent interaction, ActionEvent action){
		InteractionBehaviour[] behaviours = gameObject.GetComponents<InteractionBehaviour>() as InteractionBehaviour[];
		foreach(InteractionBehaviour ib in behaviours){
			interaction(ib);
		}
		base.callHandler(interaction, action);
	}
	
	private void createElements(){
		foreach(Panel box in allChildren){
			box.createGUIElement();
		}
	}
	
	// STATIC FUNCTIONS
	public static bool cursorInside(Vector2 elementPosition, Vector2 elementSize) {
		bool flagX = false;
		bool flagY = false;
		
		if (mousePosition.x >= elementPosition.x && (mousePosition.x <= (elementPosition.x + elementSize.x)))
			flagX = true;
		if (mousePosition.y >= elementPosition.y && (mousePosition.y <= (elementPosition.y + elementSize.y)))
			flagY = true;
		return (flagX && flagY);
	}
	
	
	public static bool cursorInside(Rect region){
		return cursorInside(new Vector2(region.x, region.y), new Vector2(region.width, region.height));
	}
	
	public static CameraScreen GetScreenForObject(GameObject obj){
		GameObject savedObj = obj;
		GameObject savedParent = null;
		CameraScreen screen = null;
		while(obj != null){
			screen = obj.GetComponent<CameraScreen>();
			if( screen != null){
				break;
			}
				
			if(obj.transform.parent != null)
				savedParent = obj.transform.parent.gameObject;
			else 
				savedParent = null;
			obj = savedParent;
		}
		if(screen == null){
			EditorDebug.LogWarning("Element: " + savedObj.gameObject.name + " is not a child of a Screen!");
		}
		return screen;
		
		
	}
	
	/*public static Vector2 PhysicalToVirtualScreenPosition(Vector2 screenPosition){
		float factorY = (float)(Screen.height) / (float)(ScreenConfig.TargetScreenHeight); 
		float factorX = (float)(Screen.width) / (float)(ScreenConfig.TargetScreenWidth);
		screenPosition.y = Screen.height - screenPosition.y;
		screenPosition.x *= factorX;
		screenPosition.y *= factorY;
		return screenPosition;
	}*/
	public static Vector2 PhysicalToVirtualScreenPosition(Vector2 screenPosition){
		//float factorY = (float)(Screen.height) / (float)(ScreenConfig.Instance.TargetScreenHeight); 
		var factor = getFactor();
		//float factorX = (float)(Screen.width) / (float)(ScreenConfig.Instance.TargetScreenWidth);
		//float factorY = factorX / ScreenConfig.Instance.ScreenAspect;
		screenPosition.y = Screen.height - screenPosition.y;
		screenPosition.x /= factor.x;
		screenPosition.y /= factor.y;
		return screenPosition;
	}
	
	
	

}
