using UnityEngine;
using System.Collections;

public class DistanceController : EnemyController
{

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		
		skillManager.setBasePvMax(4);
		skillManager.setBaseManaMax(80);
		skillManager.setPhysicalResistance(0f);
		skillManager.setMagicResistance(5f);
		skillManager.setBasePhysicAttack(1f);
		skillManager.setBaseMagicAttack(5f);

		manaCost = -40;
		timeAttack = 1F;
		probabilityAttack = 0.25F;
		
		xp = 15;
		
		timeRegen = 4;
	}
	
	protected override void Update ()
	{
		base.Update();
	}
}
