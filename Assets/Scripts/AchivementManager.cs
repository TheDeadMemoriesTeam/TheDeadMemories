using UnityEngine;
using System.Collections;

public class AchivementManager : MonoBehaviour {
	
	// Texture achivement
	public Texture texture; 
	
	// Son achivement
	public AudioClip soundAchivement; 
	
	// Booléens des achivements
	private bool firstMove = false;
	private bool firstKill = false;
	private bool tenKills = false;
	private bool untouch1min = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void FirstMoveAchievement()
	{
		if (!firstMove)
		{
			Debug.Log("Achivement First Move !");
			unlockAchivement(texture);
			firstMove = !firstMove;
		}
	}
	
	public void oneKillAchievement()
	{
		if (!firstKill)
		{
			Debug.Log("Achivement First Blood !");
			unlockAchivement(texture);
			firstKill = !firstKill;
		}
	}
	
	public void tenKillsAchievement()
	{
		if (!tenKills)
		{
			Debug.Log("Achivement Ten kills !");
			unlockAchivement(texture);
			tenKills = !tenKills;
		}
	}
	
	public void untouchOneMinuteAchievement()
	{
		if (!untouch1min)
		{
			Debug.Log("Achivement Not touch during 1min !");
			unlockAchivement(texture);
			untouch1min = !untouch1min;
		}
	}
	
	void unlockAchivement(Texture textureAchivement)
	{
		// TODO
		audio.PlayOneShot(soundAchivement);
	}
}