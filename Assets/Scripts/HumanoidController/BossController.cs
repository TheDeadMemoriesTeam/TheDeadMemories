using UnityEngine;
using System.Collections;

public class BossController : EnemyController
{

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		
		skillManager.setBasePvMax(10);
		skillManager.setPv(skillManager.getPvMax());
		
		damageAttack = -10;
		damageMagic = -20;
		manaCost = -50;
		timeAttack = 1.5F;
		probabilityAttack = 0.1F;
		
		xp = 30;
		
		skillManager.setBaseManaMax(50);
		skillManager.setMana(skillManager.getManaMax());
		
		timeRegen = 6;
		
		gameObject.renderer.material.color = new Color(0.341F, 0.725F, 0.478F);
	}
	
	protected override void Update ()
	{
		base.Update();
		if (skillManager.getPv() <= 0)
			target.achievementManager.updateKillsBerseker();
	}
}