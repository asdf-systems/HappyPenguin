using UnityEngine;
using System.Collections.Generic;
using System;
using HappyPenguin;

namespace HappyPenguin.Entities
{
	public abstract class EntityBehaviour : MonoBehaviour
	{
		public EntityBehaviour() {
			Speed = 10.0f;	
		}

		public Vector3 Position {
			get { return gameObject.transform.position; }
		}

		public Quaternion Orientation {
			get { return gameObject.transform.rotation; }
		}
		
		public void FadeAnimation(string name, int fadeDurationInMilliseconds)
		{
			var current = animation[name];
			current.wrapMode = WrapMode.Loop;
			current.layer = 0;
			var seconds = (float) TimeSpan.FromMilliseconds(fadeDurationInMilliseconds).TotalSeconds;
			animation.CrossFade(name, seconds, PlayMode.StopSameLayer);
		}
		
		public void PlayAnimation(string name)
		{
			var current = animation[name];
			current.wrapMode = WrapMode.Once;
			current.layer = 1;
			animation.CrossFade(name, 0.0f, PlayMode.StopSameLayer);
		}

		public void Awake() {
			AwakeOverride();
		}
		
		public void Update() {
			UpdateOverride();
		}

		protected virtual void UpdateOverride() {
			if (CurrentState == null) {
				return;
			}
			CurrentState.Update(this);
		}


		protected virtual void AwakeOverride() {
			// nothing here
		}
		
		private EntityState currentState;
		public EntityState CurrentState {
			get{ return currentState;} 
			set{
				if (currentState != null) {
					currentState.Stop(this);
				}
				if (value == null) {
					return;
				}
				
				currentState = value;
				currentState.Start(this);
			} 
		}

		public float Speed;
		public int RotateYCorrection;
		
		public AudioClip AttackSound;
		public AudioClip MovingSound;
		public AudioClip DeathSound;
		public AudioClip OtherSound;

	}
}
