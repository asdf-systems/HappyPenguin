using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using HappyPenguin.Spawning;

namespace HappyPenguin.Entities
{
	public sealed class EntityManager
	{
		private SpawnPointBehaviour spawnPoint;
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
			var creature = DisplayCreature(type, spawnPoint.Position);
			ActivateCreature(creature);
//			switch (type) {
//				case(CreatureTypes.Shark):
//					creature.Sounds.Add("AttackSound",new AudioClip());
//					creature.Sounds.Add("SwimSound", new AudioClip());
//					creature.Sounds.Add("DeathSound", new AudioClip());
//					break;
//				case(CreatureTypes.Pike):
//					creature.Sounds.Add("AttackSound",new AudioClip());
//					creature.Sounds.Add("SwimSound", new AudioClip());
//					creature.Sounds.Add("DeathSound", new AudioClip());
//					break;
//				case(CreatureTypes.Whale):
//					creature.Sounds.Add("AttackSound",new AudioClip());
//					creature.Sounds.Add("SwimSound", new AudioClip());
//					creature.Sounds.Add("DeathSound", new AudioClip());
//					break;
//			}	
			
			symbolManager.RegisterTargetable(creature);
			entities.Add(creature);
			
		}
		
		public void SpawnPerk(PerkTypes type) {
			var perk = DisplayPerk(type, spawnPoint.Position);
			ActivatePerk(perk);
			symbolManager.RegisterTargetable(perk);
			entities.Add(perk);
		}

		private void ActivateCreature(EntityBehaviour creature) {
			creature.audio.clip = creature.AttackSound;
			creature.audio.Play();
			creature.CurrentState = EntityStateGenerator.CreateDefaultMovementState(Player, creature.transform.position.y);
		}

		public PlayerBehaviour Player {
			get;
			set;
		}
		
		private void ActivatePerk(EntityBehaviour perk) {
			if (perk == null) {
				Debug.Log("perk is still null, needs implementing.");
				return;
			}
			perk.CurrentState = EntityStateGenerator.CreatePerkMovementState(new Vector3(0,0,0));
		}

		public void SetSpawnPoint(SpawnPointBehaviour point) {
			spawnPoint = point;
			var patrolBehaviour = point.gameObject.GetComponentInChildren<PatrolBehaviour>();
			if (patrolBehaviour == null) {
				throw new ApplicationException("patrol behaviour not found");
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
			//TODO implement stuff!
			
//			var target = player.Position;
//			var direction = position - target;
//			
//			var quaternion = Quaternion.LookRotation(direction, Vector3.up);
//			var resource = GetCreatureResourceByType(type);
//			var gameObject = GameObject.Instantiate(resource, position, quaternion) as GameObject;
//			return gameObject.GetComponentInChildren<CreatureBehaviour>();
			Debug.Log("Display Perk - TODO: Implement Stuff");
			return null;
		}

		private CreatureBehaviour DisplayCreature(CreatureTypes type, Vector3 position) {
			var target = Player.Position;
			var direction = position - target;
			
			var quaternion = Quaternion.LookRotation(direction, Vector3.up);
			var resource = GetCreatureResourceByType(type);
			var gameObject = GameObject.Instantiate(resource, position, quaternion) as GameObject;
			return gameObject.GetComponentInChildren<CreatureBehaviour>();
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

