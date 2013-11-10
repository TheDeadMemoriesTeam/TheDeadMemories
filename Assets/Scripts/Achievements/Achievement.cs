using UnityEngine;
using System.Collections;

public abstract class Achievement
{
	
	protected AchievementManager am;
	
	protected string name;
	protected string description;
	protected bool _achieved = false;
	
	
	public Achievement(AchievementManager am, string name, string description)
	{
		this.am = am;
		this.name = name;
		this.description = description;
	}
	
	public string getName()
	{
		return name;
	}
	
	public string getDescription()
	{
		return description;
	}
	
	public abstract bool achieved();
	
	public bool check()
	{
		// Returns true if the achievement is achieved for the first time
		if (!_achieved && achieved()) {
			_achieved = true;
			return true;
		}
		return false;
	}
}
