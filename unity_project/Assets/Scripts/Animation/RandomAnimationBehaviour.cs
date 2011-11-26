using UnityEngine;
using System.Collections;

public class RandomAnimationBehaviour : MonoBehaviour {

	private System.Random random;
	private string lastanimation;
	public string[] animations;
	// Zahlen von 0 - 100 ist die gezogene Zahl > der Range und kleiner der nächsten wird die anim gespielt
	public int[] animationsProbabilityRange;
	private AnimationState currentAnim;
	private float targetTime;
	private Animation animationObject;
	
	
	void Awake(){
		random = new System.Random();
		
	}
	
	// Use this for initialization
	void Start () {
		initAnimationObject();
		if(animations.Length != animationsProbabilityRange.Length)
			EditorDebug.LogError("Animations & Animations Probability dont match in size");
		startNextAnimation();	
	}
	
	private void initAnimationObject(){
		animationObject = gameObject.animation;
		if(animationObject == null){
			animationObject = gameObject.GetComponentInChildren<Animation>() as Animation;
			if(animationObject == null)
				throw new MissingComponentException();
		}
	}

	// Update is called once per frame
	void Update () {
		if(!animationObject.isPlaying){
			startNextAnimation();
			
		} 
	}
	
	private void startNextAnimation(){
		int index = 0;
		float rndValue = random.Next(0,100);
		for(int i = 0; i < animations.Length; i++){
			if(rndValue > animationsProbabilityRange[i])
				index = i;
		}
		
		lastanimation = animations[index];
		animationObject.Play(lastanimation);
		
		
	}
}