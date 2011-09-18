using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using HappyPenguin.Spawning;

namespace HappyPenguin.Entities
{
	public sealed class EntityManager
	{
		private PlayerBehaviour player;
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

		public void SpawnCreature(CreatureTypes type) {
			var creature = DisplayCreature(type, spawnPoint.Position);
			ActivateCreature(creature);
			symbolManager.RegisterTargetable(creature);
			entities.Add(creature);
		}

		private void ActivateCreature(EntityBehaviour creature) {
			creature.CurrentState = EntityStateGenerator.CreateDefaultMovementState(player);
		}

		public void SetPlayer(PlayerBehaviour behaviour) {
			player = behaviour;
		}

		public void SetSpawnPoint(SpawnPointBehaviour point) {
			spawnPoint = point;
			var patrolBehaviour = point.gameObject.GetComponentInChildren<PatrolBehaviour>();
			if (patrolBehaviour == null) {
				throw new ApplicationException("patrol behaviour not found");
			}
			patrolBehaviour.PatrolPositions.Add(new Vector3(-200, 0, 200));
			patrolBehaviour.PatrolPositions.Add(new Vector3(-200, 0, -10));
			patrolBehaviour.PatrolPositions.Add(new Vector3(65, 0, -120));
			patrolBehaviour.PatrolPositions.Add(new Vector3(-200, 0, -10));
		}

		public void SpawnPerk(PerkBehaviour perk) {
			
		}

		private void VoidCreature(CreatureBehaviour creature) {
			
		}

		private void VoidPerk(PerkBehaviour perk) {
			
		}

		private CreatureBehaviour DisplayCreature(CreatureTypes type, Vector3 position) {
			var target = player.Position;
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

