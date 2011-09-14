using System;
using System.Linq;
using HappyPenguin.Collections;
using System.Collections.Generic;

namespace HappyPenguin.Entities
{
	public sealed class EntityManager
	{
		public EntityManager () {
			Entities = new ObservableList<EntityBehaviour> ();
			//Entities.ListChanged += OnListChanged;
		}

		public IList<EntityBehaviour> Entities { get; private set; }

		public IEnumerable<CreatureBehaviour> GetCreatures () {
			return Entities.Where (x => x is CreatureBehaviour).Select (x => x as CreatureBehaviour).ToList ();
		}

		public IEnumerable<PerkBehaviour> GetPerks () {
			return Entities.Where (x => x is PerkBehaviour).Select (x => x as PerkBehaviour).ToList ();
		}

	

		private void OnItemAdded (EntityBehaviour entity) {
			if (entity is CreatureBehaviour) {
				DeleteCreature(entity as CreatureBehaviour);
			}
			
			if (entity is PerkBehaviour) {
				DeletePerk(entity as PerkBehaviour);
			}
		}

		private void OnItemRemoved (EntityBehaviour entity) {
			if (entity is CreatureBehaviour) {
				SpawnCreature (entity as CreatureBehaviour);
				return;
			}
			
			if (entity is PerkBehaviour) {
				SpawnPerk (entity as PerkBehaviour);
			}
		}

		private void SpawnCreature (CreatureBehaviour creature) {
			
		}

		private void SpawnPerk (PerkBehaviour perk) {
			
		}
		
		private void DeleteCreature(CreatureBehaviour creature)
		{
			
		}
		
		private void DeletePerk(PerkBehaviour perk)
		{
			
		}
	}
}

