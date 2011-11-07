using System;
using UnityEngine;
using Pux.Resources;


public sealed class MainResourceLoadingBehaviour : ResourceLoadingBehaviour
{
	public MainResourceLoadingBehaviour() {
	}
	protected override void OnResourcesLoaded(EventArgs e) {
		base.OnResourcesLoaded(e);
		
	}

	#region implemented abstract members of Pux.Resources.ResourceLoadingBehaviour
	protected override void LoadResources() {
		ResourceManager.LoadResource("Player/Textures/penguin_clr_001");
		ResourceManager.LoadResource("Player/Textures/penguin_hexer");	
		
		
		
	}


	protected override void UnloadResources() {
		
	}
	
	#endregion
}


