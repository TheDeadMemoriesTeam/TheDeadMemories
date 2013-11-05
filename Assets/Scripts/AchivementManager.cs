using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
//Namespaces nécessaires pour BinaryFormatter
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class AchivementManager : MonoBehaviour {
	
	// Texture achivement
	public Texture texture; 
	
	// Son achivement
	public AudioClip soundAchivement;
	
	// Variables achievements
	private int cptEnemyKilled = 0;
	private float timeNotTouched = 0;
	private float timeSurvived = 0;
	private bool assassin = true;
	private float travel = 0;
	private int cptBersekerKilled = 0;
	private int cptAssassinKill = 0;

	
	// Associe le nom de l'achievement à son état (bool) => équivalent map de la STL
	private Dictionary<string, bool> AchievementsStates;
	
	void Awake ()
    {
		// Nom des achievements
		string[] names = {
			"firstMove", "firstKill", "tenKills", "hundredKills", "thousandKills", "noLimit", "thousandKillsBersekers",
			"untouch1min", "untouch5mins", "beginner", "amateur", "ghost", "immortal", "god", "assassin", "masterAssassin",
			"longArm", "oneKilometer", "tenKilometers", "marathon", "hundredKilometers", "thousandKilometers", "milionKilometer"
		};
		
		AchievementsStates = new Dictionary<string, bool>();
		// Initialise le dictionnaire des achievements
		for (int i = 0 ; i <  names.Length ; i++)
			AchievementsStates.Add(names[i], false);
    }
	
	// Use this for initialization
	void Start () 
	{
		//File.Delete("/achievements.dat");
		//PlayerPrefs.DeleteAll();
		loadAchievements();
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	public void saveAchievements()
	{
		//Cré un BinaryFormatter
		var binFormatter = new BinaryFormatter();
		//Cré un fichier
		var file = File.Create(Application.persistentDataPath + "/achievements.dat");
		//Sauvegarde les achievements
		binFormatter.Serialize(file, AchievementsStates);
        
		file.Close();
		
		// Sauvegarde les variables dans le PlayerPrefs
		PlayerPrefs.SetInt("cptEnemyKilled", cptEnemyKilled);
		PlayerPrefs.SetInt("cptBersekerKilled", cptBersekerKilled);
		PlayerPrefs.SetInt("cptAssassinKill", cptAssassinKill);
		PlayerPrefs.SetFloat("timeNotTouched", timeNotTouched);
		PlayerPrefs.SetFloat("timeSurvived", timeSurvived);
		PlayerPrefs.SetFloat("travel", travel);
	}
	
	void loadAchievements()
	{
		//Si le fichier de sauvegarde existe on le charge
		if(File.Exists(Application.persistentDataPath + "/achievements.dat"))
		{
			//BinaryFormatter pour charger les nouvelles données
			var binFormatter = new BinaryFormatter();
			//Ouvre le fichier
			var file = File.Open(Application.persistentDataPath + "/achievements.dat", FileMode.Open);
			//Charge les achievements
			AchievementsStates = (Dictionary<string, bool>)binFormatter.Deserialize(file);
			file.Close();
			
			// Charge les variables du PlayerPrefs
			cptEnemyKilled = PlayerPrefs.GetInt("cptEnemyKilled");
			cptBersekerKilled = PlayerPrefs.GetInt("cptBersekerKilled");
			cptAssassinKill = PlayerPrefs.GetInt("cptAssassinKill");
			timeNotTouched = PlayerPrefs.GetFloat("timeNotTouched");
			timeSurvived = PlayerPrefs.GetFloat("timeSurvived");
			travel = PlayerPrefs.GetFloat("travel");
		}
	}
	
	void changeState(string achievementName)
	{
		if (AchievementsStates.ContainsKey(achievementName))
			AchievementsStates[achievementName] = !AchievementsStates[achievementName];
	}
	
	bool getState(string achievementName)
	{
		if (AchievementsStates.ContainsKey(achievementName))
			return AchievementsStates[achievementName];
		else
			return false;
	}
	
	public void firstMoveAchievement()
	{
		string name = "firstMove";
		if (!getState(name))
		{
			Debug.Log("Achivement First Move !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void firstBloodAchievement()
	{
		string name = "firstKill";
		if (!getState(name))
		{
			Debug.Log("Achivement First Blood !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void littleKillerAchievement()
	{
		string name = "tenKills";
		if (!getState(name))
		{
			Debug.Log("Achivement Ten kills !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void killerAchievement()
	{
		string name = "hundredKills";
		if (!getState(name))
		{
			Debug.Log("Achivement a hundred kills !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void serialKillerAchievement()
	{
		string name = "thousandKills";
		if (!getState(name))
		{
			Debug.Log("Achivement 1 thousand kills !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void noLimitAchievement()
	{
		string name = "noLimit";
		if (!getState(name))
		{
			Debug.Log("Achivement No Limit (kill 10 Bersekers) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void serialKillerOfSerialKillerAchievement()
	{
		string name = "thousandKillsBersekers";
		if (!getState(name))
		{
			Debug.Log("Achivement Serial Killer Of Serial Killer (kill 1000 Bersekers) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void uncatchableAchievement()
	{
		string name = "untouch1min";
		if (!getState(name))
		{
			Debug.Log("Achivement Not touch during 1 min !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void reallyUncatchableAchievement()
	{
		string name = "untouch5mins";
		if (!getState(name))
		{
			Debug.Log("Achivement Not touch during 5 min !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void surviveOneMinuteAchievement()
	{
		string name = "beginner";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 1 min !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void surviveTwentyMinutesAchievement()
	{
		string name = "amateur";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 20 mins !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void surviveOneHourAchievement()
	{
		string name = "ghost";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 1 h !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void surviveFourHoursAchievement()
	{
		string name = "immortal";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 4 h !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void surviveTwelveHoursAchievement()
	{
		string name = "god";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 12 h !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void assassinAchievement()
	{
		string name = "assassin";
		if (!getState(name))
		{
			Debug.Log("Achivement Kill 5 enemy and not be touch !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void masterAssassinAchievement()
	{
		string name = "masterAssassin";
		if (!getState(name))
		{
			Debug.Log("Achivement Kill 50 enemy and not be touch !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void longArmAchievement()
	{
		string name = "longArm";
		if (!getState(name))
		{
			Debug.Log("Achivement Long Arm (10 kills in the same time) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void sundayWalkerAchievement()
	{
		string name = "oneKilometer";
		if (!getState(name))
		{
			Debug.Log("Achivement Sunday Walker (run on 1 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void dailyJoggingAchievement()
	{
		string name = "tenKilometers";
		if (!getState(name))
		{
			Debug.Log("Achivement Daily Jogging (run on 10 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void marathonAchievement()
	{
		string name = "marathon";
		if (!getState(name))
		{
			Debug.Log("Achivement Marathon (run on 42,195 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void healthWalkAchievement()
	{
		string name = "hundredKilometers";
		if (!getState(name))
		{
			Debug.Log("Achivement Health Walk (run on 100 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void athleticAchievement()
	{
		string name = "thousandKilometers";
		if (!getState(name))
		{
			Debug.Log("Achivement Athletic (run on 1.000 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	public void dopedAddictAchievement()
	{
		string name = "milionKilometer";
		if (!getState(name))
		{
			Debug.Log("Achivement Doped Addict (run on 1.000.000 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void unlockAchivement(Texture textureAchivement)
	{
		// TODO
		audio.PlayOneShot(soundAchivement);
	}
	
	// Accesseurs
	public int getCptEnemyKilled()
	{
		return cptEnemyKilled;
	}
	
	public void updateCptEnemyKilled()
	{
		cptEnemyKilled++;
	}
	
	public int getCptAssassinKill()
	{
		return cptAssassinKill;
	}
	
	public void updateCptAssassinKill()
	{
		cptAssassinKill++;
	}
	
	public int getCptBersekerKilled()
	{
		return cptBersekerKilled;
	}
	
	public void updateCptBersekerKilled()
	{
		cptBersekerKilled++;
	}
	
	public bool getAssassin()
	{
		return assassin;
	}
	
	public void setAssassin(bool b)
	{
		assassin = b;
	}
	
	public float getTravel()
	{
		return travel;
	}
	
	public void setTravel(float t)
	{
		travel = t;
	}
	
	public float getTimeNotTouched()
	{
		return timeNotTouched;
	}
	
	public void setTimeNotTouched(float time)
	{
		timeNotTouched = time;
	}
	
	public float getTimeSurvived()
	{
		return timeSurvived;
	}
	
	public void setTimeSurvived(float time)
	{
		timeNotTouched = time;
	}
}