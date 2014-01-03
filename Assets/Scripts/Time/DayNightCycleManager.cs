using UnityEngine;
using System.Collections;

public class DayNightCycleManager : MonoBehaviour
{
	public float dayTime = 12F; // Start day time is midday.

	private Light sun;
	private float sunMaxIntensity;
	
	// Use this for initialization
	void Start () {
		// In unity, lights are set at 6 o'clock.
		// Following the dayTime specified, we update them.
		transform.Rotate(0, 0, hoursToDegrees(dayTime-6));

		sun = transform.FindChild("Sun").GetComponent<Light>();
		sunMaxIntensity = sun.intensity;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Rotate the sun and the moon.
		float timeProgress = realToInGameTime(Time.deltaTime);
		if ((dayTime += timeProgress) >= 24)
			dayTime = 0;
		transform.Rotate(0, 0, hoursToDegrees(timeProgress));

		// Diminue l'intensité du soleil la nuit
		float coef = -Mathf.Cos(dayTime/24f * 2*Mathf.PI);
		coef = Mathf.Sign(coef)*Mathf.Pow(Mathf.Sign(coef)*coef, 0.25f);
		coef = coef/2 + 0.5f; // To range [0, 1]
		sun.intensity = sunMaxIntensity * coef;
	}
	
	private float hoursToDegrees(float h)
	{
		return h/24*360;
	}
	
	private float realToInGameTime(float t)
	{
		return t/60; // 60s in real time <=> 1h in game
	}
}
