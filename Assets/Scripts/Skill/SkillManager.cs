using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour 
{
	//list des competances
	private List<Skills> listSkills;
	
	void Awake()
	{
		//creation de la liste
		listSkills = new List<Skills>();
		
		//arbre de competence Survie
		listSkills.Add(new SurvieSkills("Survie", 0, null, 200, 200, 5, 5));
		listSkills.Add(new ResistanceSkills("Resistance", 0, listSkills[0], 200, 200, 5, 5)); 
		listSkills.Add(new InvincibleSkill("Invincible", 3000, listSkills[1], 0, 30, 5));
		
		//arbre de competence Attaque
		listSkills.Add(new BaseAttaqueSkills("Attaque de base", 0, null, 200, 200, 5, 5));
		listSkills.Add(new CounterAttaqueSkills("Contre attaque", 0, listSkills[3], 200, 200, 5, 0.1f));
		listSkills.Add (new FurieSkills("Furie", 3000, listSkills[4], 0, 30, 5f, 1.5f));
		
		//arbre de competence Feu
		listSkills.Add(new PorteeSkills("Boule de feu", 1000, null, 1f, 10, 10, 200, 200, 2f));
		listSkills.Add(new ZoneSkills("Lance flames", 1000, listSkills[6], 2f, 15, 15, 200, 200, 2f));
		listSkills.Add(new SuperSkills("Meteor", 3000, listSkills[7], 3f, 20, 20, 10f, 10f)); 
		
		//arbre de competence Glace
		listSkills.Add(new PorteeSkills("Glacon", 1000, null, 1f, 10, 10, 200, 200, 2f));
		listSkills.Add(new ZoneSkills("Iceberg", 1000, listSkills[9], 2f, 15, 15, 200, 200, 2f));
		listSkills.Add(new SuperSkills("Ere glaciere", 3000, listSkills[10], 3f, 20, 20, 10f, 10f));
		
		//arbre de competence Vent
		listSkills.Add(new PorteeSkills("Soufle", 1000, null, 1f, 10, 10, 200, 200, 2f));
		listSkills.Add(new ZoneSkills("Bourasque", 1000, listSkills[12], 2f, 15, 15, 200, 200, 2f));
		listSkills.Add(new SuperSkills("Tornade", 3000, listSkills[13], 3f, 20, 20, 10f, 10f));
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
