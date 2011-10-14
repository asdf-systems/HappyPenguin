using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using HappyPenguin.Entities;
using HappyPenguin.Spawning;
using HappyPenguin.Unity;
using HappyPenguin.Controllers;

namespace HappyPenguin.Entities
{
	public sealed class EntityManager
	{		
		public void ThrowSnowball(TargetableEntityBehaviour target)
		{
			var snowball = DisplaySnowball();
			entities.Add(snowball);
		}
		
		private SnowballBehaviour DisplaySnowball()
		{
			var snowball = Resources.Load("Environment/Snowball");
			var instance = GameObject.Instantiate(snowball, Vector3.zero, Quaternion.identity) as GameObject;

			instance.transform.parent = Player.rightHandPoint.transform;
			instance.transform.localPosition = Vector3.zero;

			
			var component = instance.GetComponentInChildren<SnowballBehaviour>();
			if (component == null) {
				throw new ApplicationException("EnvironmentEntityBehaviour not found.");
			}
			
			return component;
		}

		public event EventHandler<TargetableEntityEventArgs> SnowballHit;
		private void InvokeSnowballHit(TargetableEntityBehaviour entity)
		{
			var handler = SnowballHit;
			if (handler == null) {
				return;
			}
			var e = new TargetableEntityEventArgs(entity);
			handler(this, e);
		}
		
		private readonly TargetableSymbolManager symbolManager;
		private readonly List<EntityBehaviour> entities;

		public EntityManager() {
			entities = new List<EntityBehaviour>();
			symbolManager = new TargetableSymbolManager();
		}
		
		public IEnumerable<EntityBehaviour> Entities {
			get { return entities; }
		}

		public IEnumerable<CreatureBehaviour> FindCreatures() {
			return Entities.Where(x => x is CreatureBehaviour).Select(x => x as CreatureBehaviour).ToList();
		}
		
		public IEnumerable<PerkBehaviour> FindPerks() {
			return Entities.Where(x => x is PerkBehaviour).Select(x => x as PerkBehaviour).ToList();
		}

		public IEnumerable<TargetableEntityBehaviour> FindTargetables() {
			return Entities.Where(x => x is TargetableEntityBehaviour).Select(x => x as TargetableEntityBehaviour).ToList();
		}
		
		public TargetableEntityBehaviour FindFittingTargetable(string symbolChain){
			var targets = FindTargetables();
			return targets.FirstOrDefault(x => x.SymbolChain == symbolChain);
		}

		public void SpawnCreature(CreatureTypes type) {
			var creatureSpawn = GameObjectRegistry.GetObject("creature_spawn");
			var creature = DisplayCreature(type, creatureSpawn.transform.position);
			ActivateCreature(creature);
			
			symbolManager.RegisterTargetable(creature);
			entities.Add(creature);
		}
		
		public void SpawnPerk(PerkTypes type) {
			var perkSpawn = GameObjectRegistry.GetObject("perk_spawn");
			var perkImpact = GameObjectRegistry.GetObject("perk_impact");
			var perkRetreat = GameObjectRegistry.GetObject("perk_retreat");
			
			var perk = DisplayPerk(type, perkSpawn.transform.position);
			perk.RemoveController("move");
			perk.RemoveController("float");
			perk.Speed = 240;
			
			var arc = new ArcMovementController(perk, perkImpact, 45);
			arc.ControllerFinished += (sender, e) => {
				perk.Speed = 20;
				perk.MoveTo(perkRetreat.transform.position);
				perk.AddController("impact", new WaterImpactController(Environment.SeaLevel){
					Strength = 9,
					Duration = TimeSpan.FromSeconds(10)
				});
			};
			perk.AddController("move", arc);
			
			symbolManager.RegisterTargetable(perk);
			entities.Add(perk);
		}

		private void ActivateCreature(EntityBehaviour creature) {
			creature
				.SwimTo(Player.gameObject.transform.position)
				.Float();
		}

		public PlayerBehaviour Player {
			get;
			set;
		}
	
		public void VoidTargetable(TargetableEntityBehaviour targetable) {
			if (targetable == null) {
				return;
			}
			GameObject.Destroy(targetable.gameObject);
			entities.Remove(targetable);
		}

		private PerkBehaviour DisplayPerk(PerkTypes type, Vector3 position) {
			var resource = GetPerkResourceByType(type);
			var perkSpawn = GameObjectRegistry.GetObject("perk_spawn");
			var gameObject = GameObject.Instantiate(resource, perkSpawn.transform.position, Quaternion.identity) as GameObject;
			return gameObject.GetComponentInChildren<PerkBehaviour>();
		}

		private CreatureBehaviour DisplayCreature(CreatureTypes type, Vector3 position) {
			var target = Player.transform.position;
			var direction = position - target;
			
			var quaternion = Quaternion.LookRotation(direction, Vector3.up);
			var resource = GetCreatureResourceByType(type);
			var gameObject = GameObject.Instantiate(resource, position, quaternion) as GameObject;
			return gameObject.GetComponentInChildren<CreatureBehaviour>();
		}
		
		private UnityEngine.Object GetPerkResourceByType(PerkTypes type) {
			var name = string.Empty;
			
			switch (type) {
			case PerkTypes.Nuke:
				name = "Perks/Nuke";
				break;
			case PerkTypes.Health:
				name = "Perks/Health";
				break;
			}
			if (string.IsNullOrEmpty(name)) {
				throw new ApplicationException("perk type unknown.");
			}
			
			return Resources.Load(name);
		}

		private UnityEngine.Object GetCreatureResourceByType(CreatureTypes type) {
			var name = string.Empty;
			switch (type) {
			case CreatureTypes.Pike:
				name = "Creatures/Pike";
				break;
			case CreatureTypes.Shark:
				name = "Creatures/Shark";
				break;
			case CreatureTypes.Whale:
				name = "Creatures/Whale";
				break;
			}
			if (string.IsNullOrEmpty(name)) {
				throw new ApplicationException("creature type unknown.");
			}
			return Resources.Load(name);
		}
	}
}

