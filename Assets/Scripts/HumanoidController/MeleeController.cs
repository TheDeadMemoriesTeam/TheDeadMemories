using UnityEngine;
using System.Collections;

public class MeleeController : EnemyController 
{
	public Transform[] tab;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		
		skillManager.setBasePvMax(6);
		skillManager.setBaseManaMax(50);
		skillManager.setBasePhysicalResistance(5f);
		skillManager.setBaseMagicResistance(0f);
		skillManager.setBasePhysicAttack(3f);
		skillManager.setBaseMagicAttack(2f);

		int ind = Random.Range(0, tab.Length);

		skillManager.addSkill(new PorteeSkills("skill", "", 0, null, 1f, 50, tab[ind], 1f, 40, 40, "", "", "", "", 1f)); 
		skillManager.addSkill(new PassiveSkills(LanguageManager.Instance.GetTextValue("Skills.nameSkill4"), "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills(LanguageManager.Instance.GetTextValue("Skills.nameSkill1"), "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills(LanguageManager.Instance.GetTextValue("Skills.nameSkill2"), "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));

		timeAttack = 1F;
		probabilityAttack = 0.1F;
		
		xp = 15;
		
		timeRegen = 0.5f;
	}
	
	protected override void Update ()
	{
		base.Update();
	}
}
