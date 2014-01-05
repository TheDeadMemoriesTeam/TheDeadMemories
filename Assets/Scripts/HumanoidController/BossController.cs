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
		skillManager.setBasePhysicalResistance(5f);
		skillManager.setBaseMagicResistance(5f);
		skillManager.setBasePhysicAttack(10f);
		skillManager.setBaseMagicAttack(20f);

		int ind = Random.Range(0, tab.Length);

		skillManager.addSkill(new PorteeSkills("skill", "", 0, null, 1.5f, 50, tab[ind], 10f, 40, 40, "", "", "", "", 1f));
		skillManager.addSkill(new PassiveSkills(LanguageManager.Instance.GetTextValue("Skills.nameSkill4"), "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills(LanguageManager.Instance.GetTextValue("Skills.nameSkill1"), "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills(LanguageManager.Instance.GetTextValue("Skills.nameSkill2"), "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));

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