using UnityEngine;
using System.Collections;

public class CampFireController : MonoBehaviour {

	public ParticleEmitter outerCore;
	public ParticleEmitter innerCore;
	public ParticleEmitter smoke;
	public DayNightCycleManager dayNightCycle;

	private float upperOuterCoreSize = 1.2f;
	private float lowerOuterCoreSize = 0.12f;
	private float upperOuterCoreYVelocity = 1f;
	private float lowerOuterCoreYVelocity = 0.001f;

	private float upperInnerCoreSize = 2f;
	private float lowerInnerCoreSize = 0.2f;
	private float upperInnerCoreYVelocity = 1.75f;
	private float lowerInnerCoreYVelocity = 0.00175f;

	private float upperSmokeSize = 1.5f;
	private float lowerSmokeSize = 0.15f;
	private float upperSmokeYVelocity = 1.5f;
	private float lowerSmokeYVelocity = 0.0015f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		/*
		outerCore
		min/max-size: 0.2 - 1.2
		local velocity: 0.01 - 1
		*/
		float dayTime = dayNightCycle.dayTime - 0.5f; // Add an offset of -0.5 hour on the current time to have a better result.
		float coef = Mathf.Cos(dayTime/24f * 2*Mathf.PI);
		coef = Mathf.Sign(coef)*Mathf.Pow(Mathf.Sign(coef)*coef, 0.3f);
		
		coef = coef/2 + 0.5f; // To range [0, 1]
		coef = coef*coef;

		outerCore.minSize = (upperOuterCoreSize - lowerOuterCoreSize) * coef + lowerOuterCoreSize;
		outerCore.maxSize = outerCore.minSize;

		outerCore.localVelocity = new Vector3(outerCore.localVelocity.x,
		                                      ( upperOuterCoreYVelocity - lowerOuterCoreYVelocity) * coef + lowerOuterCoreYVelocity,
		                                      outerCore.localVelocity.z);

		innerCore.minSize = (upperInnerCoreSize - lowerInnerCoreSize) * coef + lowerInnerCoreSize;
		innerCore.maxSize = innerCore.minSize;
		
		innerCore.localVelocity = new Vector3(innerCore.localVelocity.x,
		                                      ( upperInnerCoreYVelocity - lowerInnerCoreYVelocity) * coef + lowerInnerCoreYVelocity,
		                                      innerCore.localVelocity.z);

		smoke.minSize = (upperSmokeSize - lowerSmokeSize) * coef + lowerSmokeSize;
		smoke.maxSize = smoke.minSize;
		
		smoke.localVelocity = new Vector3(smoke.localVelocity.x,
		                                      ( upperSmokeYVelocity - lowerSmokeYVelocity) * coef + lowerSmokeYVelocity,
		                                      smoke.localVelocity.z);
	}
}
