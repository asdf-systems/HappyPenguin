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
		public Queue<EntityState> stateQueue { get; private set; }

		public StackedEntityState ()
		{
		}

		public override void Start (EntityBehaviour entity)
		{
			
		}

		public override void Stop (EntityBehaviour entity)
		{
			
		}

		public override void Update (EntityBehaviour entity)
		{
			
		}

		public override void RemoveControllersByType<T> ()
		{
			
		}

		public override void OnControllerFinished ()
		{
			
		}
	}
}

