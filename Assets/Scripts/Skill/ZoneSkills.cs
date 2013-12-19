using UnityEngine;
using System.Collections;

public class ZoneSkills : BaseSkills
{
	private float m_zone;
	
	//acsessor
	public float getZone()
	{
		return m_zone;	
	}
	
	// Use this for initialization
	public ZoneSkills (string name, int price, Skills father, float timeIncantation, int manaCost, Transform particule, float damage, int costIncDamage, int costIncZone, string nameDamage, string nameZone, float zone) 
		:base(name, price, father, timeIncantation, manaCost, particule, damage, costIncDamage, costIncZone, nameDamage, nameZone)
	{
		m_zone = zone;

	}

	public void launch(Vector3 position)
	{
		Transform magicZoneTransform = (Transform)GameObject.Instantiate(getParticule(),
		                                                      new Vector3(position.x, 
		            										  position.y,
		            										  position.z),
		                                                      Quaternion.identity);
	}
}
