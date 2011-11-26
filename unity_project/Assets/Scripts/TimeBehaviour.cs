using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TimeBehaviour : MonoBehaviour {

	public static TimeBehaviour Instance{get;private set;}
	
	private static List<Timer> timerList = null;
	private static List<Timer> removeQueue = null;
	
	void Awake(){
		Instance = this;
		if(timerList == null)
			timerList = new List<Timer>();
		if(removeQueue == null)
			removeQueue = new List<Timer>();
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Timer t in timerList){
			t.Update();
		}
	
		foreach(Timer t in removeQueue){
			timerList.Remove(t);
		}
		removeQueue.Clear();
		
			
	}
	
	public void AddTimer(Timer timer){
		timerList.Add(timer);
	}
	
	public void RemoveTimer(Timer timer){
		removeQueue.Add(timer);
	}
}
