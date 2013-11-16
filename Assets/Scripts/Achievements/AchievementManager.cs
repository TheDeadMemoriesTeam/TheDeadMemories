using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
//Namespaces nécessaires pour BinaryFormatter
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class AchievementManager : MonoBehaviour {
	
	
	private List<Achievement> achievements;
	private List<Achievement> achievementsUnlock;
	// Stocke un historique des ennemis tués <temps, nb Ennemis Tués sur la frame>
	private Dictionary<float, int> ennemiesKilledHistorical;
		
	// Annonceur d'achievement
	public AchievementAnnouncer Announcer;
	
	
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
		for (int i = 0 ; i < achievements.Count ; i++)
		{
			TimedKillAchievement achiev = achievements.ElementAt(i) as TimedKillAchievement;
			if (achiev != null)
			{
				if (maxTimeLimit < achiev.getTimeGive())
					maxTimeLimit = achiev.getTimeGive();
			}
		}
		
		// Liste des achievements déjà acquis
		achievementsUnlock = new List<Achievement>();
		
		// Initialisation de l'annonceur
		Announcer = GetComponent<AchievementAnnouncer>();
		
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

	}
	
	// Update is called once per frame
	void Update () 
	{

		// Manage achievements
		for (int i = 0; i < achievements.Count(); i++)
		{
			if (achievements[i].check())
			{
				// Unlocked achievement
				Debug.Log(achievements[i].getName() + ": " + achievements[i].getDescription());
				Announcer.addAchiev(achievements[i].getName());
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
		// Sauvegarde des achievements non réalisés
		//Crée un BinaryFormatter
		//var binFormatter = new BinaryFormatter();
		//Crée un fichier
		//var fileTodo = File.Create(Application.persistentDataPath + "/achievementsTodo.dat");
		//binFormatter.Serialize(fileTodo, achievements);
		//fileTodo.Close();
		
		// Sauvegarde des achievements réalisés
		//Crée un fichier
		//var fileGet = File.Create(Application.persistentDataPath + "/achievementsGet.dat");
		//Sauvegarde les achievements
		//binFormatter.Serialize(fileGet, achievementsGet);
		//fileGet.Close();
	}
	
	void loadAchievements()
	{
		//Si le fichier de sauvegarde existe on les charge
		//if(File.Exists(Application.persistentDataPath + "/achievementsTodo.dat"))
		//{
			//BinaryFormatter pour charger les nouvelles données
		//	var binFormatter = new BinaryFormatter();
			//Ouvre le fichier
		//	var fileTodo = File.Open(Application.persistentDataPath + "/achievementsTodo.dat", FileMode.Open);
			//Charge les achievements non réalisés
		//	achievements = (List<Achievement>)binFormatter.Deserialize(fileTodo);
		//	fileTodo.Close();
		//}
		/*if(File.Exists(Application.persistentDataPath + "/achievementsGet.dat"))
		{
			//BinaryFormatter pour charger les nouvelles données
			var binFormatter = new BinaryFormatter();
			//Ouvre le fichier
			var fileGet = File.Open(Application.persistentDataPath + "/achievementsGet.dat", FileMode.Open);
			//Charge les achievements réalisés
			achievementsGet = (List<Achievement>)binFormatter.Deserialize(fileGet);
			fileGet.Close();
		}*/
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
				achievementsUnlock.Add(achievements.ElementAt(i));
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