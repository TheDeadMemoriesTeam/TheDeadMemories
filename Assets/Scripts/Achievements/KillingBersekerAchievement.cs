using UnityEngine;
using System.Collections;

public class KillingBersekerAchievement : Achievement 
{
	protected int requiredKills;
	
	public KillingBersekerAchievement(AchievementManager am, string name, string description, int requiredKills)
		: base(am, name, description)
	{
		this.requiredKills = requiredKills;
	}
	
	public override bool achieved()
	{
		return am.getNbKilledBerseker() >= requiredKills;
	}
}
