using UnityEngine;
using System.Collections;

public class SurvivedAchievement : Achievement
{
	protected float requiredSurvivedTime;
	
	
	public SurvivedAchievement(AchievementManager am, string name, string description, float requiredSurvivedTime)
		: base(am, name, description)
	{
		this.requiredSurvivedTime = requiredSurvivedTime;
	}
	
	public override bool achieved()
	{
		return am.getSurvivedTime() >= requiredSurvivedTime;
	}
}
