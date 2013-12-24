using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager{
	
	private AchievementManager achievementManager;
	private SkillManager skillManager;

	// Chemin vers les fichiers de sauvegarde
	private string achievementPath = "./save/achievement.dat";
	private string skillsPath = "./save/skills.dat";


	public SaveManager(AchievementManager otherA, SkillManager otherS)
	{
		achievementManager = otherA;
		skillManager = otherS;
	}

	// Fonction de sauvegarde
	public void save()
	{
		saveSkills();
		saveAchievements();

		Debug.Log("save completed");
	}

	// Fonction de chargement
	public void load()
	{
		loadSkills();
		loadAchievements();
		
		Debug.Log("save loaded");
	}

	/****************************/
	/* Gestion des achievements */
	/****************************/
	private void saveAchievements()
	{
		List<Achievement> achievements = achievementManager.getAchievementsUnlocked();
		
		List<string> achievementList = new List<string>();
		achievementList.Capacity = achievements.Capacity;
		for (int i=0; i<achievements.Count; i++)
			achievementList.Add(achievements[i].getName());
		
		// Créé le formater
		BinaryFormatter formater = new BinaryFormatter();
		// Crée le fichier
		Stream saveFile = File.Create(achievementPath);
		// Sauvegarde les achivements
		formater.Serialize(saveFile, achievementList);
		// Libère la mémoire
		saveFile.Close();

		Debug.Log("save achievements");
	}

	private void loadAchievements()
	{
		// Si le fichier existe
		if(File.Exists(achievementPath))
		{
			// Créé le formateur
			BinaryFormatter formater = new BinaryFormatter();
			
			// Créé le fichier
			Stream file = File.Open (achievementPath, FileMode.Open);
			
			// Charge la liste des achievements
			List<string> achievementsLoaded = formater.Deserialize(file) as List<string>;
			
			achievementManager.loadAchievements(achievementsLoaded);
			
			file.Close();
			
			Debug.Log("load achievements");
		}
	}

	/***************************/
	/* Gestion des compétences */
	/***************************/
	private void saveSkills()
	{
		List<string> skills = new List<string>();
		List<Skills> skillList = skillManager.getListOfSkills();

		Skills skill;
		for (int i=0; i<skillList.Count; i++)
		{
			skill = skillList[i];
			if (skill.getIsBought())
			{
				if (skill.GetType().ToString() == "PassiveSkills")
				{
					// Récupération du nom et des niveaux
					PassiveSkills tmp = skill as PassiveSkills;
					skills.Add (tmp.getName());
					skills.Add (tmp.getLvlFirstAd().ToString());
					skills.Add (tmp.getLvlSecAd().ToString());
				}
				else if (skill.GetType().ToString() == "PorteeSkills" ||
				         skill.GetType().ToString() == "ZoneSkills")
				{
					// Récupération du non et des niveaux
					BaseSkills tmp = skill as BaseSkills;
					skills.Add (tmp.getName());
					skills.Add (tmp.getLvlAd().ToString());
					skills.Add (tmp.getLvlDamage().ToString());
				}
				else
				{
					// Récupération du nom
					skills.Add (skill.getName());
				}
				Debug.Log(skill.getName());
			}
		}

		// Créé le formater
		BinaryFormatter formater = new BinaryFormatter();
		// Crée le fichier
		Stream saveFile = File.Create(skillsPath);
		// Sauvegarde les achivements
		formater.Serialize(saveFile, skills);
		// Libère la mémoire
		saveFile.Close();
	}

	private void loadSkills()
	{
		// Si le fichier existe
		if(File.Exists(skillsPath))
		{
			// Créé le formateur
			BinaryFormatter formater = new BinaryFormatter();
			// Créé le fichier
			Stream file = File.Open (skillsPath, FileMode.Open);
			// Charge la liste des compétences
			List<string> skillsLoaded = formater.Deserialize(file) as List<string>;

			// Récupère la liste des compétences du manager
			List<Skills> skillsList = skillManager.getListOfSkills();
			// Réinitiale la liste
			for (int i=0; i<skillsList.Count; i++)
				skillsList[i].setIsBought(false);

			// Récupère les compétences déjà achetées
			int index;
			for (int i=0; i<skillsLoaded.Count; i++)
			{
				index = skillsList.FindIndex(
					delegate(Skills obj) {
					return obj.getName() == skillsLoaded[i];
				});
				// Si la compétence a été trouvée
				if(index != -1)
				{
					// Si c'est une compétence passive
					PassiveSkills passive = skillsList[index] as PassiveSkills;
					if (passive != null)
					{
						passive.setIsBought(true);
						passive.setLvlFirstAd(int.Parse (skillsLoaded[i+1]));
						passive.setLvlSecAd(int.Parse (skillsLoaded[i+2]));
						skillsList[index] = passive;
						i+=2;
					}
					else
					{
						// Si c'est une compétence héritant de BaseSkills
						PorteeSkills portee = skillsList[index] as PorteeSkills;
						if (portee != null)
						{
							portee.setIsBought(true);
							portee.setLvlAd(int.Parse (skillsLoaded[i+1]));
							portee.setLvlDamage(int.Parse (skillsLoaded[i+2]));
							skillsList[index] = portee;
							i+=2;
						}
						else
						{
							// Si c'est une compétence héritant de BaseSkills
							ZoneSkills zone = skillsList[index] as ZoneSkills;
							if (zone != null)
							{
								zone.setIsBought(true);
								zone.setLvlAd(int.Parse (skillsLoaded[i+1]));
								zone.setLvlDamage(int.Parse (skillsLoaded[i+2]));
								skillsList[index] = zone;
								i+=2;
							}
							else
							{
								// Sinon
								skillsList[index].setIsBought(true);
							}
						}
					}
					Debug.Log(skillsList[index].getName());
				}
			}
			
			// Retrouve les compétences débloquées
			for (int i=0; i<skillsList.Count; i++)
				skillsList[i].unlockedSkill();

			skillManager.setListOfSkills(skillsList);

			file.Close();
		}
	}

}
