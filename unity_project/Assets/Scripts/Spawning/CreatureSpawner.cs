using System;
using UnityEngine;
using Pux.Spawning;
using Pux.Entities;

public struct ItemProbability {
	public CreatureTypes item;
	public double probability;
	public ItemProbability(CreatureTypes t, double p) {
		item = t;
		probability = p;
	}
}

public sealed class CreatureSpawner : Spawner<CreatureTypes>
{
	//	Constants
	// SpawnTime in Seconds
	public static double DEFAULT_CREATURE_SPAWN_TIME = 5;
	public static double DEFAULT_CREATURE_SPAWN_TIME_DEVIATION = 0.3;

	// Factor to converge towards
	public static double MIN_FACTOR = 0.4
	// Period (=1/frequency) of the wave function (seconds)
	public static double WAVE_PERIOD = 30;
	public static double WAVE_AMPLITUDE = 0.05;
	// How fast is it going to get harder?
	// Sorry, no actual units...
	public static double STEEPNESS = 0.05;

	// This defines a curve which is a factor for nextSpawn.
	// If you want to modify it, go to
	// http://www.arndt-bruenner.de/mathe/java/plotter.htm
	public double DifficultyFactor {
		get {
			var x = Time.timeSinceLevelLoad;
			return 1-(1-MIN_FACTOR)/(1+exp(-(1-MIN_FACTOR)*STEEPNESS*x)*((1-MIN_FACTOR)/0.01 - 1))+(cos(x/WAVE_PERIOD*2*pi)-1)*WAVE_AMPLITUDE;
		}
	}

	// These 2 functions only exists for copy'n'pasteability of the
	// formula above
	private static double pi = Math.PI;
	private static double exp(double d) {
		return Math.Exp(d);
	}

	private static double cos(double d) {
		return Math.Cos(d);
	}

	// SpawnTime in Seconds
	public static double DIFFICULTY_CREDIT_MULTIPLIER = 5;

//	Fields & Properties
	public double Difficulty;
	public int CreditsTotal = 6;
	// init
	private TimeSpan timeSinceLastCreditGift = TimeSpan.Zero;
	private TimeSpan timeSinceLastSpawn = TimeSpan.Zero;
	private double nextSpawn;

	private NormalDistributedRandom ndr;
	private readonly System.Random random;

	// How many Spawns are on the Battlefield?
//	Constructor
	public CreatureSpawner() {
		random = new System.Random();
		ndr = new NormalDistributedRandom(DEFAULT_CREATURE_SPAWN_TIME, DEFAULT_CREATURE_SPAWN_TIME_DEVIATION);
		nextSpawn = ndr.Next();
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
		/*timeSinceLastCreditGift = timeSinceLastCreditGift.Add(TimeSpan.FromSeconds((double)Time.deltaTime));
		if (timeSinceLastCreditGift.TotalSeconds >= DEFAULT_CREDIT_SPAWN_TIME) {
			CreditsTotal += UnityEngine.Random.Range(1, (10 + DIFFICULTY_CREDIT_MULTIPLIER * (int)Difficulty));
			timeSinceLastCreditGift = TimeSpan.Zero;
			//EditorDebug.Log("Credits: " + CreditsTotal);
		}*/
	}

	private	static ItemProbability[] items = {
			new ItemProbability(CreatureTypes.Shark, 0.5),
			new ItemProbability(CreatureTypes.Pike, 0.3),
			new ItemProbability(CreatureTypes.Whale, 0.15),
			new ItemProbability(CreatureTypes.Blowfish, 0.05),
		};
//	What Creature do I spawn?
	private void SpawnCreatures() {

		timeSinceLastSpawn = timeSinceLastSpawn.Add(TimeSpan.FromSeconds((double)Time.deltaTime));

		if(timeSinceLastSpawn.TotalSeconds >= nextSpawn*DifficultyFactor) {
			double rnd = random.NextDouble();
			double cum = 0;
			foreach(var i in items) {
				cum += i.probability;
				if(rnd <= cum) {
					InvokeEntitySpawned(i.item);
					break;
				}
			}
			timeSinceLastSpawn = TimeSpan.Zero;
			nextSpawn = ndr.Next();
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


