using UnityEngine;
using System.Collections;

public class PorteeSkills : BaseSkills 
{
	private float m_portee;
	
	//acsessor
	public float getPortee()
	{
		return m_portee;	
	}
	
	// Use this for initialization
	public PorteeSkills (string name, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, int costIncDamage, int costIncPortee, string nameDamage, string namePortee, float portee) 
		:base(name, price, father, timeIncantation, manaCost, particule, damage, costIncDamage, costIncPortee, nameDamage, namePortee)
	{
		m_portee = portee;
	}

	public void launch(Vector3 position, Vector3 forward, float damageMagic)
	{
		// Création et initialisation du projectile
		Transform projectileTransform = (Transform)GameObject.Instantiate(	getParticule(),
		                                                       	new Vector3(position.x + forward.x, 
		            											position.y + 1.5f + forward.y, 
		            											position.z + forward.z),
		                                                       	Quaternion.identity);
		ProjectilController projectile = projectileTransform.GetComponent<ProjectilController>() as ProjectilController;
		projectile.init(10f, m_portee, damageMagic+getDamage(), forward);
	}
}
