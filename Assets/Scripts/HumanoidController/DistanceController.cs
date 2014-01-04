﻿using UnityEngine;
using System.Collections;

public class DistanceController : EnemyController
{
	public Transform[] tab;

	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		
		skillManager.setBasePvMax(4);
		skillManager.setBaseManaMax(80);
		skillManager.setPhysicalResistance(0f);
		skillManager.setMagicResistance(5f);
		skillManager.setBasePhysicAttack(1f);
		skillManager.setBaseMagicAttack(5f);

		int ind = Random.Range(0, tab.Length);

		skillManager.addSkill(new PorteeSkills("skill", "", 0, null, 1f, 40, tab[ind], 2.5f, 40, 40, "", "", "", "", 1f)); 
		skillManager.addSkill(new PassiveSkills("survie", "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills("attaque", "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));
		skillManager.addSkill(new PassiveSkills("resistance", "", 0, null, 40, 40, 5f, 5f, "", "", "", ""));

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
