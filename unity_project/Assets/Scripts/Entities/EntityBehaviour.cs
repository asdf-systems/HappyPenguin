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

		public EntityState CurrentState { get; set; }

		public float Speed;
		public int RotateYCorrection;
		
	}
}
