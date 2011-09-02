using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GUIManager manager;
	
	// Use this for initialization
	void Start () {
		manager.pushGUIState(new MainGUS(manager, manager.PlayerCam));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
