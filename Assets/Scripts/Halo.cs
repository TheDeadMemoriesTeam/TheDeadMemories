﻿using UnityEngine;
using System.Collections;

public class Halo : MonoBehaviour {
	
	public Light halo;
	public DayNightCycleManager time;
	private float maxIntensity;
	
	// Use this for initialization
	void Start () {
		maxIntensity = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		float cosinus = Mathf.Pow(Mathf.Cos(((time.dayTime+6f)/24f)*360f), 0.3f);
		if (cosinus<0)
			halo.intensity = 0;
		else
			halo.intensity = maxIntensity * cosinus;
	}
}