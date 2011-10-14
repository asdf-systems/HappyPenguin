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
}


