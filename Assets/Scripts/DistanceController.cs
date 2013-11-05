using UnityEngine;
using System.Collections;

public class DistanceController : EnemyController
{

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		
		pvMax = 4;
		pv = pvMax;
		
		damageAttack = -1;
		damageMagic = -5;
		manaCost = -40;
		timeAttack = 1F;
		probabilityAttack = 0.25F;
		
		xp = 15;
		
		manaMax = 80;
		mana = manaMax;
		
		timeRegen = 4;
		
		gameObject.renderer.material.color = new Color(0.341F, 0.478F, 0.725F);
	}
	
	protected override void Update ()
	{
		base.Update();
	}
}
