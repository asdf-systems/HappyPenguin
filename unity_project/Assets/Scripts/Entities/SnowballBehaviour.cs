using HappyPenguin.Entities;
using System;

public sealed class SnowballBehaviour : EnvironmentEntityBehaviour
{
	public event EventHandler DetachZoneReached;
	internal void InvokeDetachZoneReached() {
		var handler = DetachZoneReached;
		if (handler == null) {
			return;
		}
		handler(this, EventArgs.Empty);
	}
	
	public TargetableEntityBehaviour DedicatedTarget {
		get;
		set;
	}
	
	public bool IsReleased {
		get;
		set;
	}
	
	protected override void UpdateOverride () {
		// creature got disposed while throwing a snowball 
		if (IsReleased && DedicatedTarget == null) {
			this.Dispose();
			return;
		}
		
		base.UpdateOverride ();
	}
}


