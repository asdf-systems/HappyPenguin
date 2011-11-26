using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Pux;
using Pux.Entities;
using Pux.Spawning;
using Pux.Unity;
using Pux.Controllers;
using Pux.Effects;
using Pux.Resources;

namespace Pux.Entities
{
	public sealed class EntityManager
	{
		private readonly TargetableSymbolManager symbolManager;
		private readonly List<EntityBehaviour> entities;

		public event EventHandler<EffectEventArgs> EffectsReleased;
		private void InvokeEffectsReleased(IEnumerable<Effect> effects) {
			var handler = EffectsReleased;
			if (handler == null) {
				return;
			}
			var e = new EffectEventArgs(effects);
			handler(this, e);
		}
		
		public void ReleaseSymbolChain(TargetableEntityBehaviour targetable){
			targetable.HideSymbols();
			symbolManager.VoidTargetable(targetable);
		}

		public void ThrowSnowball(TargetableEntityBehaviour target, float speedMutiplier) {
			var snowball = DisplaySnowball();
			snowball.Speed *= speedMutiplier;
			target.TargetHit += OnTargetHit;
			snowball.DedicatedTarget = target;
			LaunchSnowball(snowball, target);
		}

		private void OnTargetHit(object sender, BehaviourEventArgs<SnowballBehaviour> e) {
			var target = sender as TargetableEntityBehaviour;
			target.TargetHit -= OnTargetHit;
			target.ClearControllers();
			
			// die, snowball, die
			e.Behaviour.Dispose();
			
			InvokeEffectsReleased(target.HitEffects);
		}

		private void LaunchSnowball(SnowballBehaviour snowball, TargetableEntityBehaviour target) {
			//creature got disposed while throwing
			if (target == null) {
				snowball.Dispose();
				return;
			}
			
			var root = GameObjectRegistry.GetObject("entity_root");
			snowball.transform.parent = root.transform;
			snowball.Speed = 700;
			snowball.Throw(target.gameObject);
			snowball.IsReleased = true;
		}

		private SnowballBehaviour DisplaySnowball() {
			
			var instance = ResourceManager.CreateInstance<GameObject>("Environment/Snowball");
			
			instance.transform.parent = Player.rightHandPoint.transform;
			instance.transform.localPosition = Vector3.zero;
			
			var component = instance.GetComponentInChildren<SnowballBehaviour>();
			if (component == null) {
				throw new ApplicationException("SnowballBehaviour not found.");
			}
			
			AddEnvironmentalEntity(component);
			return component;
		}

		public EntityManager() {
			entities = new List<EntityBehaviour>();
			symbolManager = new TargetableSymbolManager();
		}

		public void SpawnLifeBalloon(LifeSpawnBeacon beacon) {
			var host = beacon.UnityGameObject;
			
			var entity = ResourceManager.CreateInstance<GameObject>("Environment/Balloon");
			entity.transform.parent = host.transform;
			entity.transform.localPosition = Vector3.zero;
			entity.transform.localRotation = Quaternion.identity;
		}

		public void ReleaseLifeBalloon(LifeSpawnBeacon beacon) {
			var balloon = beacon.Balloon;
			balloon.transform.parent = null;
			var behaviour = balloon.GetComponent<EnvironmentEntityBehaviour>();
			behaviour.MoveTo(behaviour.transform.position + new Vector3(0, 1000, 0), false);
		}

		public PlayerBehaviour Player { get; set; }

		public IEnumerable<EntityBehaviour> Entities {
			get { return entities; }
		}

		public IEnumerable<CreatureBehaviour> FindCreatures() {
			return Entities.Where(x => x is CreatureBehaviour).Select(x => x as CreatureBehaviour).ToList();
		}

		public IEnumerable<PerkBehaviour> FindPerks() {
			return Entities.Where(x => x is PerkBehaviour).Select(x => x as PerkBehaviour).ToList();
		}

		public IEnumerable<TargetableEntityBehaviour> FindTargetables() {
			return Entities.Where(x => x is TargetableEntityBehaviour).Select(x => x as TargetableEntityBehaviour).ToList();
		}

		public TargetableEntityBehaviour FindTargetable(string symbolChain) {
			var targets = FindTargetables();
			return targets.FirstOrDefault(x => x.SymbolChain == symbolChain);
		}

		public void AddEnvironmentalEntity(EnvironmentEntityBehaviour env) {
			env.GrimReaperAppeared += (sender, e) => VoidEnvironmental(env);
			entities.Add(env);
		}

		private void VoidEnvironmental(EnvironmentEntityBehaviour env) {
			if (env == null) {
				return;
			}
			entities.Remove(env);
			GameObject.Destroy(env.gameObject);
		}
		
		public Range SymbolRangeModifer {
			get;
			set;
		}
		
		public void SpawnCreature(CreatureTypes type) {
			var creatureSpawn = GameObjectRegistry.GetObject("creature_spawn");
			
			var creature = DisplayCreature(type, creatureSpawn.transform.position);
			if (creature == null) {
				throw new ApplicationException(string.Format("Could not create creature of type '{0}'", type));
			}
			creature.GrimReaperAppeared += (sender, e) => VoidTargetable(creature);
			creature.SwimTo(Player.gameObject.transform.position).Float();
			
			
			if (type == CreatureTypes.Blowfish) {
				creature.AttackEffects.Clear();
				creature.HitEffects.Add(new NukeEffect(creature));
			} else {
				creature.HitEffects.Add(new RetreatEffect(creature));
			}
			
			if (type == CreatureTypes.Shark || type == CreatureTypes.Whale) {
				creature.EquipWithRandomBaddy();
			}
			
			AdjustSymbolChainRange(creature);
			symbolManager.RegisterTargetable(creature);
			entities.Add(creature);
		}
		
		private void AdjustSymbolChainRange(TargetableEntityBehaviour behaviour){
			var @from = behaviour.SymbolRange.From + SymbolRangeModifer.From;
			var to = behaviour.SymbolRange.To + SymbolRangeModifer.To;
			if (to < from) {
				to = from;
			}
			behaviour.SymbolRange = new Range(@from, to);
		}

		public void SpawnPerk(PerkTypes type) {
			// outsource into init method
			var perkSpawn = GameObjectRegistry.GetObject("perk_spawn");
			var perkImpact = GameObjectRegistry.GetObject("perk_impact");
			var perkRetreat = GameObjectRegistry.GetObject("perk_retreat");
			
			var perk = DisplayPerk(type, perkSpawn.transform.position);
			perk.GrimReaperAppeared += (sender, e) => VoidTargetable(perk);
			perk.Speed = 240;
			
			
			var arc = new ArcMovementController(perk, perkImpact, 48);
			arc.ControllerFinished += (sender, e) => {
				perk.Speed = 20;
				perk.transform.localRotation = Quaternion.identity;
				perk.MoveTo(perkRetreat.transform.position, false);
				perk.QueueController("impact", new WaterImpactController(Environment.SeaLevel) { Strength = 12, Duration = TimeSpan.FromSeconds(10) });
			};
			perk.QueueController("move", arc);
			
			AdjustSymbolChainRange(perk);
			symbolManager.RegisterTargetable(perk);
			entities.Add(perk);
		}

		public void VoidTargetable(TargetableEntityBehaviour targetable) {
			if (targetable == null) {
				return;
			}
			
			entities.Remove(targetable);
			
			GameObject.Destroy(targetable.gameObject);
		}

		private PerkBehaviour DisplayPerk(PerkTypes type, Vector3 position) {
			
			var perkSpawn = GameObjectRegistry.GetObject("perk_spawn");
			var root = GameObjectRegistry.GetObject("entity_root");
			var gameObject = ResourceManager.CreateInstance<GameObject>("Perks/env_gift_prefab");
			
			var perk = gameObject.GetComponentInChildren<PerkBehaviour>();
			perk.SetMaterial(type);
			switch (type) {
			case PerkTypes.Health:
				
				{
					perk.HitEffects.Add(new LifeEffect(1));
				
					break;
				}

			case PerkTypes.DoublePoints:
				
				{
					perk.HitEffects.Add(new PointsMultiplierEffect(2));
				
					break;
				}

			case PerkTypes.TripplePoints:
				
				{
					perk.HitEffects.Add(new PointsMultiplierEffect(3));
				
					break;
				}

			case PerkTypes.IncreasedBallSpeed:
				
				{
					perk.HitEffects.Add(new SnowballSpeedModiferEffect());
				
					break;
				}

			case PerkTypes.CreatureSlowdown:
				
				{
					perk.HitEffects.Add(new CreatureSlowdownEffect());
					
					break;
				}

			case PerkTypes.LessSymbols:
				
				{
					perk.HitEffects.Add(new LessSymbolsEffect());
					
					break;
				}

			default:
				break;
			}
			
			gameObject.transform.parent = root.transform;
			gameObject.transform.position = perkSpawn.transform.position;
			gameObject.transform.rotation = Quaternion.LookRotation(Vector3.up);
			return perk;
		}

		private CreatureBehaviour DisplayCreature(CreatureTypes type, Vector3 position) {
			var target = Player.transform.position;
			var direction = position - target;
			
			var leveledPosition = new Vector3(position.x, Environment.SeaLevel, position.z);
			var quaternion = Quaternion.LookRotation(direction, Vector3.up);
			var name = GetCreatureResourceByType(type);
			var root = GameObjectRegistry.GetObject("entity_root");
			var gameObject = ResourceManager.CreateInstance<GameObject>(name);
			gameObject.transform.position = leveledPosition;
			gameObject.transform.rotation = quaternion;
			gameObject.transform.parent = root.transform;
			return gameObject.GetComponentInChildren<CreatureBehaviour>();
		}

		private string GetPerkResourceByType(PerkTypes type) {
			var name = string.Empty;
			
			switch (type) {
			case PerkTypes.DoublePoints:
				name = "Perks/gift_blue";
				break;
			case PerkTypes.TripplePoints:
				name = "Perks/gift_purple";
				break;
			case PerkTypes.LessSymbols:
				name = "Perks/gift_black";
				break;
			case PerkTypes.CreatureSlowdown:
				name = "Perks/gift_yellow";
				break;
			case PerkTypes.IncreasedBallSpeed:
				name = "Perks/gift_green";
				break;
			case PerkTypes.Health:
				name = "Perks/gift_red";
				break;
			}
			if (string.IsNullOrEmpty(name)) {
				throw new ApplicationException("perk type unknown.");
			}
			
			return name;
		}

		private string GetCreatureResourceByType(CreatureTypes type) {
			var name = string.Empty;
			switch (type) {
			case CreatureTypes.Pike:
				name = "Creatures/Pike";
				break;
			case CreatureTypes.Shark:
				name = "Creatures/Shark";
				break;
			case CreatureTypes.Whale:
				name = "Creatures/Whale";
				break;
			case CreatureTypes.Blowfish:
				name = "Creatures/Blowfish";
				break;
			}
			if (string.IsNullOrEmpty(name)) {
				throw new ApplicationException("creature type unknown.");
			}
			return name;
		}
	}
}

