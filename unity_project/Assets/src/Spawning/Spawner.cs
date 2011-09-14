using UnityEngine;
using System;
using System.Collections;
using HappyPenguin.Entities;

namespace HappyPenguin.Spawning
{
	public abstract class Spawner<T> where T : TargetableEntityBehaviour
	{

		// Use this for initialization
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {
			
		}

		public event EventHandler<EntitySpawnedEventArgs<T>> EntitySpawned;
		protected void InvokeEntitySpawned (T entity) {
			var handler = EntitySpawned;
			if (handler == null) {
				return;
			}
			
			var e = new EntitySpawnedEventArgs<T> (entity);
			EntitySpawned (this, e);
		}
		
	}
}
