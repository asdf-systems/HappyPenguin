using System;
using System.Linq;
using UnityEngine;
using HappyPenguin.Entities;
using HappyPenguin.Controllers;

namespace HappyPenguin.Entities
{
	public static class EntityBehaviourExtensions
	{
		public static EntityBehaviour Stop(this EntityBehaviour entity)
		{
			entity.ClearControllers();	
			return entity;
		}
		
		public static EntityBehaviour FadeAnimation(this EntityBehaviour entity, string name, int fadeDurationInMilliseconds)
		{
			return entity.FadeAnimation(name, fadeDurationInMilliseconds, false);
		}
		
		public static EntityBehaviour FadeAnimation(this EntityBehaviour entity, string name, int fadeDurationInMilliseconds, bool isLooped)
		{
			var current = entity.gameObject.animation[name];
			if (current == null) {
				Debug.Log(string.Format("invalid animation name {0}"));
				return entity;
			}
			
			current.layer = 0;
			current.wrapMode = isLooped ? WrapMode.Loop : WrapMode.Once;
			var seconds = (float) TimeSpan.FromMilliseconds(fadeDurationInMilliseconds).TotalSeconds;
			entity.animation.CrossFade(name, seconds, PlayMode.StopSameLayer);
			return entity;
		}
		
		public static EntityBehaviour PlayAnimation(this EntityBehaviour entity, string name, bool isLooped)
		{
			var current = entity.gameObject.animation[name];
			if (current == null) {
				Debug.Log(string.Format("invalid animation name {0}"));
				return entity;
			}
			
			current.layer = 0;
			current.wrapMode = isLooped ? WrapMode.Loop : WrapMode.Once;
			entity.gameObject.animation.Play(name, PlayMode.StopSameLayer);
			return entity;
		}
		
		public static EntityBehaviour PlayAnimation(this EntityBehaviour entity, string name)
		{
			entity.PlayAnimation(name, false);
			return entity;
		}
		
		public static EntityBehaviour SwimTo(this EntityBehaviour entity, Vector3 target)
		{
			entity.RemoveController("move");
			
			var seaTarget = new Vector3(target.x, Environment.SeaLevel, target.z);
			entity.AddController("move", new LinearMovementController(seaTarget));
			return entity;
		}
		
		public static EntityBehaviour MoveTo(this EntityBehaviour entity, Vector3 target)
		{
			entity.RemoveController("move");
			entity.AddController("move", new LinearMovementController(target));
			return entity;
		}
		
		public static EntityBehaviour Float(this EntityBehaviour entity)
		{
			if (entity.HasController("float")) {
				return entity;
			}
			entity.AddController("float", new FloatController(Environment.SeaLevel));
			return entity;
		}
		
		public static EntityBehaviour Follow(this EntityBehaviour entity, GameObject target)
		{
			entity.RemoveController("move");
			entity.AddController("move", new LinearObjectFollowMovementController(target));
			return entity;
		}
		
		public static EntityBehaviour Patrol(this EntityBehaviour entity){
			throw new NotImplementedException();
		}
		
		public static EntityBehaviour Throw(this EntityBehaviour entity, GameObject target) {
			return entity.Follow(target);
		}
	
		public static EntityBehaviour Dive(this EntityBehaviour entity, GameObject vanishingPoint, int flatness)
		{
			entity.RemoveController("move");
			entity.RemoveController("float");
			
			var arc = new ArcMovementController(entity, vanishingPoint, flatness);
			entity.AddController("move", arc);
			return entity;
		}
	}
}

