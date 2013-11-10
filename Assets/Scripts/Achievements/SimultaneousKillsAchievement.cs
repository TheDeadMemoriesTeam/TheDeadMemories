using UnityEngine;
using System.Collections;

public class SimultaneousKillsAchievement : Achievement
{
	protected int requiredSimultaneousKills;
	
	
	public SimultaneousKillsAchievement(AchievementManager am, string name, string description, int requiredSimultaneousKills)
		: base(am, name, description)
	{
		this.requiredSimultaneousKills = requiredSimultaneousKills;
	}
	
	public override bool achieved()
	{
		return am.getNbSimultaneouslyKilledEnemy() >= requiredSimultaneousKills;
	}
}
