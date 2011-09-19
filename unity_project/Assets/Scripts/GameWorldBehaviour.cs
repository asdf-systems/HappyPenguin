using UnityEngine;
using System;
using System.Collections.Generic;
using HappyPenguin.Entities;
using HappyPenguin.Effects;
using HappyPenguin.Spawning;
using HappyPenguin;

public sealed class GameWorldBehaviour : MonoBehaviour
{

	

	private Camera _playerCamera;
	

	private readonly EffectManager effectManager;
	public GUIManager guiManager;

	private readonly CreatureSpawner creatureSpawner;
	//private readonly PerkSpawner perkSpawner;
	private readonly TargetableSymbolManager symbolManager;
	private readonly EntityManager entityManager;
	
	
	private AttackZoneBehaviour attackZone;
	private PlayerBehaviour player;
	
	public string GamePlayFunction;

	public GameWorldBehaviour() {
		entityManager = new EntityManager();
		effectManager = new EffectManager();
		
		creatureSpawner = new CreatureSpawner();
		creatureSpawner.EntitySpawned += OnCreatureGenerated;
		
		
		//perkSpawner = new PerkSpawner();

	}
	
	void Start(){
		attackZone = gameObject.GetComponentInChildren<AttackZoneBehaviour>();
		if(attackZone == null){
			Debug.LogError("No AttackZone found under Gameworld");
		}
		player = gameObject.GetComponentInChildren<PlayerBehaviour>();
		if(player == null){
			Debug.LogError("No Player found under Gameworld");
		}
		
		attackZone.EnemyEnteredAttackZone += OnEnemyEnterAttackZone; 
		guiManager.SwipeCommitted += OnSwipeCommitted;
	}

	void OnSwipeCommitted(object sender, SwipeEventArgs e){
		TargetableEntityBehaviour target = entityManager.FindFittingTargetable(e.symbolChain);
		if (target == null) {
			return;
		}
		// TODO implement
		Debug.Log("Swipe Commit - TODO: Implement Stuff");

	}

	void OnEnemyEnterAttackZone(object sender, AttackZoneEventArgs e){
		CreatureBehaviour creature = e.enemy.GetComponent<CreatureBehaviour>();
		if(creature != null){
			List<Effect> attackEffects = creature.AttackEffects;
			Debug.Log("Creature Found");
			foreach(Effect effect in attackEffects){
				Debug.Log("Register Effect");
				effectManager.RegisterEffect(effect);		
				// TODO: Implement Retreat
			}
			
		}
		
		                             
		
	}

	public void Awake() {
		InitPlayer();
		InitSpawnPoint();
	}
	
	private void InitPlayer() {
		var player = gameObject.GetComponentInChildren<PlayerBehaviour>();
		if (player == null) {
			throw new ApplicationException("player component not found");
		}
		
		entityManager.SetPlayer(player);
	}
	
	private void InitSpawnPoint()
	{
		var spawnPoint = gameObject.GetComponentInChildren<SpawnPointBehaviour>();
		if (spawnPoint == null) {
			throw new ApplicationException("spawn point component not found");
		}
		
		entityManager.SetSpawnPoint(spawnPoint);
	}

	private void OnCreatureGenerated(object sender, EntityGeneratedEventArgs<CreatureTypes> e) {
		entityManager.SpawnCreature(e.EntityType);
	}
	
	public void ChangePlayerHealth (float lifeChange){
		player.life += lifeChange;
		Debug.Log("Health modified: " + lifeChange);
		if(player.isDead()){
			Debug.Log("YOU SUCK!!");
			Application.LoadLevel(2);
		}
	}
	
	public void ChangePlayerPoints(float pointsChange){
		Debug.Log("points modified: " + pointsChange);
		player.points += pointsChange;
	}
		
	public void Update() {
		creatureSpawner.Update();
		effectManager.Update(this);
	}
}
