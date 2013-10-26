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
	}
	
	protected override void Update ()
	{
		base.Update();
	}
}
