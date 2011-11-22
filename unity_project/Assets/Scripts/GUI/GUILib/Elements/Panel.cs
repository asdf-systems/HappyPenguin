using UnityEngine;
using System.Collections;
using Pux.Resources;

public class Panel : Frame {

	public enum HorizontalFloatPositions {left, right, none}
	public enum VerticalFloatPositions {top, bottom, none}
	
	public Rect VirtualRegionOnScreen; 
	public LayoutBehaviour Layout;
	public string help1 = "NOT WORKING LIVE:";
	public int GUIDepth = 1;
	public VerticalFloatPositions verticalFloat;
	public HorizontalFloatPositions horizontalFloat;
	
	public bool Visibility{
		get{
			bool flag = false;
			if(plane != null)
				flag = plane.renderer.enabled;
			return flag;
		}
		set{
			if(plane != null)
				plane.renderer.enabled = value;
		}
	}
	
	public string helpLate = "This option give possibilty to create Element Later via Code";
	public bool LateCreation = false;
	private bool created = false;
	
	public Rect Uv;
	
	private GUIStyle inactiveStyle;
	
	protected GUIPlane plane;

	public Rect RealRegionOnScreen{
		get;
		set;
	}	
	protected GUIStyle currentStyle;

	public CameraScreen activeScreen;
	

	// PROPERTYS
	public Vector2 Position{
		get{
			return new Vector2(VirtualRegionOnScreen.x, VirtualRegionOnScreen.y);
		}
		set{
			VirtualRegionOnScreen.x = value.x; 
			VirtualRegionOnScreen.y = value.y;
		}
		
	}
	
	public Vector2 Size{
		get{
			return new Vector2(VirtualRegionOnScreen.xMax, VirtualRegionOnScreen.yMin);
		}
		set{
			VirtualRegionOnScreen.width = value.x; 
			VirtualRegionOnScreen.height = value.y; 
		}
	}
	
	
	
	// DONT USE THIS
	void Awake(){
		AwakeOverride();
	}
	
	void OnDestroy(){
		OnDestroyOverride();
	}
	
	protected virtual void OnDestroyOverride(){
		
		if(plane != null){
			GameObject.Destroy(plane.gameObject);
		}
	}
	
	// Use this for initialization
	protected override void AwakeOverride(){
		base.AwakeOverride();
		if(!LateCreation)
			CreateElement();
	}
	

	void Start () {
		StartOverride();
		
	}
	
	protected virtual void StartOverride(){
		UpdateRegionOnScreen();
	}
	
	void OnGUI(){
		OnGUIOverride();
	}
	
	protected virtual void OnGUIOverride(){
		
	}
	
	// Update is called once per frame
#if UNITY_EDITOR
	void Update () {
		UpdateOverride();
	}
	
	protected override void UpdateOverride(){
		base.UpdateOverride();
		if(activeScreen.DebugModus ){
			UpdateElement();
		}
	}
#endif		

	
	public override void CreateElement(){
		base.CreateElement();
		if(created){
			Debug.Log("Element: "+ gameObject.name + "already created");
			return;
		}
		RealRegionOnScreen = new Rect(0,0,0,0);
		activeScreen = CameraScreen.GetScreenForObject(this.gameObject);
		this.createGUIElement();
		created = true;
		UpdateElement();
	}
	
	public override void UpdateElement(){
		base.UpdateElement();
		this.RealRegionOnScreen = activeScreen.GetPhysicalRegionFromRect(this.VirtualRegionOnScreen);
		UpdateRegionOnScreen();
	}
	
	public virtual void UpdateRegionOnScreen(){
		if(plane != null)
			plane.VirtualRegionOnScreen = RealRegionOnScreen;
		
		resetElement();
	}
	
	public virtual void createGUIElement(){
		
		if(created)
			return;
		
		CreateGUIPlane();
				
		Camera cam = activeScreen.ScreenCamera; 
		plane.name = gameObject.name + "_guiPlane";
		plane.transform.parent = cam.transform;
		
		// Orient Plane to Camera
		resetPlaneTransform();
		float layer = (float)GUIDepth * 0.0001f;
		plane.transform.Translate(new Vector3(0,0,(activeScreen.ScreenCamera.nearClipPlane+layer)), Space.Self);
		plane.transform.LookAt(cam.transform);
		
		// set Materials
		plane.GUIMaterial = activeScreen.GUIMaterial;
		plane.UV = Uv;
		plane.VirtualRegionOnScreen = RealRegionOnScreen;
			
	}
	
	private Vector3 WorldToLocalCoordinates(Vector3 worldCoordinates){
		return gameObject.transform.InverseTransformPoint(worldCoordinates);
	}
	
	private void CreateGUIPlane(){
		GameObject go = ResourceManager.CreateInstance<GameObject>("guiPlane");
		if(go == null){
			Debug.LogError("No GameObject found for Plane on Object "+ this.gameObject.name + "! Stop!");
			return;
		}
		
		plane = go.GetComponent<GUIPlane>();
		if(plane == null){
			Debug.LogError("No GUIPlane found on Object "+ this.gameObject.name + "! Stop!");
			return;
		}
		
		
	}
	
	public override  bool checkMouseOverElement(){
		return CameraScreen.cursorInside(RealRegionOnScreen);
	}
	
	
	
	public override void resetElement(){
		if(plane != null)
			plane.UV = Uv;
		
	}
	
	private void resetPlaneTransform(){
		plane.transform.rotation = Quaternion.identity;
		plane.transform.localRotation = Quaternion.identity;
		plane.transform.localPosition = Vector3.zero;
		plane.transform.localScale = Vector3.one;
	}
	
	
}
