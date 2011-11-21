using UnityEngine;
using System.Collections;

public class AnimatedUVBehaviour : UVMoveBehaviour {

	public int MovieSpeedFPS;
	public int FrameSize;
	public int FramesPerRow;
	public int RowCount;
	
	private float frameTime;
	private int currentFrameNumber;
	private int frameCount;
	private int currentRow;
	private int currentColoum;
	private int textureSize;
	
	// Use this for initialization
	void Start () {
		StartOverride();
		textureSize = mainTexture.height;
	}
	
	protected override void StartOverride(){
		base.StartOverride();
		
	}
	
	void Awake(){
		AwakeOverride();
	}
	protected override void AwakeOverride(){
		base.AwakeOverride();
		frameTime = 0;
		currentFrameNumber = 0;
		currentRow = 0;
		currentColoum = 0;
		frameCount = FramesPerRow * RowCount;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void FixedUpdate(){
		
		frameTime += Time.deltaTime;
		if(frameTime > 1/MovieSpeedFPS){ // change frame
			
			currentFrameNumber ++;
			frameTime = 0;
			if(currentFrameNumber >= frameCount)
				currentFrameNumber = 0;
			
			changeFrame();
			
		}
	}
	
	private void changeFrame(){
		int x = currentColoum * FrameSize;
		int y = textureSize - (currentRow * FrameSize);
		var newPosition = new Rect(x,y,FrameSize, FrameSize);
	}
}
