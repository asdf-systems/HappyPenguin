using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using HappyPenguin.Spawning;

namespace HappyPenguin.Entities
{
	public sealed class EntityManager
	{
		private SpawnPointBehaviour creatureSpawnPoint;
		private SpawnPointBehaviour PerkSpawnPoint;
		public GameObject PerkRetreatPoint{
			get;
			set;
		}
		private GameObject PerkSpawnTarget;
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
			var creature = DisplayCreature(type, creatureSpawnPoint.Position);
			ActivateCreature(creature);
			
			symbolManager.RegisterTargetable(creature);
			entities.Add(creature);
		}
		
		public void SpawnPerk(PerkTypes type) {
			var perk = DisplayPerk(type, PerkSpawnPoint.Position);
			ActivatePerk(perk);
			symbolManager.RegisterTargetable(perk);
			entities.Add(perk);
		}

		private void ActivateCreature(EntityBehaviour creature) {
			creature.CurrentState = EntityStateGenerator.CreateDefaultMovementState(Player.gameObject, creature.transform.position.y);
		}

		public PlayerBehaviour Player {
			get;
			set;
		}
		
		private void ActivatePerk(TargetableEntityBehaviour perk) {
			perk.CurrentState = EntityStateGenerator.CreatePerkMovementState(perk, PerkSpawnTarget, PerkRetreatPoint);
		}
		
		public void SetPerkSpawnPoint(SpawnPointBehaviour point)
		{
			PerkSpawnPoint = point;
			var patrolBehaviour = point.gameObject.GetComponentInChildren<PatrolBehaviour>();
			if (patrolBehaviour == null) {
				throw new ApplicationException("perk patrol behaviour not found");
			}
			
			var y = patrolBehaviour.Position.y;
			patrolBehaviour.PatrolPositions.Add(new Vector3(90, y, -20));
			patrolBehaviour.PatrolPositions.Add(new Vector3(-20, y, -60));
		}
		
		public void SetPerkSpawnTarget(GameObject point){
			PerkSpawnTarget = point;
		}

		public void SetCreatureSpawnPoint(SpawnPointBehaviour point) {
			creatureSpawnPoint = point;
			var patrolBehaviour = point.gameObject.GetComponentInChildren<PatrolBehaviour>();
			if (patrolBehaviour == null) {
				throw new ApplicationException("creature patrol behaviour not found");
			}
			
			var y = patrolBehaviour.Position.y;
			patrolBehaviour.PatrolPositions.Add(new Vector3(-200, y, 200));
			patrolBehaviour.PatrolPositions.Add(new Vector3(-200, y, -10));
		}

		public void VoidTargetable(TargetableEntityBehaviour targetable) {
			GameObject.Destroy(targetable.gameObject);
			entities.Remove(targetable);
		}

		private PerkBehaviour DisplayPerk(PerkTypes type, Vector3 position) {
			var resource = GetPerkResourceByType(type);
			var gameObject = GameObject.Instantiate(resource, PerkSpawnPoint.Position, Quaternion.identity) as GameObject;
			return gameObject.GetComponentInChildren<PerkBehaviour>();
		}

		private CreatureBehaviour DisplayCreature(CreatureTypes type, Vector3 position) {
			var target = Player.Position;
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

