using UnityEngine;
using System.Collections;

public class Button : Control {

	public Rect hoverUV;
	public Rect activeUV;
	
	private bool down = false;
	
	
	
	public override void OnClick(object sender, MouseEventArgs e){
		base.OnClick(sender,e);
		
	}
	
	public override void OnDown(object sender, MouseEventArgs e){
		base.OnDown(sender,e);
		down = true;
		plane.UV = activeUV;
	}
	
	public override void OnUp(object sender, MouseEventArgs e){
		base.OnUp(sender,e);
		down = false;
		plane.UV = Uv;
	}
	
	public override void OnHover(object sender, MouseEventArgs e){
		base.OnHover(sender,e);
		if(!down)
			plane.UV = hoverUV;
		
	}
	
	public override void resetElement(){
		if(!down && plane != null){
			plane.UV = Uv;
		}
	}
	
	
	
	
}
