using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
//Namespaces nécessaires pour BinaryFormatter
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class AchievementManager : MonoBehaviour {
	
	// Texture Achievement
	public GUITexture achievTexture;
	public GUIText achievementGet;
	public GUIText achievText;
	public string waitText2;
	public string waitText3;
	public string waitText4;
	public string waitText5;
	public bool achievGet = false;
	public bool monte = true;
	public int posYAchiev = 0;
	public float posModifier = 0;
	public float counter = 0;
	
	private List<Achievement> achievements;
	private List<Achievement> achievementsGet;
	// Stocke un historique des ennemis tués <temps, nb Ennemis Tués sur la frame>
	private Dictionary<float, int> ennemiesKilledHistorical;
		
	// Son Achievement
	public AudioClip soundAchievement;
	
	// Variables achievements
	private float travelledDistance = 0;	// walking achievement
	private int nbBersekerKilled = 0;		// kill berseker achievement
	private int nbKilledEnemy = 0;			// kill ennemies achievement
	private int lastNbEnemyKilled = 0;		// kill simultaneous achievement
	private bool assassin = true;			// assassin achievement
	private int nbAssassinKill = 0;
	private float timeNotTouched = 0;		// untouch achievement
	private float timeSurvived = 0;			// survive achievement
	private float maxTimeLimit = 0;			// kill x ennemies in x time achievement
	
	// Associe le nom de l'achievement à son état (bool) => équivalent map de la STL
	private Dictionary<string, bool> AchievementsStates;
	
	void Awake ()
    {
		// Add achievements
		achievements = new List<Achievement>();
		// Famille d'achievements Walking
		achievements.Add(new WalkingAchievement(this, "First Move", "Do your first move!", 1));
		achievements.Add(new WalkingAchievement(this, "Sunday Walker", "Walk on 1 km!", 1000));
		achievements.Add(new WalkingAchievement(this, "Daily Jogging", "Walk on 10 km!", 10000));
		achievements.Add(new WalkingAchievement(this, "Marathon", "Walk on 42.195 km!", 42195));
		achievements.Add(new WalkingAchievement(this, "Health Walk", "Walk on 100 km!", 100000));
		achievements.Add(new WalkingAchievement(this, "Athletic", "Walk on 1.000 km!", 1000000));
		achievements.Add(new WalkingAchievement(this, "Doped Addict", "Walk on 10.000 km!", 10000000));
		
		// Famille d'achievements Kill x ennemis
		achievements.Add(new KillingAchievement(this, "First Blood", "Kill for the first time!", 1));
		achievements.Add(new KillingAchievement(this, "Little Killer", "Kill 10 enemies !", 10));
		achievements.Add(new KillingAchievement(this, "Killer", "Kill 100 enemies !", 100));
		achievements.Add(new KillingAchievement(this, "Serial Killer", "Kill 1000 enemies !", 1000));
		
		// Famille d'achievements Kill x bersekers
		achievements.Add(new KillingBersekerAchievement(this, "First Berseker", "Kill your first Berseker !", 1));
		achievements.Add(new KillingBersekerAchievement(this, "No Limit", "Kill 10 Bersekers !", 10));
		achievements.Add(new KillingBersekerAchievement(this, "Serial Killer of Serial Killer", "Kill 100 Bersekers !", 100));
		achievements.Add(new KillingBersekerAchievement(this, "Berserker Genocide", "Kill 1000 Bersekers !", 1000));
		
		// Famille d'achievement kill simultanés
		achievements.Add(new SimultaneousKillsAchievement(this, "Nice Hit", "Kill 5 enemies in the same time", 5));
		achievements.Add(new SimultaneousKillsAchievement(this, "Long Arm", "Kill 10 enemies in the same time", 10));
		
		// Famille d'achievements Assassin
		achievements.Add(new AssassinAchievement(this, "Assassin", "Kill 5 enemies without being touch !", 5));
		achievements.Add(new AssassinAchievement(this, "Master Assassin", "Kill 50 enemies without being touch !", 50));
		
		// Famille d'achievements kill x ennemis en x temps
		achievements.Add(new TimedKillAchievement(this, "Little Hoodlum", "Kill 10 enemies in 1 min", 10, 60));
		achievements.Add(new TimedKillAchievement(this, "Boxer", "Kill 25 enemies in 1 min", 25, 60));
		achievements.Add(new TimedKillAchievement(this, "Clod", "Kill 50 enemies in 1 min", 50, 60));
		achievements.Add(new TimedKillAchievement(this, "Brute", "Kill 100 enemies in 1 min", 100, 60));
		achievements.Add(new TimedKillAchievement(this, "Barbarian", "Kill 200 enemies in 1 min", 200, 60));
		
		// Famille d'achievements survivre x temps
		achievements.Add(new SurvivedAchievement(this, "Beginner", "Survive during 1 min!", 60));
		achievements.Add(new SurvivedAchievement(this, "Amateur", "Survive during 20 min!", 1200));
		achievements.Add(new SurvivedAchievement(this, "Ghost", "Survive during 1 h!", 3600));
		achievements.Add(new SurvivedAchievement(this, "Immortal", "Survive during 4 h!", 14400));
		achievements.Add(new SurvivedAchievement(this, "God", "Survive during 12 h!", 43200));
		
		// Famille d'achievements ne pas etre touché x temps
		achievements.Add(new UntouchedAchievement(this, "Uncatchable", "Not being touched during 1 min !", 60));
		achievements.Add(new UntouchedAchievement(this, "Really Uncatchable", "Not being touched during 5 min !", 300));
		
		// Récupère l'interval de temps maximal à conserver en historique pour les TimedKillAchievement
		for (int i = 0 ; i < achievements.Count() ; i++)
		{
			TimedKillAchievement achiev = achievements.ElementAt(i) as TimedKillAchievement;
			if (achiev != null)
			{
				if (maxTimeLimit < achiev.getTimeGive())
					maxTimeLimit = achiev.getTimeGive();
			}
		}
		
		// Liste des achievements déjà acquis
		achievementsGet = new List<Achievement>();
		
		// Historique des ennemis tués
		ennemiesKilledHistorical = new Dictionary<float, int>();
    }
	
	// Use this for initialization
	void Start () 
	{
		//File.Delete("/achievements.dat");
		//PlayerPrefs.DeleteAll();
		loadAchievements();
		// Supprime de la liste des achievements a check tous ceux déjà réalisés et les ajoute à une liste de ceux réalisés
		refreshListAchievements();
			
		// positionne la texture d'affichage des achievements cachée au début du jeu
		achievTexture.pixelInset = new Rect(Screen.width-256, -128, 256, 128);
		achievementGet.pixelOffset = new Vector2(Screen.width-215, posYAchiev-35);
		achievText.pixelOffset = new Vector2(Screen.width-215, posYAchiev-75);
		achievText.text = "000";
		waitText2 = "000";
		waitText3 = "000";
		waitText4 = "000";
		waitText5 = "000";
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(achievGet)
		{
			posModifier += Time.deltaTime;
			if(posModifier > 0.05)
			{
				if(monte)
				{
					posYAchiev += 8;
					if(posYAchiev > 128)
					{
						posYAchiev = 128;
						monte = false;
					}
				}
				else
				{
					counter += 8;
				}
				
				if(!monte && counter > 256)
				{
					posYAchiev -= 8;
					if(posYAchiev < 0)
					{
						posYAchiev = 0;
						monte = true;
						counter = 0;
						newAchievement();
						
						if(achievText.text == "000")
							achievGet = false;
						
					}
				}
				posModifier = 0;
			}
			achievTexture.pixelInset = new Rect(Screen.width-256, posYAchiev-128, 256, 128);
			achievementGet.pixelOffset = new Vector2(Screen.width-215, posYAchiev-35);
			achievText.pixelOffset = new Vector2(Screen.width-215, posYAchiev-75);
		}

		// Manage achievements
		for (int i = 0; i < achievements.Count(); i++)
		{
			if (achievements[i].check())
			{
				// Unlocked achievement
				Debug.Log(achievements[i].getName() + ": " + achievements[i].getDescription());
				// Supprime de la liste des achievements a check tous ceux déjà réalisés
				refreshListAchievements();
			}
		}
		
		updateTimes();
		// Ajoute à l'historique des kills le nombre d'ennemis tués sur la frame (que si différent de 0 !)
		if (getNbSimultaneouslyKilledEnemy() > 0)
			ennemiesKilledHistorical.Add(Time.time, getNbSimultaneouslyKilledEnemy());
		lastNbEnemyKilled = nbKilledEnemy;
		// Vide l'historique pour conserver l'historique que sur la période voulue
		updateEnnemiesKilledHistorical();
	}
	
	public void saveAchievements()
	{
		//Crée un BinaryFormatter
		var binFormatter = new BinaryFormatter();
		//Crée un fichier
		var file = File.Create(Application.persistentDataPath + "/achievements.dat");
		//Sauvegarde les achievements
		binFormatter.Serialize(file, AchievementsStates);
        
		file.Close();
		
		// Sauvegarde les variables dans le PlayerPrefs
//		PlayerPrefs.SetInt("nbKilledEnemy", nbKilledEnemy);
//		PlayerPrefs.SetInt("cptBersekerKilled", cptBersekerKilled);
//		PlayerPrefs.SetInt("cptAssassinKill", cptAssassinKill);
//		PlayerPrefs.SetFloat("timeNotTouched", timeNotTouched);
//		PlayerPrefs.SetFloat("timeSurvived", timeSurvived);
//		PlayerPrefs.SetFloat("travelledDistance", travelledDistance);
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
//			nbKilledEnemy = PlayerPrefs.GetInt("nbKilledEnemy");
//			cptBersekerKilled = PlayerPrefs.GetInt("cptBersekerKilled");
//			cptAssassinKill = PlayerPrefs.GetInt("cptAssassinKill");
//			timeNotTouched = PlayerPrefs.GetFloat("timeNotTouched");
//			timeSurvived = PlayerPrefs.GetFloat("timeSurvived");
//			travelledDistance = PlayerPrefs.GetFloat("travelledDistance");
		}
	}
	
	void unlockAchievement(string nameAchievement)
	{
		// TODO///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		if(achievText.text == "000")
			achievText.text = nameAchievement;
		else if(waitText2 == "000")
			waitText2 = nameAchievement;
		else if(waitText3 == "000")
			waitText3 = nameAchievement;
		else if(waitText4 == "000")
			waitText4 = nameAchievement;
		else
			waitText5 = nameAchievement;
		
		achievGet = true;
		audio.PlayOneShot(soundAchievement);
	}
	
	// met a jour les achievements à afficher
	void newAchievement()
	{
		
		achievText.text = waitText2;
		waitText2 = waitText3;
		waitText3 = waitText4;
		waitText4 = waitText5;
		waitText5 = "000";
		
	}
	
	
	
	
	
	
	
	// Méthodes permettant un Update des variables des achievements
	public void updateTravel(Vector3 fromWhere, Vector3 to)
	{
		travelledDistance += Vector3.Distance(fromWhere, to);
	}
	
	public void updateKills()
	{
		nbKilledEnemy++;
		nbAssassinKill++;
	}
	
	public void updateTimeNotTouched(float time)
	{
		timeNotTouched = time;
		assassin = false;
	}
	
	void updateTimes()
	{	
		timeNotTouched += Time.deltaTime;
		timeSurvived += Time.deltaTime;
	}
	
	public void updateKillsBerseker()
	{
		nbBersekerKilled++;
	}
	
	void updateEnnemiesKilledHistorical()
	{
		// Parcours tout l'historique pour supprimer les enregistrements en dehors de la période maxTimeLimit
		for (int i = 0 ; i < ennemiesKilledHistorical.Count() ; i++)
		{
			if (!isValidDuration(i, maxTimeLimit))
				ennemiesKilledHistorical.Remove(ennemiesKilledHistorical.ElementAt(i).Key);
		}
	}
	
	bool isValidDuration(int index, float duration)
	{
		// condition pour vérifier que le temps est dans l'espace temps définit avec duration
		return ennemiesKilledHistorical.ElementAt(index).Key >= (Time.time - duration);
	}
	
	void refreshListAchievements()
	{
		// Parcours la liste des achievements pour retirer ceux déjà réalisés et les ajoute à une liste de ceux réalisés
		for (int i = 0 ; i < achievements.Count() ; i++)
		{
			if (achievements.ElementAt(i).getAchieved())
			{
				achievementsGet.Add(achievements.ElementAt(i));
				achievements.RemoveAt(i);
			}
		}
	}
	
	// Accesseurs
	public int getNbKilledEnemy()
	{
		return nbKilledEnemy;
	}
	
	public int getNbKilledBerseker()
	{
		return nbBersekerKilled;
	}
	
	public int getNbSimultaneouslyKilledEnemy()
	{
		return nbKilledEnemy - lastNbEnemyKilled;	
	}
	
	public float getTravelledDistance()
	{
		return travelledDistance;
	}
	
	public float getUntouchedTime()
	{
		return timeNotTouched;
	}
	
	public float getSurvivedTime()
	{
		return timeSurvived;
	}
	
	public int getNbAssassinKills()
	{
		return nbAssassinKill;
	}
	
	public bool getAssassin()
	{
		return assassin;
	}
	
	public int getNbEnnemiesKilledPerDuration(float duration)
	{
		int nbKillPerDuration = 0;
		
		// Parcours l'historique des ennemis tués pour compté leur nombre
		// Le comptage se fait sur la période définit par le paramètre duration
		for (int i = 0 ; i < ennemiesKilledHistorical.Count() ; i++)
		{
			if (isValidDuration(i, duration))
				nbKillPerDuration += ennemiesKilledHistorical.ElementAt(i).Value;
		}
		
		return nbKillPerDuration;
	}
}