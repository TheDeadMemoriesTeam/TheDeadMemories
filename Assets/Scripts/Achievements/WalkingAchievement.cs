using UnityEngine;
using System.Collections;

public class WalkingAchievement : Achievement
{
	protected float requiredDistance;
	
	
	public WalkingAchievement(AchievementManager am, string name, string description, float requiredDistance)
		: base(am, name, description)
	{
		this.requiredDistance = requiredDistance;
	}
	
	public override bool achieved()
	{
		return am.getTravelledDistance() >= requiredDistance;
	}
}
