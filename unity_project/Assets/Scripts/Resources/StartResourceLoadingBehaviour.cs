using System;
using UnityEngine;
using Pux.Resources;


public sealed class StartResourceLoadingBehaviour : ResourceLoadingBehaviour
{
	
	
	public StartResourceLoadingBehaviour() {
	}
	protected override void OnResourcesLoaded(EventArgs e) {
		base.OnResourcesLoaded(e);
		
		Application.LoadLevel("MainMenu");
	}
	
	

}


