using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin.Entities
{
	public sealed class StackedEntityState : EntityState
	{
		public Queue<EntityState> stateQueue { 
			get; 
			private set;
		}

		public StackedEntityState (string name) : base(name){
			stateQueue = new Queue<EntityState>();
		}

		public override void Start (EntityBehaviour entity){
			stateQueue.Peek().Start(entity);
		}

		public override void Stop (EntityBehaviour entity){
			stateQueue.Peek().Stop(entity);
		}

		public override void Update (EntityBehaviour entity){
			stateQueue.Peek().Update(entity);
		}

		public override void RemoveControllersByType<T> (){
			stateQueue.Peek().RemoveControllersByType<T>();
		}
		
		public void AddEntityState(EntityState state) {
			stateQueue.Enqueue(state);
		}

		public void OnStateFinished(object sender, StateFinishedEventArgs<EntityBehaviour> e){
			Stop(e.EntityType);
			stateQueue.Dequeue();
			Start(e.EntityType);
			if(stateQueue.Count == 0){
				InvokeStateFinished(e.EntityType);
			}
		}
	}
}

