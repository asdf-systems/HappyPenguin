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
	
		public State State {
			get;
			set;
		}
	}
}
