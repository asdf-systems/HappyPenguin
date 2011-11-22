using UnityEngine;
using System.Collections;

public class MovieTexture : MonoBehaviour {

	public int movieSpeedFPS; 
	public Texture[] textures;
	private float frameTime;
	
	private int currentFrameNumber;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake(){
		frameTime = 0;
		currentFrameNumber = 0;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate(){
		//if(!GameConfig.videoPlay)
		//	return;
		
		frameTime += Time.deltaTime;
		if(frameTime > (1.0f/movieSpeedFPS)){ // change frame
			
			currentFrameNumber ++;
			frameTime = 0;
			if(currentFrameNumber >= textures.Length)
				currentFrameNumber = 0;
				gameObject.renderer.material.SetTexture("_MainTex", textures[currentFrameNumber]);		
		}
	}
	
	
 
 

}
