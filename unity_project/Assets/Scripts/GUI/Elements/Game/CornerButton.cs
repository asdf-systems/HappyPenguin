using UnityEngine;
using System.Collections;
using Pux.Controllers;

public sealed class CornerButton : Button
{
	private ControlManager<CornerButton> controlManager;
	public CornerButton() {
		controlManager = new ControlManager<CornerButton>();
	}

	public void QueueController(string name, Controller<CornerButton> controller) {
		controlManager.QueueController(name, controller);
	}

	public void ClearController() {
		controlManager.ClearControllers();
	}

	public void DequeueController(string name) {
		controlManager.DequeueController(name);
	}

	protected override void UpdateOverride() {
		base.UpdateOverride();
		controlManager.Update(this);
	}

	protected override void AwakeOverride() {
		base.AwakeOverride();
		StoreDefaultPosition();
	}

	public string Symbol;

	public override void OnClick(object sender, MouseEventArgs e) {
		base.OnClick(sender, e);
		if (Symbol == string.Empty)
			EditorDebug.LogWarning("Button " + gameObject.name + " has no symbol set!");
		
		GUIManager.Instance.NotifyButtonHit(Symbol);
	}

	public Vector2 DefaultPosition { get; set; }

	private void StoreDefaultPosition() {
		var x = VirtualRegionOnScreen.x;
		var y = VirtualRegionOnScreen.y;
		DefaultPosition = new Vector2(x, y);
	}
}
