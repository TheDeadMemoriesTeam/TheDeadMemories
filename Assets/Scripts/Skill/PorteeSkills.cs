using UnityEngine;
using System.Collections;

public class PorteeSkills : BaseSkills 
{	
	// Use this for initialization
	public PorteeSkills (string name, string description, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, int costIncDamage, int costIncPortee, string nameDamage, string namePortee, string descriptionDamage, string descriptionPortee, float basePortee) 
		:base(name, description, price, father, timeIncantation, manaCost, particule, damage, costIncDamage, costIncPortee, nameDamage, namePortee, descriptionDamage, descriptionPortee, basePortee)
	{
	}

	public void launch(Vector3 position, Vector3 forward, float damageMagic, float critic, float factor)
	{
		// Création et initialisation du projectile
		Transform projectileTransform = (Transform)GameObject.Instantiate(	m_particule,
		                                                       	new Vector3(position.x + forward.x, 
		            											position.y + 1.5f + forward.y, 
		            											position.z + forward.z),
		                                                       	Quaternion.identity);
		ProjectilController projectile = projectileTransform.GetComponent<ProjectilController>() as ProjectilController;
		float damage = damageMagic + m_damage;
		//gestion furie
		damage += damage/100 * factor;
		//gestion critique
		if(critic/100 < Random.value)
			damage *= 2;
		projectile.init(10f, m_ad, damage, forward);
	}
}
