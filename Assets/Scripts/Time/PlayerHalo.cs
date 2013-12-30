using UnityEngine;
using System.Collections;

public class PlayerHalo : MonoBehaviour {
	
	public Light halo;
	public DayNightCycleManager dayNightCycle;
	private float maxIntensity = 0.7f;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		float intensity = Mathf.Sin(toRadian(hourToIntensityVal(dayNightCycle.dayTime)/2));
		
		if (intensity < 0)
			halo.intensity = 0;
		else
			halo.intensity = maxIntensity * intensity;
	}
	
	float hourToIntensityVal(float hour)
	{
		if (hour/24f*360f + 180 > 360)
			return (hour/24f*360f + 180) - 360;
		else
			return hour/24f*360f + 180;
	}
	
	float toRadian(float degree)
	{
		return Mathf.PI*degree/180 ;
	}
}
