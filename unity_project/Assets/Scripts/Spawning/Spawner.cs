using UnityEngine;
using System;
using System.Collections;
using HappyPenguin.Entities;

namespace HappyPenguin.Spawning
{
	public abstract class Spawner<T> 
	{
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
