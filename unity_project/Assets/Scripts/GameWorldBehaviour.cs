using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using HappyPenguin.Entities;
using HappyPenguin.Effects;
using HappyPenguin.Spawning;
using HappyPenguin;

public sealed class GameWorldBehaviour : MonoBehaviour
{
	private Camera _playerCamera;
	public GUIManager guiManager;
	public GameObject RetreatPoint;
	public GameObject PerkRetreatPoint;
	public GameObject PerkSpawnTarget;
	public GameObject Trebuchet;
	public int CreatureCount;
	private readonly EffectManager effectManager;

	private readonly CreatureSpawner creatureSpawner;
	private readonly PerkSpawner perkSpawner;
	private readonly TargetableSymbolManager symbolManager;
	
	public EntityManager entityManager{ get; private set;}

	private AttackZoneBehaviour attackZone;
	
	public string PerkText;
	public string WrongSymbolChainText;
	public string LooseText;

	public GameWorldBehaviour() {
		entityManager = new EntityManager();
		effectManager = new EffectManager(this);
		
		creatureSpawner = new CreatureSpawner();
		creatureSpawner.EntitySpawned += OnCreatureGenerated;
		creatureSpawner.CreatureCountNeeded += OnCreatureCountNeeded;
		
		perkSpawner = new PerkSpawner();
		perkSpawner.EntitySpawned += OnPerkGenerated;
		
	}

	void OnSwipeCommitted(object sender, SwipeEventArgs e) {
		entityManager.Player.PlayAnimation("throw");
		guiManager.clearSymbols();
		
		var target = entityManager.FindFittingTargetable(e.symbolChain);
		if (target == null) {
			guiManager.alert(WrongSymbolChainText);
			Debug.Log("implemented punish player");
			return;
		}
		
		
		
		List<Effect> killEffects = target.CollectedEffects;
		foreach (Effect effect in killEffects) {
			effectManager.RegisterEffect(effect);
		}		
	}

	void OnAttackZoneEntered(object sender, AttackZoneEventArgs e) {
		var creature = e.Creature.GetComponent<CreatureBehaviour>();
		if (creature != null) {
			var attackEffects = creature.NotCollectedEffects;
			foreach (var effect in attackEffects) {
				effectManager.RegisterEffect(effect);
			}
		}
	}
	
	private void InitAttackZone() {
		attackZone = gameObject.GetComponentInChildren<AttackZoneBehaviour>();
		if (attackZone == null) {
			Debug.LogError("No AttackZone found in Gameworld");
		}
		
		attackZone.AttackZoneEntered += OnAttackZoneEntered;
		
	}
	


	public void Awake() {
		InitPlayer();
		InitCreatureSpawningNode();
		InitPerkNodes();
		InitAttackZone();
		InitUI();
	}
	
	private void InitUI()
	{
		guiManager.changePoints(entityManager.Player.Points);
		guiManager.changeLife(entityManager.Player.Life);
		guiManager.SwipeCommitted += OnSwipeCommitted;
	}
	
	private void InitPerkNodes()
	{
		var spawnPoint = gameObject.GetComponentsInChildren<SpawnPointBehaviour>().FirstOrDefault(x => x.Key == "perks");
		var perkRetreatPoint = gameObject.GetComponentInChildren<PerkRetreatPointBehaviour>();
		
		if (spawnPoint == null) {
			throw new ApplicationException("spawn point component not found");
		}
		
		if(perkRetreatPoint == null){
			Debug.LogError("No Perk retreatpoint found");
		}
		
		entityManager.SetPerkSpawnPoint(spawnPoint);
		entityManager.SetPerkSpawnTarget(PerkSpawnTarget);
		entityManager.PerkRetreatPoint = PerkRetreatPoint;
		
		perkRetreatPoint.PerkRetreatPointReached += OnPerkReatreatPointReached;
	}
	
	private void OnPerkReatreatPointReached(object sender, AttackZoneEventArgs e){
		foreach(Effect effect in e.Creature.NotCollectedEffects){
			effectManager.RegisterEffect(effect);	
		}
	}

	private void InitPlayer() {
		var player = gameObject.GetComponentInChildren<PlayerBehaviour>();
		if (player == null) {
			throw new ApplicationException("player component not found");
		}
		
		player.StartLife = 5;
		player.StartPoints = 0;
		var state =  new EntityState("player_idle");
		state.AnimationNames.Add("idle");
		player.CurrentState = state;
		entityManager.Player = player;
		entityManager.Player.Life = 5;
		guiManager.changeLife(entityManager.Player.Life);
	}

	private void InitCreatureSpawningNode() {
		var spawnPoint = gameObject.GetComponentsInChildren<SpawnPointBehaviour>().FirstOrDefault(x => x.Key == "creatures");
		if (spawnPoint == null) {
			throw new ApplicationException("spawn point component not found");
		}
		
		entityManager.SetCreatureSpawnPoint(spawnPoint);
	}

	private void OnCreatureGenerated(object sender, EntityGeneratedEventArgs<CreatureTypes> e) {
		entityManager.SpawnCreature(e.EntityType);
		CreatureCount++;
	}


	private void OnPerkGenerated(object sender, EntityGeneratedEventArgs<PerkTypes> e) {
		if (e.EntityType == PerkTypes.Health) {
			PerkText = "More Health, yay!";
		}
		else {
			PerkText = "More Nuke, yay!";
		}
		entityManager.SpawnPerk(e.EntityType);
		Trebuchet.animation.Play("shoot");
		Trebuchet.animation.PlayQueued("pull");
	}

	public void ChangePlayerHealth(float lifeChange) {
		var player = gameObject.GetComponentInChildren<PlayerBehaviour>();
		if (lifeChange > 0) {
			if (entityManager.Player.Life + lifeChange <= 5) {
				entityManager.Player.Life += lifeChange;
				guiManager.changeLife(entityManager.Player.Life);
				guiManager.alert("++ Health");
			}
			else if (entityManager.Player.Life == 5) {
				guiManager.alert("Already at full Health");
			}
			else {
				entityManager.Player.Life = 5;
				guiManager.changeLife(entityManager.Player.Life);
				guiManager.alert("++ Health");
			}
		}
		else {
			entityManager.Player.Life += lifeChange;
			guiManager.changeLife(entityManager.Player.Life);
			guiManager.alert("-- Health");
		}
			player.audio.clip = player.AttackSound;
			player.audio.Play();
				
		if (entityManager.Player.IsDead) {
			guiManager.alert(LooseText);
			PlayerBehaviour.FinalPoints = entityManager.Player.Points;
			Application.LoadLevel(2);
		}
	}
	
	public void OnCreatureCountNeeded(object sender, CreatureCountNeededEventArgs e){
		e.CreatureCount = CreatureCount;
	}

	public void ChangePlayerPoints(float pointsChange) {
		guiManager.alert("+ " + pointsChange + " Points");
		entityManager.Player.Points += pointsChange;
		guiManager.changePoints(entityManager.Player.Points);
	}

	public void Update() {
		creatureSpawner.Update();
		effectManager.Update();
		perkSpawner.Update();
	}
}
