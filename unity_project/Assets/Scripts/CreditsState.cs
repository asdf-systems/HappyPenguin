using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsState : MonoBehaviour {

	private float time; 
	private List<string> texts;
	private int currentText;
	public CreditsText textElement; 
	private bool flag;
	public GameObject katapult;
	public GameObject penguin1;
	public GameObject penguin2;
	
	// Use this for initialization
	void Start () {
		time = 0.0f;
		texts = new List<string>();
		InitCredits();
		flag = true;
	}
	
	void InitCredits(){
		currentText = 0;
		texts.Add("");		
		texts.Add("Alexander Surma - Programming");
		texts.Add("");
		texts.Add("Alexander Wieser - Programming");
		texts.Add("");
		texts.Add("Christian Schmiedl - Sound");
		texts.Add("");
		texts.Add("Friedrich Wessel - 3d & Animation");
		texts.Add("");
		texts.Add("Sebastian Vollmar - Design");
		texts.Add("");
		texts.Add("Fabian Reif - Marketing");
		texts.Add("");
		texts.Add("Philipp Lämmel - Trainee");
		texts.Add("");
		texts.Add("Felix Dietz - Trainee");
		
	}
	// Update is called once per frame
	void Update () {
		
		time+= Time.deltaTime;
		//Debug.Log("Update GameEndState, Time: " + time);
		if(time > 3 && flag){
			if(texts[currentText] == ""){
				katapult.gameObject.animation.Play("shoot");
				katapult.gameObject.animation.PlayQueued("pull");
				penguin1.gameObject.animation.Play("happy");
				penguin2.gameObject.animation.Play("happy");
			} 
			currentText++;		
			textElement.text = texts[currentText];
			if(currentText > texts.Count)
				flag = false;
			time = 0;
		}
	}
}
