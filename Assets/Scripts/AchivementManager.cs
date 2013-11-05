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
	private const float timesLimit = 60f;	// Temps imparti par session de kill => 1min
	private float timeLimit = 0;
	private int cptKillPerMin = 0;

	
	// Associe le nom de l'achievement à son état (bool) => équivalent map de la STL
	private Dictionary<string, bool> AchievementsStates;
	
	void Awake ()
    {
		// Nom des achievements
		string[] names = {
			"firstMove", "firstKill", "tenKills", "hundredKills", "thousandKills", "noLimit", "thousandKillsBersekers",
			"untouch1min", "untouch5mins", "beginner", "amateur", "ghost", "immortal", "god", "assassin", "masterAssassin",
			"longArm", "oneKilometer", "tenKilometers", "marathon", "hundredKilometers", "thousandKilometers", "milionKilometer",
			"littleHoodlum", "boxer", "clod", "brute", "barbarian"
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
		timedAchievements();
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
			return true;
	}
	
	void firstMoveAchievement()
	{
		string name = "firstMove";
		if (!getState(name))
		{
			Debug.Log("Achivement First Move !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void firstBloodAchievement()
	{
		string name = "firstKill";
		if (!getState(name))
		{
			Debug.Log("Achivement First Blood !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void littleKillerAchievement()
	{
		string name = "tenKills";
		if (!getState(name))
		{
			Debug.Log("Achivement Ten kills !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void killerAchievement()
	{
		string name = "hundredKills";
		if (!getState(name))
		{
			Debug.Log("Achivement a hundred kills !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void serialKillerAchievement()
	{
		string name = "thousandKills";
		if (!getState(name))
		{
			Debug.Log("Achivement 1 thousand kills !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void noLimitAchievement()
	{
		string name = "noLimit";
		if (!getState(name))
		{
			Debug.Log("Achivement No Limit (kill 10 Bersekers) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void serialKillerOfSerialKillerAchievement()
	{
		string name = "thousandKillsBersekers";
		if (!getState(name))
		{
			Debug.Log("Achivement Serial Killer Of Serial Killer (kill 1000 Bersekers) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void uncatchableAchievement()
	{
		string name = "untouch1min";
		if (!getState(name))
		{
			Debug.Log("Achivement Not touch during 1 min !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void reallyUncatchableAchievement()
	{
		string name = "untouch5mins";
		if (!getState(name))
		{
			Debug.Log("Achivement Not touch during 5 min !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void surviveOneMinuteAchievement()
	{
		string name = "beginner";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 1 min !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void surviveTwentyMinutesAchievement()
	{
		string name = "amateur";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 20 mins !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void surviveOneHourAchievement()
	{
		string name = "ghost";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 1 h !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void surviveFourHoursAchievement()
	{
		string name = "immortal";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 4 h !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void surviveTwelveHoursAchievement()
	{
		string name = "god";
		if (!getState(name))
		{
			Debug.Log("Achivement Survive during 12 h !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void assassinAchievement()
	{
		string name = "assassin";
		if (!getState(name))
		{
			Debug.Log("Achivement Kill 5 enemy and not be touch !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void masterAssassinAchievement()
	{
		string name = "masterAssassin";
		if (!getState(name))
		{
			Debug.Log("Achivement Kill 50 enemy and not be touch !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void longArmAchievement()
	{
		string name = "longArm";
		if (!getState(name))
		{
			Debug.Log("Achivement Long Arm (10 kills in the same time) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void sundayWalkerAchievement()
	{
		string name = "oneKilometer";
		if (!getState(name))
		{
			Debug.Log("Achivement Sunday Walker (run on 1 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void dailyJoggingAchievement()
	{
		string name = "tenKilometers";
		if (!getState(name))
		{
			Debug.Log("Achivement Daily Jogging (run on 10 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void marathonAchievement()
	{
		string name = "marathon";
		if (!getState(name))
		{
			Debug.Log("Achivement Marathon (run on 42,195 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void healthWalkAchievement()
	{
		string name = "hundredKilometers";
		if (!getState(name))
		{
			Debug.Log("Achivement Health Walk (run on 100 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void athleticAchievement()
	{
		string name = "thousandKilometers";
		if (!getState(name))
		{
			Debug.Log("Achivement Athletic (run on 1.000 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void dopedAddictAchievement()
	{
		string name = "milionKilometer";
		if (!getState(name))
		{
			Debug.Log("Achivement Doped Addict (run on 1.000.000 km) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void littleHoodlumAchievement()
	{
		string name = "littleHoodlum";
		if (!getState(name))
		{
			Debug.Log("Achivement little Hoodlum (kill 10 enemies in 1 min) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void boxerAchievement()
	{
		string name = "boxer";
		if (!getState(name))
		{
			Debug.Log("Achivement Boxer (kill 25 enemies in 1 min) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void clodAchievement()
	{
		string name = "clod";
		if (!getState(name))
		{
			Debug.Log("Achivement Clod (kill 50 enemies in 1 min) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void bruteAchievement()
	{
		string name = "brute";
		if (!getState(name))
		{
			Debug.Log("Achivement Brute (kill 100 enemies in 1 min) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void barbarianAchievement()
	{
		string name = "barbarian";
		if (!getState(name))
		{
			Debug.Log("Achivement Barbarian (kill 200 enemies in 1 min) !");
			unlockAchivement(texture);
			changeState(name);
		}
	}
	
	void unlockAchivement(Texture textureAchivement)
	{
		// TODO
		audio.PlayOneShot(soundAchivement);
	}
	
	void killPerMin()
	{
		if (timesLimit - timeLimit >= 0)
		{
			// Si on a tuer plus de x ennemis dans un temps de 1 min
			if (cptKillPerMin >= 10)
				littleHoodlumAchievement();
			if (cptKillPerMin >= 25)
				boxerAchievement();
			if (cptKillPerMin >= 50)
				clodAchievement();
			if (cptKillPerMin >= 100)
				bruteAchievement();
			if (cptKillPerMin >= 200)
				barbarianAchievement();
		}
		else
			resetKillPerMin();
	}
	
	void resetKillPerMin()
	{
		timeLimit = 0;
		cptKillPerMin = 0;
	}
	
	public void setTimeNotTouched(float time)
	{
		timeNotTouched = time;
		assassin= false;
	}
	
	public void updateTravel(Vector3 fromWhere, Vector3 to)
	{
		travel += Vector3.Distance(fromWhere, to);
		
		if (travel >= 1000)		// 1 km parcourut
			sundayWalkerAchievement();
		if (travel >= 10000)	// 10 km parcourut
			dailyJoggingAchievement();
		if (travel >= 42195)	// 42,195 km parcourut
			marathonAchievement();
		if (travel >= 100000)	// 100 km parcourut
			healthWalkAchievement();
		if (travel >= 1000000)	// 1.000 km parcourut
			athleticAchievement();
		if (travel >= 10000000)	// 10.000 km parcourut
			dopedAddictAchievement();
	}
	
	void timedAchievements()
	{	
		timeNotTouched += Time.deltaTime;
		timeSurvived += Time.deltaTime;
		timeLimit += Time.deltaTime;
		
		// Débloque les achievements non touché pendant x temps
		if (timeNotTouched >= 60)	// 1 min
			uncatchableAchievement();
		if (timeNotTouched >= 300)	// 5 mins
			reallyUncatchableAchievement();
		
		// Débloque les achievements survivre x temps
		if (timeSurvived >= 60)		// 1 min
			surviveOneMinuteAchievement();
		if (timeSurvived >= 1200)	// 20 mins
			surviveTwentyMinutesAchievement();
		if (timeSurvived >= 3600)	// 1 h
			surviveOneHourAchievement();
		if (timeSurvived >= 14400)	// 4 h
			surviveFourHoursAchievement();
		if (timeSurvived >= 43200)	// 12 h
			surviveTwelveHoursAchievement();
		
		killPerMin();
	}
	
	public void killsAchievements()
	{
		// Compteur d'ennemis tués, débloque les achievements avec un certain nombre
		cptEnemyKilled++;
		cptAssassinKill++;
		cptKillPerMin++;
		
		// Achievements de la série tuer x ennemis
		if (cptEnemyKilled == 1)
			firstBloodAchievement();
		else if (cptEnemyKilled == 10)
			littleKillerAchievement();
		else if (cptEnemyKilled == 100)
			killerAchievement();
		else if (cptEnemyKilled == 1000)
			serialKillerAchievement();
		
		// Achievements de la série assassin
		if (assassin)
		{
			if (cptAssassinKill == 5)
				assassinAchievement();
			else if (cptAssassinKill == 50)
				masterAssassinAchievement();
		}
		
		killBersekerAchievement();
	}
	
	public void killBersekerAchievement()
	{
		// Achievements avec les bersekers
		cptBersekerKilled++;
			
		if (cptBersekerKilled == 10)
			noLimitAchievement();
		else if (cptBersekerKilled == 1000)
			serialKillerOfSerialKillerAchievement();
	}
	
	public void firstMove(Vector3 move)
	{
		if (move != Vector3.zero)
			firstMoveAchievement();
	}
	
	public void multiKills(int lastVal, int newVal)
	{
		if (lastVal - newVal >= 10)
			longArmAchievement();
	}
}