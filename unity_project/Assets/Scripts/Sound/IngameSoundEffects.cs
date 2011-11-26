using UnityEngine;
using System.Collections;

public class IngameSoundEffects : MonoBehaviour {

	public AudioClip StartSound;
	public AudioClip BooSound;
	public AudioClip CheerSound;
	public AudioClip BaddySound;
	
	// Use this for initialization
	void Start () {
		audio.clip = StartSound;
		audio.Play();
	}
	
	public void PlayBooSound(){
		audio.clip = BooSound;
		audio.Play();
	}
	
	public void PlayCheerSound(){
		audio.clip = CheerSound;
		audio.Play();
	}
	
	public void PlayBaddySound(){
		audio.clip = BaddySound;
		audio.Play();
	}
}
