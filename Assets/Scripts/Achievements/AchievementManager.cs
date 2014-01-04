using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
//Namespaces nécessaires pour BinaryFormatter
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class AchievementManager : MonoBehaviour 
{

	// Pause
	private bool pause = false;

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
	private Vector3 playerPosition = Vector3.zero;	// zone achievement
	
	// Associe le nom de l'achievement à son état (bool) => équivalent map de la STL
	private Dictionary<string, bool> AchievementsStates;
	
	void Awake ()
    {
		// Add achievements
		achievements = new List<Achievement>();
		// Famille d'achievements Walking
		achievements.Add(new WalkingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev1"), "Do your first move!", 1));
		achievements.Add(new WalkingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev2"), "Walk on 1 km!", 1000));
		achievements.Add(new WalkingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev3"), "Walk on 10 km!", 10000));
		achievements.Add(new WalkingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev4"), "Walk on 42.195 km!", 42195));
		achievements.Add(new WalkingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev5"), "Walk on 100 km!", 100000));
		achievements.Add(new WalkingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev6"), "Walk on 1.000 km!", 1000000));
		achievements.Add(new WalkingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev7"), "Walk on 10.000 km!", 10000000));
		
		// Famille d'achievements Kill x ennemis
		achievements.Add(new KillingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev8"), "Kill for the first time!", 1));
		achievements.Add(new KillingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev9"), "Kill 10 enemies !", 10));
		achievements.Add(new KillingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev10"), "Kill 100 enemies !", 100));
		achievements.Add(new KillingAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev11"), "Kill 1000 enemies !", 1000));
		
		// Famille d'achievements Kill x bersekers
		achievements.Add(new KillingBersekerAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev12"), "Kill your first Berseker !", 1));
		achievements.Add(new KillingBersekerAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev13"), "Kill 10 Bersekers !", 10));
		achievements.Add(new KillingBersekerAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev14"), "Kill 100 Bersekers !", 100));
		achievements.Add(new KillingBersekerAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev15"), "Kill 1000 Bersekers !", 1000));
		
		// Famille d'achievement kill simultanés
		achievements.Add(new SimultaneousKillsAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev16"), "Kill 5 enemies in the same time", 5));
		achievements.Add(new SimultaneousKillsAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev17"), "Kill 10 enemies in the same time", 10));
		
		// Famille d'achievements Assassin
		achievements.Add(new AssassinAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev18"), "Kill 5 enemies without being touch !", 5));
		achievements.Add(new AssassinAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev19"), "Kill 50 enemies without being touch !", 50));
		
		// Famille d'achievements kill x ennemis en x temps
		achievements.Add(new TimedKillAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev20"), "Kill 10 enemies in 1 min", 10, 60));
		achievements.Add(new TimedKillAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev21"), "Kill 25 enemies in 1 min", 25, 60));
		achievements.Add(new TimedKillAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev22"), "Kill 50 enemies in 1 min", 50, 60));
		achievements.Add(new TimedKillAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev23"), "Kill 100 enemies in 1 min", 100, 60));
		achievements.Add(new TimedKillAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev24"), "Kill 200 enemies in 1 min", 200, 60));
		
		// Famille d'achievements survivre x temps
		achievements.Add(new SurvivedAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev25"), "Survive during 1 min!", 60));
		achievements.Add(new SurvivedAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev26"), "Survive during 20 min!", 1200));
		achievements.Add(new SurvivedAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev27"), "Survive during 1 h!", 3600));
		achievements.Add(new SurvivedAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev28"), "Survive during 4 h!", 14400));
		achievements.Add(new SurvivedAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev29"), "Survive during 12 h!", 43200));
		
		// Famille d'achievements ne pas etre touché x temps
		achievements.Add(new UntouchedAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev30"), "Not being touched during 1 min !", 60));
		achievements.Add(new UntouchedAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev31"), "Not being touched during 5 min !", 300));

		// Famille d'achievements avec triggers de zone
		achievements.Add(new ZoneAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev32"), "Arrive to the castle", "castle"));
		achievements.Add(new ZoneAchievement(this, LanguageManager.Instance.GetTextValue("Achievement.nameAchiev33"), "Discover the village", "village"));
		
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
		// Supprime de la liste des achievements a check tous ceux déjà réalisés et les ajoute à une liste de ceux réalisés
		refreshListAchievements();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pause)
			return;

		// Manage achievements
		for (int i = 0; i < achievements.Count(); i++)
		{
			if (achievements[i].check())
			{
				// Unlocked achievement
				if (Announcer != null)
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
	
	public void loadAchievements(List<string> achievementsGet)
	{
		// Parcours la liste des achievements aquis pour mettre à jour la liste générale des achievements
		for (int i = 0 ; i < achievementsGet.Count() ; i++)
		{
			for (int j = 0 ; j < achievements.Count() ; j++)
			{
				if (achievements.ElementAt(j).getName() == achievementsGet.ElementAt(i))
					achievements.ElementAt(j).setAchieved();
			}
		}

		refreshListAchievements();
	}

	public List<Achievement> getAchievementsLocked()
	{
		return achievements;
	}

	public List<Achievement> getAchievementsUnlocked()
	{
		return achievementsUnlock;
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

	public void setNbKilledEnemy(int nb)
	{
		nbKilledEnemy = nb;
	}
	
	public int getNbKilledBerseker()
	{
		return nbBersekerKilled;
	}

	public void setNbKilledBerseker(int nb)
	{
		nbBersekerKilled = nb;
	}
	
	public int getNbSimultaneouslyKilledEnemy()
	{
		return nbKilledEnemy - lastNbEnemyKilled;	
	}
	
	public float getTravelledDistance()
	{
		return travelledDistance;
	}

	public void setTravelledDistance(float distance)
	{
		travelledDistance = distance;
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

	public void setNbAssassinKills(int nb)
	{
		nbAssassinKill = nb;
	}
	
	public bool getAssassin()
	{
		return assassin;
	}

	public void setPause(bool state)
	{
		pause = state;
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

	public Vector3 getPlayerPos()
	{
		return playerPosition;
	}

	public void setPlayerPos(Vector3 pos)
	{
		playerPosition = pos;
	}
}