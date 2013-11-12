using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
	private float intensityMax = 2F;
	private float intensityMin = 0.5F;
	private bool intensitySens = true;
	
	private float rangeMax = 45F;
	private float rangeMin = 35F;
	private bool rangeSens = true;
	
	private float angleMaxX = 70F;
	private float angleMinX = 60F;
	private bool angleSensX = true;
	
	private float angleMaxY = 282.5F;
	private float angleMinY = 277.5F;
	private bool angleSensY = true;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(rangeSens)
		{
			if(light.spotAngle <= rangeMax)
				light.spotAngle += 0.05F;
			else
				rangeSens = false;
		}
		else
		{
			if(light.spotAngle >= rangeMin)
				light.spotAngle -= 0.05F;
			else
				rangeSens = true;
		}
		
		if(intensitySens)
		{
			if(light.intensity <= intensityMax)
				light.intensity += 0.01F;
			else
				intensitySens = false;
		}
		else
		{
			if(light.intensity >= intensityMin)
				light.intensity -= 0.01F;
			else
				intensitySens = true;
		}
		
		if(angleSensX)
		{
			if(transform.rotation.eulerAngles.x <= angleMaxX)
				transform.Rotate(0.02F, 0, 0);
			else
				angleSensX = false;
		}
		else
		{
			if(transform.rotation.eulerAngles.x >= angleMinX)
				transform.Rotate(-0.02F, 0, 0);
			else
				angleSensX = true;
		}
		
		if(angleSensY)
		{
			if(transform.rotation.eulerAngles.y <= angleMaxY)
				transform.Rotate(0, 0.01F, 0);
			else
				angleSensY = false;
		}
		else
		{
			if(transform.rotation.eulerAngles.y >= angleMinY)
				transform.Rotate(0, -0.01F, 0);
			else
				angleSensY = true;
		}
	}
}
