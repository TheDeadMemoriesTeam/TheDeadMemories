using UnityEngine;
using System.Collections;

public class BossController : EnemyController
{

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		
		skillManager.setBasePvMax(10);
		skillManager.setBaseManaMax(50);
		skillManager.setPhysicalResistance(5f);
		skillManager.setMagicResistance(5f);
		skillManager.setBasePhysicAttack(10f);
		skillManager.setBaseMagicAttack(20f);

		manaCost = -50;
		timeAttack = 1.5F;
		probabilityAttack = 0.1F;
		
		xp = 30;
		
		timeRegen = 6;
		
		gameObject.renderer.material.color = new Color(0.341F, 0.725F, 0.478F);
	}
	
	protected override void Update ()
	{
		base.Update();
		if (skillManager.getPv() <= 0)
			player.achievementManager.updateKillsBerseker();
	}
}