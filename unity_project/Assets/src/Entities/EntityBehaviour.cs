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
		
		public Vector3 Position {
			get { return gameObject.transform.position; }
			
		}
		
		public Quaternion Orientation {
			get { return gameObject.transform.rotation; }
		}
	
		public State CurrentState {
			get;
			set;
		}
	}
}
