using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour 
{

	/*****     Objets      *****/
	public PlayerController player;
	public SkillManager playerSkill;
	/***** * * * * * * * * *****/

	// Text du game over
	public GUIText gameOverText;

	/*****     Gestion de la barre de vie     *****/
	//texture de la barre de vie de base
	public GUITexture healthManaTextureEmpty;

	// texture de la boule de mana
	public GUITexture ManaTexture;
	// texture de la boule de vie
	public GUITexture VieTexture;

	// texture n&b de la case element feu
	public GUITexture FeuOmbres;
	// texture n&b de la case element glace
	public GUITexture GlaceOmbres;
	// texture n&b de la case element vent
	public GUITexture VentOmbres;

	// texture de bois pour cacher la case element feu
	public GUITexture FeuCache;
	// texture de bois pour cacher la case element glace
	public GUITexture GlaceCache;
	// texture de bois pour cacher la case element vent
	public GUITexture VentCache;
	/***** * * * * * * * * * * * * * * * * * * *****/

	// texte servant à l'affichage de l'expérience
	public GUIText playerXp;

	// Use this for initialization
	void Start ()
	{
		// cache le texte de Game Over
		gameOverText.enabled=false;

		// placement de la barre de vie vide sur l'écran
		healthManaTextureEmpty.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		healthManaTextureEmpty.border = new RectOffset(0,0,0,511);

		// placement de la texture du mana (au maximum) dans la barre de vie
		ManaTexture.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		ManaTexture.border = new RectOffset(0,0,0,511);

		// placement de la texture de la vie (au maximum) dans la barre de vie
		VieTexture.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		VieTexture.border = new RectOffset(0,0,0,511);

		// on place les ombres des éléments pour les griser
		// sauf pour le feu car pré-selectionné
		FeuOmbres.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		FeuOmbres.border = new RectOffset(0,0,0,0);
		GlaceOmbres.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		GlaceOmbres.border = new RectOffset(0,0,0,511);
		VentOmbres.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		VentOmbres.border = new RectOffset(0,0,0,511);

		// on cache les trois éléments car il ne sont pas achetés
		FeuCache.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		FeuCache.border = new RectOffset(0,0,0,511);
		GlaceCache.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		GlaceCache.border = new RectOffset(0,0,0,511);
		VentCache.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		VentCache.border = new RectOffset(0,0,0,511);
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{

		// on test si l'élément feu est acheté
		if( playerSkill.getSkill(6).getIsBought() )
		{
			// on enleve le cache de l'élément feu
			FeuCache.border = new RectOffset(0,0,0,0);
		}
		// on test si l'élément glace est acheté
		if( playerSkill.getSkill(9).getIsBought() )
		{
			// on enleve le cache de l'élément glace
			GlaceCache.border = new RectOffset(0,0,0,0);
		}
		// on test si l'élément vent est acheté
		if( playerSkill.getSkill(12).getIsBought() )
		{
			// on enleve le cache de l'élément vent
			VentCache.border = new RectOffset(0,0,0,0);
		}

		// Permet l'affichage du changement de type de magie
		// si on selectionne l'élément feu
		if (Input.GetKeyDown(KeyCode.F1))
		{
			// on verifie si l'élément est acheté et on grise les deux autres
			if(playerSkill.getSkill(6).getIsBought())
			{

				FeuOmbres.border = new RectOffset(0,0,0,0);
			}
			GlaceOmbres.border = new RectOffset(0,0,0,511);
			VentOmbres.border = new RectOffset(0,0,0,511);
		}
		// si on selectionne l'élément glace
		else if (Input.GetKeyDown(KeyCode.F2))
		{	
			FeuOmbres.border = new RectOffset(0,0,0,511);
			// on verifie si l'élément est acheté et on grise les deux autres
			if(playerSkill.getSkill(9).getIsBought())
			{
				GlaceOmbres.border = new RectOffset(0,0,0,0);
			}
			VentOmbres.border = new RectOffset(0,0,0,511);
		}
		// si on selectionne l'élément vent
		else if (Input.GetKeyDown(KeyCode.F3))
		{
			FeuOmbres.border = new RectOffset(0,0,0,511);
			GlaceOmbres.border = new RectOffset(0,0,0,511);
			// on verifie si l'élément est acheté et on grise les deux autres
			if(playerSkill.getSkill(12).getIsBought())
			{
				VentOmbres.border = new RectOffset(0,0,0,0);
			}
		}


		/*****     Affichage du score     *****/
		// gestion de la police et de la position de l'expérience du joueur
		// selon la taille du nombre
		if(player.getExperience() < 10)
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-5, 50);
			playerXp.fontSize = 35;
		}
		else if(player.getExperience() < 100)
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-10, 48);
			playerXp.fontSize = 33;
		}
		else if(player.getExperience() < 1000)
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-20, 46);
			playerXp.fontSize = 31;
		}
		else if(player.getExperience() < 10000)
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-25, 44);
			playerXp.fontSize = 29;
		}
		else if(player.getExperience() < 100000)
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-30, 42);
			playerXp.fontSize = 27;
		}
		else if(player.getExperience() < 1000000)
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-30, 40);
			playerXp.fontSize = 22;
		}
		else if(player.getExperience() < 10000000)
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-30, 38);
			playerXp.fontSize = 19;
		}
		else if(player.getExperience() < 100000000)
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-35, 36);
			playerXp.fontSize = 17;
		}
		else if(player.getExperience() < 1000000000)
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-35, 34);
			playerXp.fontSize = 16;
		}
		else
		{
			playerXp.pixelOffset = new Vector2(Screen.width/2-35, 32);
			playerXp.fontSize = 15;
		}
		playerXp.text = player.getExperience().ToString();
		/***** * * * * * * * * * * * * * *****/

		/*****     Gestion de la vie et du mana     *****/
		float hp = player.getSkillManager().getPv();
		float hpMax = player.getSkillManager().getPvMax();
		float mana = player.getSkillManager().getMana();
		float manaMax = player.getSkillManager().getManaMax();

		// si la vie est inférieure à 0
		if(hp <= 0)
		{
			// on affiche le Game Over et le menu de fin de jeu
			hp = 0;
			gameOverText.enabled=true;
			Screen.showCursor = true;
			Screen.lockCursor = false;
			if (Input.anyKeyDown)
				Application.LoadLevel("MainMenuScene");
		}
		
		
		// actualisation de la barre de vie
		double hpBar = (double)hp * (129/(double)hpMax);
		int hpBarPxl = (int)hpBar + 284;
		
		VieTexture.border = new RectOffset(0,0,0,hpBarPxl);
		
		
		
		// actualisation de la barre de mana
		double manaBar = (double)mana * (122/(double)manaMax);
		int manaBarPxl = (int)manaBar + 288;
		
		ManaTexture.border = new RectOffset(0,0,0,manaBarPxl);

	}
}
