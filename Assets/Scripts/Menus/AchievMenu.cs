using UnityEngine;
using System.Collections;

public class AchievMenu : SubMenu 
{
	private AchievementsSaveReader asr;

	// Use this for initialization
	void Start () 
	{
		asr = FindObjectOfType<AchievementsSaveReader>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnGUI()
	{
		if (!inFrontOf)
			return;
		Debug.Log("dessine achiev");
	}
}
