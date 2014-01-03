using UnityEngine;
using System.Collections;

public class BossController : EnemyController
{
	public Transform[] tab;

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

		int ind = Random.Range(0, tab.Length);

		skillManager.addSkill(new PorteeSkills("Boss Skill", "", 0, null, 1.5f, 50, tab[ind], 10f, 0, 0, "", "", "", "", 1f)); 

		manaCost = -50;
		timeAttack = 1.5F;
		probabilityAttack = 0.1F;
		
		xp = 30;
		
		timeRegen = 6;
	}
	
	protected override void Update ()
	{
		base.Update();
		if (skillManager.getPv() <= 0)
			player.achievementManager.updateKillsBerseker();
	}
}