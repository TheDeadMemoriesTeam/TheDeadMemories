using UnityEngine;
using System.Collections;

public class PeonController : EnemyController 
{

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		
		pvMax = 5;
		pv = pvMax;
		
		xp = 15;
	}
	
	protected override void Update ()
	{
		base.Update();
		if (pv<=0)
		{
			gameObject.renderer.material.color = new Color(1, 0, 0);
			Destroy(this);	
		}
	}
}
