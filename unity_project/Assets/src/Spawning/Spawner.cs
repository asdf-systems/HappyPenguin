using UnityEngine;
using System;
using System.Collections;
using HappyPenguin.Entities;

namespace HappyPenguin.Spawning
{
	public abstract class Spawner<T> where T : TargetableEntityBehaviour
	{

		// Use this for initialization
		public virtual void Start () {
			
		}

		// Update is called once per frame
		public virtual void Update () {
			
		}

		public event EventHandler<EntityGeneratedEventArgs<T>> EntitySpawned;
		protected void InvokeEntitySpawned (T entity) {
			var handler = EntitySpawned;
			if (handler == null) {
				return;
			}
			
			var e = new EntityGeneratedEventArgs<T> (entity);
			EntitySpawned (this, e);
		}
		
	}
}
