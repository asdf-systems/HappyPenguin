using System;
using UnityEngine;

namespace Pux.Resources
{
	public abstract class ResourceLoadingBehaviour : MonoBehaviour
	{
		protected ResourceLoadingBehaviour() {
		}

		private void Awake() {
			OnResourcesLoading(EventArgs.Empty);
			LoadResources();
			OnResourcesLoaded(EventArgs.Empty);
		}
		
		protected virtual void OnResourcesLoading(EventArgs e) { }
		protected virtual void OnResourcesLoaded(EventArgs e) { }

		protected abstract void LoadResources();
		protected abstract void UnloadResources();
	}
}

