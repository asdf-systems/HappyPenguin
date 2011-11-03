using System;
using System.Collections.Generic;
using UnityEngine;
using Pux.Spawning;
using Pux.Entities;


public sealed class PerkSpawner : Spawner<PerkTypes>
{
	private System.Random random = new System.Random();
	private TimeSpan timeSinceLastSpawn = TimeSpan.Zero;
	private List<Action> spawnList = new List<Action>();

	public static int DEFAULT_PERK_SPAWN_TIME = 20;

	public PerkSpawner() {
		spawnList.Add(SpawnDoublePoints);
		spawnList.Add(SpawnTripplePoints);
		spawnList.Add(SpawnHealth);
		spawnList.Add(SpawnIncreasedBallSpeed);
		spawnList.Add(SpawnLessSymbols);
		spawnList.Add(SpawnCreatureSlowdown);
	}

	public void Update() {
		SpawnPerk();
	}

	private double calculateSpawnTime() {
		return (DEFAULT_PERK_SPAWN_TIME + (random.NextDouble() * 20));
	}

	private void SpawnPerk() {
		timeSinceLastSpawn = timeSinceLastSpawn.Add(TimeSpan.FromSeconds((double)Time.deltaTime));
		double spawnTime = calculateSpawnTime();
		
		if (timeSinceLastSpawn.TotalSeconds >= spawnTime) {
			int rnd = random.Next(0, spawnList.Count);
			spawnList[rnd]();
			timeSinceLastSpawn = TimeSpan.Zero;
		}
	}
	private void SpawnHealth() {
		InvokeEntitySpawned(PerkTypes.Health);
	}

	private void SpawnDoublePoints() {
		InvokeEntitySpawned(PerkTypes.DoublePoints);
	}
	
	private void SpawnTripplePoints() {
		InvokeEntitySpawned(PerkTypes.TripplePoints);
	}
	
	private void SpawnCreatureSlowdown() {
		InvokeEntitySpawned(PerkTypes.CreatureSlowdown);
	}
	
	private void SpawnIncreasedBallSpeed() {
		InvokeEntitySpawned(PerkTypes.IncreasedBallSpeed);
	}
	
	private void SpawnLessSymbols() {
		InvokeEntitySpawned(PerkTypes.LessSymbols);
	}
}
