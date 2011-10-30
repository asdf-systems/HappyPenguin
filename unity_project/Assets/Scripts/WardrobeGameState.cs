using UnityEngine;
using System.Collections;

public class WardrobeGameState : MonoBehaviour {

	private System.Random random;
	private string lastanimation;
	public string[] animations;
	// Zahlen von 0 - 100 ist die gezogene Zahl > der Range und kleiner der nächsten wird die anim gespielt
	public int[] animationsProbabilityRange;
	public PlayerBehaviour player;
	private AnimationState currentAnim;
	private float targetTime;
	
	
	void Awake(){
		random = new System.Random();
		
	}
	
	// Use this for initialization
	void Start () {
		if(animations.Length != animationsProbabilityRange.Length)
			Debug.LogError("Animations & Animations Probability dont match in size");
		startNextAnimation();
		

		
	}
	
	// Update is called once per frame
	void Update () {
		if(!player.gameObject.animation.isPlaying){
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
		player.gameObject.animation.Play(lastanimation);
		
		
	}
}
