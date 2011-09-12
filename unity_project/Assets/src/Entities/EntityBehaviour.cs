using UnityEngine;
using System.Collections.Generic;
using System;
using HappyPenguin;

namespace HappyPenguin.Entities
{
	public abstract class EntityBehaviour : MonoBehaviour
	{
		public EntityBehaviour ()
		{
			
		}
		
		// Use this for initialization
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {
			
		}

		public event EventHandler<AnimationFinishedEventArgs> AnimationFinished;
		private void InvokeAnimationFinished (string animationName) {
			var handler = AnimationFinished;
			if (handler == null) {
				return;
			}
			
			var e = new AnimationFinishedEventArgs (animationName);
			this.AnimationFinished (this, e);
		}
	
		public State State {
			get;
			set;
		}
	}
}
