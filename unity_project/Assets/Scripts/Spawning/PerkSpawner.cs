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

	public static int DEFAULT_PERK_SPAWN_TIME = 10;

	public PerkSpawner() {
		spawnList.Add(SpawnHealth);
		spawnList.Add(SpawnHealth);
		spawnList.Add(SpawnHealth);
		spawnList.Add(SpawnNuke);
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

	private void SpawnNuke() {
		InvokeEntitySpawned(PerkTypes.Nuke);
	}
}
