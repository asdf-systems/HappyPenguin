using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	
	public AudioClip MusicStart;
	public AudioClip Music;

	// Use this for initialization
	void Awake () {
		audio.clip = MusicStart;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		PlayMusic(Music);
	}
	
	private void PlayMusic(AudioClip clip){
		if (!audio.isPlaying) {
			audio.clip = clip;
			audio.Play();
		}
	}
}