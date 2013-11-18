using UnityEngine;
using System.Collections;

public class Halo : MonoBehaviour {
	
	public Light halo;
	private DayNightCycleManager time;
	private float maxIntensity;
	
	// Use this for initialization
	void Start () {
		time = (DayNightCycleManager)FindObjectOfType(System.Type.GetType("DayNightCycle"));
		maxIntensity = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		float cosinus = Mathf.Pow(Mathf.Cos(time.dayTime/24f*360f), 0.3f);
		if (cosinus<0)
			halo.intensity = 0;
		else
			halo.intensity = maxIntensity * cosinus;
	}
}
