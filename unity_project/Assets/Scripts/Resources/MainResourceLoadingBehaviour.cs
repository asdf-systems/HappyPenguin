using System;
using UnityEngine;
using asdf.Resources;


public sealed class MainResourceLoadingBehaviour : ResourceLoadingBehaviour
{
	public MainResourceLoadingBehaviour() {
	}
	protected override void OnResourcesLoaded(EventArgs e) {
		base.OnResourcesLoaded(e);
		
	}

	#region implemented abstract members of Pux.Resources.ResourceLoadingBehaviour
	protected override void LoadResources() {
		ResourceManager.LoadResource("Player/Textures/pux_normal_skin");
		ResourceManager.LoadResource("Player/Textures/pux_hexer_skin");	
		ResourceManager.LoadResource("Player/Textures/pux_priester_skin");	
		ResourceManager.LoadResource("Player/Textures/pux_kenny_skin");	
		
		
	}


	protected override void UnloadResources() {
		
	}
	
	#endregion
}


