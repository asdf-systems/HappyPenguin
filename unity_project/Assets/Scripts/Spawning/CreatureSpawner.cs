using System;
using UnityEngine;
using HappyPenguin.Spawning;
using HappyPenguin.Entities;


public sealed class CreatureSpawner : Spawner<CreatureTypes>
{
//	Constants
	public static int DEFAULT_CREDIT_SPAWN_TIME = 5;	// SpawnTime in Seconds
	public static int DEFAULT_CREATURE_SPAWN_TIME = 5;	// SpawnTime in Seconds
	public static int DIFFICULTY_CREDIT_MULTIPLIER = 5;
	public static int MAX_CREATURE_COUNT = 10;
	
//	Fields & Properties
//	private double spawn = 1.999;
	public double Difficulty;
	public int CreditsTotal = 6;	// init
	private TimeSpan timeSinceLastCreditGift = TimeSpan.Zero;
	private TimeSpan timeSinceLastSpawn = TimeSpan.Zero;
	
	private int spawnCounter = 0;	// How many Spawns are on the Battlefield?
	
//	Constructor
	public CreatureSpawner ()
	{
		
	}
	
	
//	Update
	public override void Update (){
		timeSinceLastCreditGift = timeSinceLastCreditGift.Add(TimeSpan.FromSeconds((double)Time.deltaTime));
		timeSinceLastSpawn = timeSinceLastSpawn.Add(TimeSpan.FromSeconds((double)Time.deltaTime));
		
		Difficulty = CalculateDifficulty(Time.timeSinceLevelLoad);
		
		CalculateCredits();
		
		SpawnCreatures();
	}

//	private bool SpawnReady ()
//	{
//		if (Difficulty>=spawn) {
//			return true;
//		}
//		return false;
//	}	// wird grad nicht gebraucht
	
//	Insert Difficulty-Curve here
	private double CalculateDifficulty(float time){
		return Math.Sin(Time.timeSinceLevelLoad)+1;
	}
	
//	How many Credits do I recieve?
	private void CalculateCredits(){
		if (timeSinceLastCreditGift.TotalSeconds >= DEFAULT_CREDIT_SPAWN_TIME) {
			CreditsTotal += UnityEngine.Random.Range(1,(10 + DIFFICULTY_CREDIT_MULTIPLIER * (int)Difficulty));
			timeSinceLastCreditGift = TimeSpan.Zero;
			Debug.Log("Credits: " + CreditsTotal);
		}	
	}
	
//	What Creature do I spawn?
	private void SpawnCreatures(){
		if (spawnCounter < MAX_CREATURE_COUNT && 
		    timeSinceLastSpawn.TotalSeconds >= (DEFAULT_CREATURE_SPAWN_TIME - Difficulty)) {
			int rnd = UnityEngine.Random.Range(1, 10);
			
			if (1 <= rnd && rnd <=4) {
				SpawnPike();
			}
			else if (5 <= rnd && rnd <=7) {
				SpawnShark();
			}
			else {
				SpawnWhale();
			}	
			timeSinceLastSpawn = TimeSpan.Zero;
		}
	}
	
//	Can I afford a Pike?
	private void SpawnPike(){
		int spawningPrice = 3;
		if (CreditsTotal >= spawningPrice) {
			InvokeEntitySpawned(CreatureTypes.Pike);
			Debug.Log("Pike spawned. Credits lost: " + spawningPrice + " after " + Time.timeSinceLevelLoad);
			CreditsTotal -= spawningPrice;
			++spawnCounter;
		}
	}
	
//	Can I afford a Shark?
	private void SpawnShark(){
		int spawningPrice = 6;
		if (CreditsTotal >= spawningPrice) {
			InvokeEntitySpawned(CreatureTypes.Shark);
			Debug.Log("Shark spawned. Credits lost: " + spawningPrice + " after " + Time.timeSinceLevelLoad);
			CreditsTotal -= spawningPrice;
			++spawnCounter;
		}
	}
	
//	Can I afford a Whale?
	private void SpawnWhale(){
		int spawningPrice =10;
		if (CreditsTotal >= spawningPrice) {
			InvokeEntitySpawned(CreatureTypes.Whale);
			Debug.Log("Whale spawned. Credits lost: " + spawningPrice + " after " + Time.timeSinceLevelLoad);
			CreditsTotal -= spawningPrice;
			++spawnCounter;
		}
	}
	
	
//	CreatureCountNeeded
	public event EventHandler<CreatureCountNeededEventArgs> CreatureCountNeeded;
	
	private int InvokeCreatureCountNeeded(){
		var handler = CreatureCountNeeded;
		if (handler == null) {
			return -1;
		}
		var e = new CreatureCountNeededEventArgs();
		CreatureCountNeeded(this , e);
		return e.CreatureCount;
	}
	
}


