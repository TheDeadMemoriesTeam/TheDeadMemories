using UnityEngine;
using System.Collections;

public class ZoneAchievement : Achievement
{
	private string nameZone;

	public ZoneAchievement(AchievementManager am, string name, string description, string nameZone)
		: base(am, name, description)
	{
		this.nameZone = nameZone;
	}
	
	public override bool achieved()
	{
		Collider[] hitColliders = Physics.OverlapSphere(am.getPlayerPos(), 1f);

		for (int i = 0 ; i < hitColliders.Length ; i++)
			if (hitColliders[i].tag == nameZone)
				return true;
		return false;
	}
}
