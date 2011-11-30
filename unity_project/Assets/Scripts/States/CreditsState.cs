using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreditsState : MonoBehaviour {

	private float time; 
	private List<string> texts;
	public TextPanel textElement; 
	private bool flag;
	public GameObject katapult;
	public GameObject penguin1;
	public GameObject penguin2;
	
	// Use this for initialization
	void Start () {
		/*time = 0.0f;
		flag = true;*/
		texts = new List<string>();
		textElement.Text = string.Empty;
		InitCredits();
		
		foreach(var s in texts){
			textElement.Text +=s;	
		}
		
	}
	
	void InitCredits(){
		texts.Add("Programming");
		texts.Add("\n");
		texts.Add("    Alexander Wieser");
		texts.Add("\n");
		texts.Add("    Alexander Surma");
		texts.Add("\n");
		texts.Add("\n");
		texts.Add("Sound");
		texts.Add("\n");
		texts.Add("    Christian Schmiedl");
		texts.Add("\n");
		texts.Add("\n");
		texts.Add("3d and Animation");
		texts.Add("\n");
		texts.Add("    Friedrich Wessel");
		texts.Add("\n");
		texts.Add("\n");
		texts.Add("Design");
		texts.Add("\n");
		texts.Add("    Sebastian Vollmar");
		texts.Add("\n");
		texts.Add("    Marco Ehrensberger");
		texts.Add("\n");
		texts.Add("\n");
		texts.Add("Marketing");
		texts.Add("\n");
		texts.Add("    Fabian Reif");
		texts.Add("\n");
		texts.Add("\n");
		texts.Add("Trainees");
		texts.Add("\n");
		texts.Add("    Philipp Laemmel");
		texts.Add("\n");
		texts.Add("    Felix Dietz");
		
	}
	// Update is called once per frame
	void Update () {
		
		/*time+= Time.deltaTime;
		//EditorDebug.Log("Update GameEndState, Time: " + time);
		if(time > 3 && flag){
			if(texts[currentText] == ""){
				katapult.gameObject.animation.Play("shoot");
				katapult.gameObject.animation.PlayQueued("pull");
				penguin1.gameObject.animation.Play("happy");
				penguin2.gameObject.animation.Play("happy");
			} 
			currentText++;		
			textElement.Text = texts[currentText];
			if(currentText > texts.Count)
				flag = false;
			time = 0;
		} else if(!flag){
			Application.LoadLevel(1);
		}*/
	}
}
