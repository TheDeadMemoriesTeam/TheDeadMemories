using UnityEngine;
using System.Collections;

public class DistanceController : EnemyController
{

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		
		skillManager.setPvMax(4);
		skillManager.setPv(skillManager.getPvMax());
		
		damageAttack = -1;
		damageMagic = -5;
		manaCost = -40;
		timeAttack = 1F;
		probabilityAttack = 0.25F;
		
		xp = 15;
		
		skillManager.setManaMax(80);
		skillManager.setMana(skillManager.getManaMax());
		
		timeRegen = 4;
	}
	
	protected override void Update ()
	{
		base.Update();
	}
}
