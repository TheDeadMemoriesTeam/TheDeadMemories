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
		Debug.Log(tab[ind].name);

		skillManager.addSkill(new PorteeSkills("melee Skill", "", 0, null, 1f, 50, tab[ind], 1f, 0, 0, "", "", "", "", 1f)); 
		
		manaCost = -50;
		timeAttack = 1F;
		probabilityAttack = 0.1F;
		
		xp = 15;
		
		timeRegen = 6;
		
		//gameObject.renderer.material.color = new Color(0.725F, 0.478F, 0.341F);
	}
	
	protected override void Update ()
	{
		base.Update();
	}
}
