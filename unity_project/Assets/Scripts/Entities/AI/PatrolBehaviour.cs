using System.Linq;
using UnityEngine;
using HappyPenguin.Unity;
using HappyPenguin.Entities;
using System.Collections;
using System.Collections.Generic;

public sealed class PatrolBehaviour : EntityBehaviour
{
	private int currentTargetIndex;
	private bool isActive;
	
	public List<Vector3> PatrolPositions;
	
	public bool IsActive {
		get{ return isActive; }
		set{
			if (isActive == value) {
				return;
			}
			isActive = value;
			if (isActive) {
				StartOverride();
			}
		}
	}
	
	protected override void AwakeOverride ()
	{
		base.AwakeOverride ();
		currentTargetIndex = 0;
		isActive = false;
		PatrolPositions = new List<Vector3>();
	} 

	protected override void StartOverride() {
		base.StartOverride();
		if (PatrolPositions.Count == 0 || !IsActive) {
			return;
		}
		
		var targetPosition = PatrolPositions[currentTargetIndex];
		this.MoveTo(targetPosition);
	}

	// Update is called once per frame
	protected override void UpdateOverride() {
		base.UpdateOverride();
		if (PatrolPositions.Count == 0 || !IsActive) {
			return;
		}
		
		var targetPosition = PatrolPositions[currentTargetIndex];
		if (!transform.position.IsCloseEnoughTo(targetPosition)) {
			return;
		}
		
		currentTargetIndex++;
		if (currentTargetIndex == PatrolPositions.Count) {
			// start from beginning
			currentTargetIndex = 0;
		}
		
		targetPosition = PatrolPositions[currentTargetIndex];
		this.MoveTo(targetPosition);
	}
}
