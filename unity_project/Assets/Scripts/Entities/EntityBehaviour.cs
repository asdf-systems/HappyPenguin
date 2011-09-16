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
			Speed = 1.0f;
		}
		
		public Vector3 Position {
			get { return gameObject.transform.position; }
		}
		
		public Quaternion Orientation {
			get { return gameObject.transform.rotation; }
		}
		
		public void Awake()
		{
			Debug.Log("entity created, base call");
			AwakeOverride();
		}
		
		public virtual void AwakeOverride()
		{
			// nothing here
		}
	
		public EntityState CurrentState {
			get;
			set;
		}
		
		public float Speed {
			get;
			set;
		}	
	}
}
