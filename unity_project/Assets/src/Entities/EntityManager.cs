using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace HappyPenguin.Entities
{
	public sealed class EntityManager
	{
		private readonly SpawnPointGenerator spawnPointGenerator;
		private readonly SymbolManager symbolManager;
		private readonly List<EntityBehaviour> entities;

		public EntityManager () {
			spawnPointGenerator = new SpawnPointGenerator();
			entities = new List<EntityBehaviour>();
			symbolManager = new SymbolManager();
		}

		public IEnumerable<EntityBehaviour> Entities {
			get { return entities; }
		}

		public IEnumerable<CreatureBehaviour> FindCreatures () {
			return Entities.Where (x => x is CreatureBehaviour).Select (x => x as CreatureBehaviour).ToList ();
		}

		public IEnumerable<PerkBehaviour> FindPerks () {
			return Entities.Where (x => x is PerkBehaviour).Select (x => x as PerkBehaviour).ToList ();
		}

		public IEnumerable<TargetableEntityBehaviour> FindTargetables () {
			return Entities.Where (x => x is TargetableEntityBehaviour).Select (x => x as TargetableEntityBehaviour).ToList ();
		}

		public void SpawnCreature (CreatureBehaviour creature) {
			var position = spawnPointGenerator.CreateNext();
			entities.Add(creature);
			DisplayEntity(creature, position);
		}
		
		
		
		public void SpawnPerk (PerkBehaviour perk) {
			
		}

		private void VoidCreature (CreatureBehaviour creature) {
			
		}

		private void VoidPerk (PerkBehaviour perk) {
			
		}
		
		public void Update()
		{
			
		}
		
		private void DisplayEntity(EntityBehaviour entity, Vector3 position)
		{
			var quaternion = new Quaternion();
			var resource = Resources.Load("shark");
			GameObject.Instantiate(resource, position, quaternion);
		}
	}
}

