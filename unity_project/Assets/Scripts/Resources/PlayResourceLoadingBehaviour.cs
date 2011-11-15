using System;
using UnityEngine;
using Pux.Resources;


public sealed class PlayResourceLoadingBehaviour : ResourceLoadingBehaviour
{
	public PlayResourceLoadingBehaviour() {
	}
	protected override void OnResourcesLoaded(EventArgs e) {
		base.OnResourcesLoaded(e);
		Application.LoadLevel("Arena");
	}

}


