using System;
using UnityEngine;
using Pux.Spawning;
using Pux.Entities;


public sealed class CreatureSpawner : Spawner<CreatureTypes>
{
//	Constants
	public static int DEFAULT_CREDIT_SPAWN_TIME = 5;
	// SpawnTime in Seconds
	public static int DEFAULT_CREATURE_SPAWN_TIME = 4;
	// SpawnTime in Seconds
	public static int DIFFICULTY_CREDIT_MULTIPLIER = 5;

//	Fields & Properties
	public double Difficulty;
	public int CreditsTotal = 6;
	// init
	private TimeSpan timeSinceLastCreditGift = TimeSpan.Zero;
	private TimeSpan timeSinceLastSpawn = TimeSpan.Zero;

	private readonly System.Random random;

	private int spawnCounter;
	// How many Spawns are on the Battlefield?
//	Constructor
	public CreatureSpawner() {
		random = new System.Random();
	}

//	Update

	public void Update() {
		Difficulty = CalculateDifficulty(Time.timeSinceLevelLoad);
		
		CalculateCredits();
		
		SpawnCreatures();
	}


//	Insert Difficulty-Curve here
	private double CalculateDifficulty(float time) {
		return Math.Sin(Time.timeSinceLevelLoad) + 1;
	}

//	How many Credits do I recieve?
	private void CalculateCredits() {
		timeSinceLastCreditGift = timeSinceLastCreditGift.Add(TimeSpan.FromSeconds((double)Time.deltaTime));
		if (timeSinceLastCreditGift.TotalSeconds >= DEFAULT_CREDIT_SPAWN_TIME) {
			CreditsTotal += UnityEngine.Random.Range(1, (10 + DIFFICULTY_CREDIT_MULTIPLIER * (int)Difficulty));
			timeSinceLastCreditGift = TimeSpan.Zero;
			//Debug.Log("Credits: " + CreditsTotal);
		}
	}

//	What Creature do I spawn?
	private void SpawnCreatures() {
		timeSinceLastSpawn = timeSinceLastSpawn.Add(TimeSpan.FromSeconds((double)Time.deltaTime));
		spawnCounter = InvokeCreatureCountNeeded();
		if(timeSinceLastSpawn.TotalSeconds >= (DEFAULT_CREATURE_SPAWN_TIME - Difficulty)) {
			int rnd = random.Next(1, 100);
			
			if (1 <= rnd && rnd <= 50) {
				InvokeEntitySpawned(CreatureTypes.Pike);
			} else if (51 <= rnd && rnd <= 80) {
				InvokeEntitySpawned(CreatureTypes.Shark);
			} else if (81 <= rnd && rnd <= 95) {
				InvokeEntitySpawned(CreatureTypes.Whale);
			} else {
				InvokeEntitySpawned(CreatureTypes.Blowfish);
			}
			timeSinceLastSpawn = TimeSpan.Zero;
		}
	}

//	Can I afford a Pike?
	private void SpawnPike() {
		int spawningPrice = 3;
		if (CreditsTotal >= spawningPrice) {
			InvokeEntitySpawned(CreatureTypes.Pike);
			CreditsTotal -= spawningPrice;
		}
	}

//	Can I afford a Shark?
	private void SpawnShark() {
		int spawningPrice = 6;
		if (CreditsTotal >= spawningPrice) {
			InvokeEntitySpawned(CreatureTypes.Shark);
			CreditsTotal -= spawningPrice;
		}
	}

//	Can I afford a Whale?
	private void SpawnWhale() {
		int spawningPrice = 10;
		if (CreditsTotal >= spawningPrice) {
			InvokeEntitySpawned(CreatureTypes.Whale);
			CreditsTotal -= spawningPrice;
		}
	}


//	CreatureCountNeeded
	public event EventHandler<CreatureCountNeededEventArgs> CreatureCountNeeded;

	private int InvokeCreatureCountNeeded() {
		var handler = CreatureCountNeeded;
		if (handler == null) {
			return -1;
		}
		var e = new CreatureCountNeededEventArgs();
		CreatureCountNeeded(this, e);
		return e.CreatureCount;
	}
	
}


