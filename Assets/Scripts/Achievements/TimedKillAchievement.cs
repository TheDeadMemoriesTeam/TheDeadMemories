using UnityEngine;
using System.Collections;

public class TimedKillAchievement : Achievement
{
	protected int requiredKills;
	protected float timeGive;
	
	public TimedKillAchievement(AchievementManager am, string name, string description, int requiredKills, float timeGive)
		: base(am, name, description)
	{
		this.requiredKills = requiredKills;
		this.timeGive = timeGive;
	}
	
	public override bool achieved()
	{
		if (!(timeGive - am.getLimitTime() >= 0))
		{
			am.resetKillPerSession();
			return false;
		}
		return am.getNbKillPerSession() >= requiredKills;
	}
}
