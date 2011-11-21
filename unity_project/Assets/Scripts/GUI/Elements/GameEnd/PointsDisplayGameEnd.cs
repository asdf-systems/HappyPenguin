using UnityEngine;
using System.Collections;

public class PointsDisplayGameEnd : TextPanel{
		
	protected override void StartOverride(){
		base.StartOverride();
		Text = "Points " + GameStatics.FormatPoints(GameStatics.Points);
	}
	
}
