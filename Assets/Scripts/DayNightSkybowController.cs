using UnityEngine;
using System.Collections;

public class DayNightSkybowController : MonoBehaviour {

	public Camera camera;
	public DayNightCycleManager dayNightCycle;
	public Color lightestColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float dayTime = dayNightCycle.dayTime - 0.7f; // Add an offset of -0.7 hour on the current time to have a better result.
		float coef = -Mathf.Cos(dayTime/24f * 2*Mathf.PI);
		coef = Mathf.Sign(coef)*Mathf.Pow(Mathf.Sign(coef)*coef, 0.4f);

		coef = coef/2 + 0.5f; // To range [0, 1]
		coef *= 0.75f; // Scale
		coef += 0.25f; // Light intensity to range [0.6, 1]

		camera.backgroundColor = new Color(lightestColor.r * coef, lightestColor.g * coef, lightestColor.b * coef);
	}
}
