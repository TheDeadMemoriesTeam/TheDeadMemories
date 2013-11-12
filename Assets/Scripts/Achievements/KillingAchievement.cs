using UnityEngine;
using System.Collections;

public class KillingAchievement : Achievement
{
	protected int requiredKills;
	
	
	public KillingAchievement(AchievementManager am, string name, string description, int requiredKills)
		: base(am, name, description)
	{
		this.requiredKills = requiredKills;
	}
	
	public override bool achieved()
	{
		return am.getNbKilledEnemy() >= requiredKills;
	}
}
