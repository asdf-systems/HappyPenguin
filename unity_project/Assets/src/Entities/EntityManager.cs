using System;
using System.Linq;
using HappyPenguin.Collections;
using System.Collections.Generic;

namespace HappyPenguin.Entities
{
	public sealed class EntityManager
	{
		private readonly ObservableList<EntityBehaviour> entities;

		public EntityManager () {
			entities = new ObservableList<EntityBehaviour> ();
			entities.ItemRemoved += (sender, e) => OnItemRemoved (e.Item);
			entities.ItemAdded += (sender, e) => OnItemAdded (e.Item);
		}

		public IList<EntityBehaviour> Entities {
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

		private void OnItemAdded (EntityBehaviour entity) {
			if (entity is CreatureBehaviour) {
				VoidCreature (entity as CreatureBehaviour);
			}
			
			if (entity is PerkBehaviour) {
				VoidPerk (entity as PerkBehaviour);
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

		private void VoidCreature (CreatureBehaviour creature) {
			
		}

		private void VoidPerk (PerkBehaviour perk) {
			
		}
	}
}

