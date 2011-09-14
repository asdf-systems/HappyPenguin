using System;
using HappyPenguin.Entities;

namespace HappyPenguin.Spawning
{
	public sealed class CreatureSpawner : Spawner<CreatureBehaviour>
	{
		public CreatureSpawner () {
			InvokeEntitySpawned(null);
		}
	}
}

