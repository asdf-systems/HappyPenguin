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
		private readonly SymbolManager symbolManager;
		private readonly List<EntityBehaviour> entities;

		public EntityManager() {
			entities = new List<EntityBehaviour>();
			symbolManager = new SymbolManager();
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
		
		public Camera PlayerCamera {
			get;
			set;
		}

		public void SpawnCreature(CreatureTypes type) {
			
			var creature = DisplayCreature(type, spawnPoint.Position);
			entities.Add(creature);
		}

		public void SetPlayer(PlayerBehaviour behaviour) {
			player = behaviour;
		}
		
		public void SetSpawnPoint(SpawnPointBehaviour point)
		{
			spawnPoint = point;
		}

		public void SpawnPerk(PerkBehaviour perk) {
			
		}

		private void VoidCreature(CreatureBehaviour creature) {
			
		}

		private void VoidPerk(PerkBehaviour perk) {
			
		}

		public void Update() {
			
		}

		private CreatureBehaviour DisplayCreature(CreatureTypes type, Vector3 position) {
			var target = player.Position;
			var direction = position - target;
			
			var quaternion = Quaternion.LookRotation(direction, Vector3.up);
			var resource = Resources.Load("Creatures/Shark");
			var gameObject = GameObject.Instantiate(resource, position, quaternion) as GameObject;
			
			var component = gameObject.GetComponentInChildren<BillboardBehaviour>();
			component.PlayerCamera = PlayerCamera;
			return gameObject.GetComponentInChildren<CreatureBehaviour>();
		}
	}
}

