using UnityEngine;
using System.Collections;

public class DistanceController : EnemyController
{
	public Transform[] tab;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		
		skillManager.setBasePvMax(9);
		skillManager.setBaseManaMax(80);
		skillManager.setBasePhysicalResistance(0f);
		skillManager.setBaseMagicResistance(5f);
		skillManager.setBasePhysicAttack(1f);
		skillManager.setBaseMagicAttack(5f);

		int ind = Random.Range(0, tab.Length);

		skillManager.addSkill(new PorteeSkills("skill", "", 0, null, 1f, 40, tab[ind], 2.5f, 40, 40, "", "", "", "", 1f)); 
		skillManager.addSkill(new PassiveSkills(LanguageManager.Instance.GetTextValue("Skills.nameSkill4"), "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills(LanguageManager.Instance.GetTextValue("Skills.nameSkill1"), "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills(LanguageManager.Instance.GetTextValue("Skills.nameSkill2"), "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));

		timeAttack = 1F;
		probabilityAttack = 0.25F;
		
		xp = 15;
		
		timeRegen = 0.5f;
	}
	
	protected override void Update ()
	{
		base.Update();
	}
}
