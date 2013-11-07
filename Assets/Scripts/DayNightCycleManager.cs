using UnityEngine;
using System.Collections;

public class DayNightCycleManager : MonoBehaviour
{
	
	public float dayTime = 12F; // Start day time is midday.
	
	// Use this for initialization
	void Start () {
		// In unity, lights are set at 6 o'clock.
		// Following the dayTime specified, we update them.
		transform.Rotate(0, 0, hoursToDegrees(dayTime-6));
	}
	
	// Update is called once per frame
	void Update ()
	{
		float timeProgress = realToInGameTime(Time.deltaTime);
		dayTime += timeProgress;
		transform.Rotate(0, 0, hoursToDegrees(timeProgress));
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
