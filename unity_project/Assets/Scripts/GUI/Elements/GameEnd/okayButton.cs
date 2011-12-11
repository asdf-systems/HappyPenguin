using UnityEngine;
using System.Collections;

public class okayButton : Button {
	public GameEndState gameState;
	public GetNameAlert alert;
	
	public bool Show = false;
	
	void Start(){
		StartOverride();
	}
	
	protected override void StartOverride(){
		base.StartOverride();
		Visibility = false;
		//showButton = false;		
	}
	
	public override void OnClick(object sender, MouseEventArgs e){
		base.OnClick(sender,e);
		alert.HideText();
		GameStatics.Username = alert.Text;
		gameState.usernameInputFinished();
		alert.Visibility = false;
		//showButton = false;
		Visibility = false;
		
	}
	
#if UNITY_EDITOR
	void Update(){
		UpdateOverride();
	}
	protected override void UpdateOverride(){
		base.UpdateOverride();
		if(activeScreen.DebugModus){
			Visibility |= Show;
		}
			
		
	}
#endif
	
	

}
