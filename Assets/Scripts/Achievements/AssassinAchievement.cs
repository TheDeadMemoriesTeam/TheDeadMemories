using UnityEngine;
using System.Collections;

public class AssassinAchievement : Achievement
{
	protected int requiredKills;
	
	
	public AssassinAchievement(AchievementManager am, string name, string description, int requiredKills)
		: base(am, name, description)
	{
		this.requiredKills = requiredKills;
	}
	
	public override bool achieved()
	{
		return am.getNbAssassinKills() >= requiredKills;
	}
}
