using System;
using UnityEngine;



public sealed class CreatureSpawner : HappyPenguin.Spawning.Spawner<HappyPenguin.Entities.CreatureBehaviour>
{

	public double Difficulty { get; private set; }

	public int Credits { get; set; }


	public CreatureSpawner ()
	{
		InvokeEntitySpawned (null);
	}

	void Update ()
	{
		MonoBehaviour.print (Difficulty);
	}

	private bool SpawnReady ()
	{
		double spawn = Difficulty / 5;
		if (spawn >= 1) {
			return true;
		}
		return false;
	}
	
	
}


