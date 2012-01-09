using System;
using UnityEngine;
using asdf.Resources;


public sealed class PlayResourceLoadingBehaviour : ResourceLoadingBehaviour
{
	public event EventHandler LoadingFinished;
	
	public PlayResourceLoadingBehaviour() {
	}
	protected override void OnResourcesLoaded(EventArgs e) {
		base.OnResourcesLoaded(e);
		InvokeLoadingFinished();
	}
	
	private void InvokeLoadingFinished(){
		var handler = LoadingFinished;
		if (handler == null) {
			return;
		}
		handler(this, EventArgs.Empty);
	}

}


