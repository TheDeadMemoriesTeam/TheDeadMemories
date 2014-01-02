using UnityEngine;
using System.Collections;

public class CandleController : MonoBehaviour 
{
	// Lumière de la bougie
	private Light candle;

	// durée d'oscillation
	private float oscillationTime;

	// Use this for initialization
	void Start () 
	{
		candle = GetComponent<Light>();
		oscillationTime = Random.Range(1f, 2f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		candle.intensity = Mathf.PingPong(Time.time, oscillationTime) + 1f;
	}
}
