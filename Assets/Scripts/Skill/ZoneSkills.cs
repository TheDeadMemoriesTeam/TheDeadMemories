using UnityEngine;
using System.Collections;

public class ZoneSkills : BaseSkills
{	
	// Use this for initialization
	public ZoneSkills (string name, string description, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, int costIncDamage, int costIncZone, string nameDamage, string nameZone, string descriptionDamage, string descriptionZone,float baseZone) 
		:base(name, description, price, father, timeIncantation, manaCost, particule, damage, costIncDamage, costIncZone, nameDamage, nameZone, descriptionDamage, descriptionZone, baseZone)
	{
	}

	public void launch(Vector3 position)
	{
		GameObject.Instantiate(m_particule, new Vector3(position.x, position.y, position.z), Quaternion.identity);
	}
}
