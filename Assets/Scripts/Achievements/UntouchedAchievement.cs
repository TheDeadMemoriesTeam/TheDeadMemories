using UnityEngine;
using System.Collections;

public class UntouchedAchievement : Achievement
{
	protected float requiredUntouchedTime;
	
	
	public UntouchedAchievement(AchievementManager am, string name, string description, float requiredUntouchedTime)
		: base(am, name, description)
	{
		this.requiredUntouchedTime = requiredUntouchedTime;
	}
	
	public override bool achieved()
	{
		return am.getUntouchedTime() >= requiredUntouchedTime;
	}
}
