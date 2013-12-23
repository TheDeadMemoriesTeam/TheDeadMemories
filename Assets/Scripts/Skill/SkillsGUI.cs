using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillsGUI : MonoBehaviour 
{
	private bool isActive = false;
	private bool skillsOpen = false;
	
	//Fenetre d'achat des skills
	private Rect skillsWindowRect = new Rect(0,0, Screen.width, Screen.height);
	
	public PlayerController player;
	
	public GUIText openSkills;

	// Marge en pixels entre 2 arbres de compétences
	private int marginBetweenSkillTree = 150;
	// Pourcentage de décalage sur la droite après chaque bouton
	private float horizontalMarginBetweenButton = 0.05f;

	// Position sur la scrollBar
	private Vector2 scrollPosition = Vector2.zero;

	// Nombre d'arbre de compétences déjà affichés
	private int nbTreeAlreadyDrawn = 0;
	
	// Use this for initialization
	void Start () 
	{
		openSkills.text = "Press P to buy Skills";
		openSkills.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isActive)
			return;
		
		if (Input.GetKeyUp(KeyCode.P))
		{
			//les stats skills
			player.getSkillManager().update();
			// Met le joueur en pause pour qu'il ne se déplace pas en meme temps qu'il achète ses skills
			player.onPause();
			// Change l'état affiché/masqué du panneau d'achat des skills
			skillsOpen = !skillsOpen;
			// Affiche ou masque le curseur
			Screen.showCursor = !Screen.showCursor;
			Screen.lockCursor = !Screen.lockCursor;
		}
		
		// Affiche ou non le message
		if (!skillsOpen)
			openSkills.enabled = true;
		else
			openSkills.enabled = false;
	}
	
	void OnGUI()
	{
		// Ouvre la fenetre des arbres de compétences
		if (isActive && skillsOpen)
			skillsWindowRect = GUI.Window(1, skillsWindowRect, skillsWindowOpen, "Arbre des compétences");
	}
	
	void skillsWindowOpen(int windowId)
	{
		
		GUILayout.BeginArea(new Rect(20, 20, Screen.width-10, Screen.height-20));

		// Nombre de sous arbre de compétence à afficher
		float nbTree = (int)(player.getSkillManager().getListOfSkills().Count/3);
		scrollPosition = GUI.BeginScrollView(	new Rect(0, 0, Screen.width-30, Screen.height-50),
		                                     	scrollPosition,
		                                     	new Rect(0, 0, Screen.width-50, nbTree*marginBetweenSkillTree+25));

		nbTreeAlreadyDrawn = 0;

		// Affiche les arbres de compétences
		showGUIPassiveSkills(1, 2);
		showGUIMagicSkills(3, 3);

		GUI.EndScrollView();

		GUILayout.EndArea();
	}

	void showGUIMagicSkills(int firstTreePosition, int nbSkillTree)
	{
		List<Skills> listSkills = player.getSkillManager().getListOfSkills();

		// Pour le parcours de la liste des compétences, assigne les bons index
		int beginFor = (firstTreePosition-1)*3;
		int endFor = beginFor + nbSkillTree*3;
		
		// On démarre à 50 pixels du bord supérieur
		// et on se décale du si d'autre arbre sont avant
		int heightFromTop = -50 + nbTreeAlreadyDrawn*marginBetweenSkillTree;
		
		// Parcours de la liste des skills pour le nombre de sous arbre demandés
		// Un tour de boucle affiche 1 arbre
		for (int i = beginFor ; i < endFor ; i+=3)
		{
			heightFromTop += marginBetweenSkillTree;

			// Largeur du bouton en fonction du texte
			float firstSkillButtonWidth = listSkills[i].getName().Length * 9;
			// Met à jour la marge gauche
			float marginLeft = 0.05f*Screen.width;
			// Si le premier skill est déjà acheté
			if (listSkills[i].getIsBought())
			{
				BaseSkills baseSkillRank1 = listSkills[i] as BaseSkills;

				// Le skill étant acheté on l'affiche avec un bouton grisé
				GUI.enabled = false;
				GUI.Button(new Rect(marginLeft,
				                    heightFromTop,
				                    firstSkillButtonWidth,
				                    30),
				           listSkills[i].getName());

				// Met à jour la marge gauche
				marginLeft += (firstSkillButtonWidth + horizontalMarginBetweenButton*Screen.width);

				// Largeur des boutons en fonction du texte
				float firstLittleSkillButtonWidthUp = baseSkillRank1.getNameDamage().Length * 11;
				float firstLittleSkillButtonWidthDown = baseSkillRank1.getNameAd().Length * 11;
				// On affiche les boutons des 2 branches de skills qui suivent
				GUI.enabled = baseSkillRank1.getCostIncDamage() <= player.getExperience();
				if (GUI.Button(new Rect(marginLeft,
				                        heightFromTop-25,
				                        firstLittleSkillButtonWidthUp,
				                        30),
				               baseSkillRank1.getNameDamage()))
				{
					upgradeMagicLittleSkill(baseSkillRank1);
				}
				
				GUI.enabled = baseSkillRank1.getCostIncAd() <= player.getExperience();
				if (GUI.Button(new Rect(marginLeft,
				                        heightFromTop+25,
				                        firstLittleSkillButtonWidthDown,
				                        30),
				               baseSkillRank1.getNameAd()))
				{
					upgradeMagicLittleSkill(baseSkillRank1, false);
				}

				// Met à jour la marge gauche
				if (marginLeft + firstLittleSkillButtonWidthUp > marginLeft + firstLittleSkillButtonWidthDown)
					marginLeft += (firstLittleSkillButtonWidthUp + horizontalMarginBetweenButton*Screen.width);
				else
					marginLeft += (firstLittleSkillButtonWidthDown + horizontalMarginBetweenButton*Screen.width);

				// Vérifie si le second skill peut etre débloqué
				listSkills[i+1].unlockedSkill();

				// Largeur du bouton en fonction du texte
				float secondSkillButtonWidth = listSkills[i+1].getName().Length * 9;
				// Si le second skill est déjà acheté
				if (listSkills[i+1].getIsBought())
				{
					BaseSkills baseSkillRank2 = listSkills[i+1] as BaseSkills;
					
					// Le skill étant acheté on l'affiche avec un bouton grisé
					GUI.enabled = false;
					GUI.Button(new Rect(marginLeft,
					                    heightFromTop,
					                    secondSkillButtonWidth,
					                    30),
					           listSkills[i+1].getName());

					// Met à jour la marge gauche
					marginLeft += (secondSkillButtonWidth + horizontalMarginBetweenButton*Screen.width);

					// Largeur des boutons en fonction du texte
					float secondLittleSkillButtonWidthUp = baseSkillRank2.getNameDamage().Length * 11;
					float secondLittleSkillButtonWidthDown = baseSkillRank2.getNameAd().Length * 11;
					// On affiche les boutons des 2 branches de skills qui suivent
					GUI.enabled = baseSkillRank2.getCostIncDamage() <= player.getExperience();
					if (GUI.Button(new Rect(marginLeft,
					                        heightFromTop-25,
					                        secondLittleSkillButtonWidthUp,
					                        30),
					               baseSkillRank2.getNameDamage()))
					{
						upgradeMagicLittleSkill(baseSkillRank2);
					}
					
					GUI.enabled = baseSkillRank2.getCostIncAd() <= player.getExperience();
					if (GUI.Button(new Rect(marginLeft,
					                        heightFromTop+25,
					                        secondLittleSkillButtonWidthDown,
					                        30),
					               baseSkillRank2.getNameAd()))
					{
						upgradeMagicLittleSkill(baseSkillRank2, false);
					}

					// Met à jour la marge gauche
					if (marginLeft + secondLittleSkillButtonWidthUp > marginLeft + secondLittleSkillButtonWidthDown)
						marginLeft += (secondLittleSkillButtonWidthUp + horizontalMarginBetweenButton*Screen.width);
					else
						marginLeft += (secondLittleSkillButtonWidthDown + horizontalMarginBetweenButton*Screen.width);
					
					// Vérifie si le dernier skill peut etre débloqué
					listSkills[i+2].unlockedSkill();

					// Largeur du bouton en fonction du texte
					float lastSkillButtonWidth = listSkills[i+2].getName().Length * 9;
					// Si le dernier skill est déjà acheté
					if (listSkills[i+2].getIsBought())
					{
						// Le skill étant acheté on l'affiche avec un bouton grisé
						GUI.enabled = false;
						GUI.Button(new Rect(marginLeft,
						                    heightFromTop,
						                    lastSkillButtonWidth,
						                    30),
						           listSkills[i+2].getName());
					}
					// Si le dernier skill est débloqué
					else if (listSkills[i+2].getIsUnlock())
					{
						// On active ou non le bouton si on a suffisamment d'expérience
						GUI.enabled = listSkills[i+2].getPrice() <= player.getExperience();
						
						// Si on achète le skill on met à jour l'expérience du joueur
						if (GUI.Button(new Rect(marginLeft,
						                        heightFromTop,
						                        lastSkillButtonWidth,
						                        30),
						               listSkills[i+2].getName()))
						{
							unlockSkill(listSkills[i+2]);
						}
					}

				}
				// Si le deuxième skill est débloqué
				else if (listSkills[i+1].getIsUnlock())
				{
					// On active ou non le bouton si on a suffisamment d'expérience
					GUI.enabled = listSkills[i+1].getPrice() <= player.getExperience();
					
					// Si on achète le skill on met à jour l'expérience du joueur
					if (GUI.Button(new Rect(marginLeft,
					                        heightFromTop,
					                        secondSkillButtonWidth,
					                        30),
					               listSkills[i+1].getName()))
					{
						unlockSkill(listSkills[i+1]);
					}
				}

			}
			// Si le premier skill est débloqué
			else if (listSkills[i].getIsUnlock())
			{
				// On active ou non le bouton si on a suffisamment d'expérience
				GUI.enabled = listSkills[i].getPrice() <= player.getExperience();
				
				// Si on achète le skill on met à jour l'expérience du joueur
				if (GUI.Button(new Rect(marginLeft,
				                        heightFromTop,
				                        firstSkillButtonWidth,
				                        30),
				               listSkills[i].getName()))
				{
					unlockSkill(listSkills[i]);
				}
			}

			nbTreeAlreadyDrawn++;
		}
	}

	void showGUIPassiveSkills(int firstTreePosition, int nbSkillTree)
	{
		List<Skills> listSkills = player.getSkillManager().getListOfSkills();
		
		// Pour le parcours de la liste des compétences, assigne les bons index
		int beginFor = (firstTreePosition-1)*3;
		int endFor = beginFor + nbSkillTree*3;
		
		// On démarre à 50 pixels du bord supérieur
		// et on se décale du si d'autre arbre sont avant
		int heightFromTop = -50 + nbTreeAlreadyDrawn*marginBetweenSkillTree;
		
		// Parcours de la liste des skills pour le nombre de sous arbre demandés
		// Un tour de boucle affiche 1 arbre
		for (int i = beginFor ; i < endFor ; i+=3)
		{
			heightFromTop += marginBetweenSkillTree;
			
			float firstSkillButtonWidth = listSkills[i].getName().Length * 9;
			// Met à jour la marge gauche
			float marginLeft = 0.05f*Screen.width;
			// Si le premier skill est déjà acheté
			if (listSkills[i].getIsBought())
			{
				PassiveSkills passiveSkillRank1 = listSkills[i] as PassiveSkills;
				
				// Le skill étant acheté on l'affiche avec un bouton grisé
				GUI.enabled = false;
				GUI.Button(new Rect(marginLeft,
				                    heightFromTop,
				                    firstSkillButtonWidth,
				                    30),
				           listSkills[i].getName());
				
				// Met à jour la marge gauche
				marginLeft += (firstSkillButtonWidth + horizontalMarginBetweenButton*Screen.width);
				
				float firstLittleSkillButtonWidthUp = passiveSkillRank1.getNameFirstAd().Length * 11;
				float firstLittleSkillButtonWidthDown = passiveSkillRank1.getNameSecAd().Length * 11;
				// On affiche les boutons des 2 branches de skills qui suivent
				GUI.enabled = passiveSkillRank1.getCostIncFirstAd() <= player.getExperience();
				if (GUI.Button(new Rect(marginLeft,
				                        heightFromTop-25,
				                        firstLittleSkillButtonWidthUp,
				                        30),
				               passiveSkillRank1.getNameFirstAd()))
				{
					upgradePassiveLittleSkill(passiveSkillRank1);
				}
				
				GUI.enabled = passiveSkillRank1.getCostIncSecAd() <= player.getExperience();
				if (GUI.Button(new Rect(marginLeft,
				                        heightFromTop+25,
				                        firstLittleSkillButtonWidthDown,
				                        30),
				               passiveSkillRank1.getNameSecAd()))
				{
					upgradePassiveLittleSkill(passiveSkillRank1, false);
				}
				
				// Met à jour la marge gauche
				if (marginLeft + firstLittleSkillButtonWidthUp > marginLeft + firstLittleSkillButtonWidthDown)
					marginLeft += (firstLittleSkillButtonWidthUp + horizontalMarginBetweenButton*Screen.width);
				else
					marginLeft += (firstLittleSkillButtonWidthDown + horizontalMarginBetweenButton*Screen.width);
				
				// Vérifie si le second skill peut etre débloqué
				listSkills[i+1].unlockedSkill();
				
				float secondSkillButtonWidth = listSkills[i+1].getName().Length * 9;
				// Si le second skill est déjà acheté
				if (listSkills[i+1].getIsBought())
				{
					PassiveSkills passiveSkillRank2 = listSkills[i+1] as PassiveSkills;
					
					// Le skill étant acheté on l'affiche avec un bouton grisé
					GUI.enabled = false;
					GUI.Button(new Rect(marginLeft,
					                    heightFromTop,
					                    secondSkillButtonWidth,
					                    30),
					           listSkills[i+1].getName());
					
					// Met à jour la marge gauche
					marginLeft += (secondSkillButtonWidth + horizontalMarginBetweenButton*Screen.width);
					
					float secondLittleSkillButtonWidthUp = passiveSkillRank2.getNameFirstAd().Length * 11;
					float secondLittleSkillButtonWidthDown = passiveSkillRank2.getNameSecAd().Length * 11;
					// On affiche les boutons des 2 branches de skills qui suivent
					GUI.enabled = passiveSkillRank2.getCostIncFirstAd() <= player.getExperience();
					if (GUI.Button(new Rect(marginLeft,
					                        heightFromTop-25,
					                        secondLittleSkillButtonWidthUp,
					                        30),
					               passiveSkillRank2.getNameFirstAd()))
					{
						upgradePassiveLittleSkill(passiveSkillRank2);
					}
					
					GUI.enabled = passiveSkillRank2.getCostIncSecAd() <= player.getExperience();
					if (GUI.Button(new Rect(marginLeft,
					                        heightFromTop+25,
					                        secondLittleSkillButtonWidthDown,
					                        30),
					               passiveSkillRank2.getNameSecAd()))
					{
						upgradePassiveLittleSkill(passiveSkillRank2, false);
					}
					
					// Met à jour la marge gauche
					if (marginLeft + secondLittleSkillButtonWidthUp > marginLeft + secondLittleSkillButtonWidthDown)
						marginLeft += (secondLittleSkillButtonWidthUp + horizontalMarginBetweenButton*Screen.width);
					else
						marginLeft += (secondLittleSkillButtonWidthDown + horizontalMarginBetweenButton*Screen.width);
					
					// Vérifie si le dernier skill peut etre débloqué
					listSkills[i+2].unlockedSkill();
					
					float lastSkillButtonWidth = listSkills[i+2].getName().Length * 9;
					// Si le dernier skill est déjà acheté
					if (listSkills[i+2].getIsBought())
					{
						// Le skill étant acheté on l'affiche avec un bouton grisé
						GUI.enabled = false;
						GUI.Button(new Rect(marginLeft,
						                    heightFromTop,
						                    lastSkillButtonWidth,
						                    30),
						           listSkills[i+2].getName());
					}
					// Si le dernier skill est débloqué
					else if (listSkills[i+2].getIsUnlock())
					{
						// On active ou non le bouton si on a suffisamment d'expérience
						GUI.enabled = listSkills[i+2].getPrice() <= player.getExperience();
						
						// Si on achète le skill on met à jour l'expérience du joueur
						if (GUI.Button(new Rect(marginLeft,
						                        heightFromTop,
						                        lastSkillButtonWidth,
						                        30),
						               listSkills[i+2].getName()))
						{
							unlockSkill(listSkills[i+2]);
						}
					}
					
				}
				// Si le deuxième skill est débloqué
				else if (listSkills[i+1].getIsUnlock())
				{
					// On active ou non le bouton si on a suffisamment d'expérience
					GUI.enabled = listSkills[i+1].getPrice() <= player.getExperience();
					
					// Si on achète le skill on met à jour l'expérience du joueur
					if (GUI.Button(new Rect(marginLeft,
					                        heightFromTop,
					                        secondSkillButtonWidth,
					                        30),
					               listSkills[i+1].getName()))
					{
						unlockSkill(listSkills[i+1]);
					}
				}
				
			}
			// Si le premier skill est débloqué
			else if (listSkills[i].getIsUnlock())
			{
				// On active ou non le bouton si on a suffisamment d'expérience
				GUI.enabled = listSkills[i].getPrice() <= player.getExperience();
				
				// Si on achète le skill on met à jour l'expérience du joueur
				if (GUI.Button(new Rect(marginLeft,
				                        heightFromTop,
				                        firstSkillButtonWidth,
				                        30),
				               listSkills[i].getName()))
				{
					unlockSkill(listSkills[i]);
				}
			}
			
			nbTreeAlreadyDrawn++;
		}
	}

	void upgradePassiveLittleSkill(PassiveSkills skillRank, bool first = true)
	{
		if (first)
		{
			int newLevel = skillRank.getLvlFirstAd() + 1;
			skillRank.setLvlFirstAd(newLevel);
			player.experienceUpdate(-skillRank.getCostIncFirstAd());
		}
		else
		{
			int newLevel = skillRank.getLvlSecAd() + 1;
			skillRank.setLvlSecAd(newLevel);
			player.experienceUpdate(-skillRank.getCostIncSecAd());
		}
	}

	void upgradeMagicLittleSkill(BaseSkills skillRank, bool first = true)
	{
		if (first)
		{
			int newLevel = skillRank.getLvlDamage() + 1;
			skillRank.setLvlDamage(newLevel);
			player.experienceUpdate(-skillRank.getCostIncDamage());
		}
		else
		{
			int newLevel = skillRank.getLvlAd() + 1;
			skillRank.setLvlAd(newLevel);
			player.experienceUpdate(-skillRank.getCostIncAd());
		}
	}

	void unlockSkill(Skills skill)
	{
		skill.setIsBought(true);
		player.experienceUpdate(-skill.getPrice());
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			isActive = true;
			return;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			isActive = false;
			openSkills.enabled = false;
			return;
		}
	}
}

