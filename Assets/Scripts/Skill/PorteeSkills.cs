using UnityEngine;
using System.Collections;

public class PorteeSkills : BaseSkills 
{	
	// Use this for initialization
	public PorteeSkills (string name, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, int costIncDamage, int costIncPortee, string nameDamage, string namePortee, float basePortee) 
		:base(name, price, father, timeIncantation, manaCost, particule, damage, costIncDamage, costIncPortee, nameDamage, namePortee, basePortee)
	{
	}

	public void launch(Vector3 position, Vector3 forward, float damageMagic)
	{
		// Création et initialisation du projectile
		Transform projectileTransform = (Transform)GameObject.Instantiate(	m_particule,
		                                                       	new Vector3(position.x + forward.x, 
		            											position.y + 1.5f + forward.y, 
		            											position.z + forward.z),
		                                                       	Quaternion.identity);
		ProjectilController projectile = projectileTransform.GetComponent<ProjectilController>() as ProjectilController;
		projectile.init(10f, m_ad, damageMagic+m_damage, forward);
	}
}
