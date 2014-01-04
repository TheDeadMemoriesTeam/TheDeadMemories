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

		skillManager.addSkill(new PorteeSkills("skill", "", 0, null, 1.5f, 50, tab[ind], 10f, 40, 40, "", "", "", "", 1f));
		skillManager.addSkill(new PassiveSkills("survie", "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills("attaque", "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills("resistance", "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));

		timeAttack = 1.5F;
		probabilityAttack = 0.1F;
		
		xp = 30;
		
		timeRegen = 0.8f;
	}
	
	protected override void Update ()
	{
		base.Update();
		if (skillManager.getPv() <= 0)
			player.achievementManager.updateKillsBerseker();
	}
}