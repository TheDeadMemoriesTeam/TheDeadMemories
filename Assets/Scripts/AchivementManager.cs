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
	private bool hundredKills = false;
	private bool thousandKills = false;
	
	private bool untouch1min = false;
	private bool untouch5mins = false;
	
	private bool beginner = false;
	private bool amateur = false;
	private bool ghost = false;
	private bool immortal = false;
	private bool god = false;
	
	private bool assassin = false;
	private bool masterAssassin = false;
	
	private bool longArm = false;
	
	private bool oneKilometer = false;
	private bool tenKilometers = false;
	private bool marathon = false;
	private bool hundredKilometers = false;
	private bool thousandKilometers = false;
	private bool milionKilometer = false;
	
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
	
	public void firstBloodAchievement()
	{
		if (!firstKill)
		{
			Debug.Log("Achivement First Blood !");
			unlockAchivement(texture);
			firstKill = !firstKill;
		}
	}
	
	public void littleKillerAchievement()
	{
		if (!tenKills)
		{
			Debug.Log("Achivement Ten kills !");
			unlockAchivement(texture);
			tenKills = !tenKills;
		}
	}
	
	public void killerAchievement()
	{
		if (!hundredKills)
		{
			Debug.Log("Achivement a hundred kills !");
			unlockAchivement(texture);
			hundredKills = !hundredKills;
		}
	}
	
	public void serialKillerAchievement()
	{
		if (!thousandKills)
		{
			Debug.Log("Achivement 1 thousand kills !");
			unlockAchivement(texture);
			thousandKills = !thousandKills;
		}
	}
	
	public void noLimitAchievement()
	{
		if (!thousandKills)
		{
			Debug.Log("Achivement No Limit (kill 10 Bersekers) !");
			unlockAchivement(texture);
			thousandKills = !thousandKills;
		}
	}
	
	public void serialKillerOfSerialKillerAchievement()
	{
		if (!thousandKills)
		{
			Debug.Log("Achivement Serial Killer Of Serial Killer (kill 1000 Bersekers) !");
			unlockAchivement(texture);
			thousandKills = !thousandKills;
		}
	}
	
	public void uncatchableAchievement()
	{
		if (!untouch1min)
		{
			Debug.Log("Achivement Not touch during 1 min !");
			unlockAchivement(texture);
			untouch1min = !untouch1min;
		}
	}
	
	public void reallyUncatchableAchievement()
	{
		if (!untouch5mins)
		{
			Debug.Log("Achivement Not touch during 5 min !");
			unlockAchivement(texture);
			untouch5mins = !untouch5mins;
		}
	}
	
	public void surviveOneMinuteAchievement()
	{
		if (!beginner)
		{
			Debug.Log("Achivement Survive during 1 min !");
			unlockAchivement(texture);
			beginner = !beginner;
		}
	}
	
	public void surviveTwentyMinutesAchievement()
	{
		if (!amateur)
		{
			Debug.Log("Achivement Survive during 20 mins !");
			unlockAchivement(texture);
			amateur = !amateur;
		}
	}
	
	public void surviveOneHourAchievement()
	{
		if (!ghost)
		{
			Debug.Log("Achivement Survive during 1 h !");
			unlockAchivement(texture);
			ghost = !ghost;
		}
	}
	
	public void surviveFourHoursAchievement()
	{
		if (!immortal)
		{
			Debug.Log("Achivement Survive during 4 h !");
			unlockAchivement(texture);
			immortal = !immortal;
		}
	}
	
	public void surviveTwelveHoursAchievement()
	{
		if (!god)
		{
			Debug.Log("Achivement Survive during 12 h !");
			unlockAchivement(texture);
			god = !god;
		}
	}
	
	public void assassinAchievement()
	{
		if (!assassin)
		{
			Debug.Log("Achivement Kill 5 enemy and not be touch !");
			unlockAchivement(texture);
			assassin = !assassin;
		}
	}
	
	public void masterAssassinAchievement()
	{
		if (!masterAssassin)
		{
			Debug.Log("Achivement Kill 50 enemy and not be touch !");
			unlockAchivement(texture);
			masterAssassin = !masterAssassin;
		}
	}
	
	public void longArmAchievement()
	{
		if (!longArm)
		{
			Debug.Log("Achivement Long Arm (10 kills in the same time) !");
			unlockAchivement(texture);
			longArm = !longArm;
		}
	}
	
	public void sundayWalkerAchievement()
	{
		if (!oneKilometer)
		{
			Debug.Log("Achivement Sunday Walker (run on 1 km) !");
			unlockAchivement(texture);
			oneKilometer = !oneKilometer;
		}
	}
	
	public void dailyJoggingAchievement()
	{
		if (!tenKilometers)
		{
			Debug.Log("Achivement Daily Jogging (run on 10 km) !");
			unlockAchivement(texture);
			tenKilometers = !tenKilometers;
		}
	}
	
	public void marathonAchievement()
	{
		if (!marathon)
		{
			Debug.Log("Achivement Marathon (run on 42,195 km) !");
			unlockAchivement(texture);
			marathon = !marathon;
		}
	}
	
	public void healthWalkAchievement()
	{
		if (!hundredKilometers)
		{
			Debug.Log("Achivement Health Walk (run on 100 km) !");
			unlockAchivement(texture);
			hundredKilometers = !hundredKilometers;
		}
	}
	
	public void athleticAchievement()
	{
		if (!thousandKilometers)
		{
			Debug.Log("Achivement Athletic (run on 1.000 km) !");
			unlockAchivement(texture);
			thousandKilometers = !thousandKilometers;
		}
	}
	
	public void dopedAddictAchievement()
	{
		if (!milionKilometer)
		{
			Debug.Log("Achivement Doped Addict (run on 1.000.000 km) !");
			unlockAchivement(texture);
			milionKilometer = !milionKilometer;
		}
	}
	
	void unlockAchivement(Texture textureAchivement)
	{
		// TODO
		audio.PlayOneShot(soundAchivement);
	}
}