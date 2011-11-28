using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class NormalDistributedTimer : Timer {

	public float mu = 0;
	public float sigma = 1;
	public Cutoff upperLimit, lowerLimit;


	public new void StartTimer(float timeInSeconds){
		StartTimer();
	}

	public new void StartTimer(){
		base.StartTimer(10);
		/*
		  y1 = sqrt( - 2 ln(x1) ) cos( 2 pi x2 )
         y2 = sqrt( - 2 ln(x1) ) sin( 2 pi x2 )
         */
	}

}

[System.Serializable]
public class Cutoff {
	public bool enabled = false;
	public float treshold = 0;
	public float value = 0;
}
