using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour 
{
	
	public PlayerController player;
	
	public GUIText gameOverText;
	public float lifeShaker;
	public GUITexture healthTextureEmpty;
	public GUITexture healthTextureEmpty2;
	public GUITexture healthTexture;
	public GUITexture healthTexture2;

	public GUITexture healthManaTextureEmpty;
	
	// Use this for initialization
	void Start ()
	{
		gameOverText.enabled=false;
		
		lifeShaker = 0;
		
		healthTextureEmpty.pixelInset = new Rect(-64,Screen.height-32,64,32); // position de la texture vide de la barre de vie
		healthTextureEmpty2.pixelInset = new Rect(-64,Screen.height-32,64,32); // position de la texture vide de la barre de vie
		healthTexture.pixelInset = new Rect(-64,Screen.height-32,64,32); // position de la texture de la barre de vie actuelle du joueur
		healthTexture2.pixelInset = new Rect(-64,Screen.height-32,64,32); // position de la texture de la barre de vie actuelle du joueur

		healthManaTextureEmpty.pixelInset = new Rect(Screen.width/2-260, -256, 521, 256);
		healthManaTextureEmpty.border = new RectOffset(0,0,0,511);

	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		
		float hp = player.getSkillManager().getPv();
		float hpMax = player.getSkillManager().getPvMax();
		float mana = player.getSkillManager().getMana();
		float manaMax = player.getSkillManager().getManaMax();
		int exp = player.getExperience();
		
		if(hp <= 0)
		{
			hp = 0;
			gameOverText.enabled=true;
			Screen.showCursor = true;
			Screen.lockCursor = false;
		}
		
		
		// actualisation de la barre de vie
		double hpBar = (double)hp * (256/(double)hpMax);
		int hpBarPxl = (int)hpBar + 64;
		
		if((double)hp/(double)hpMax <= 0.25)
		{
			Rect rec1 = new Rect(-64,Screen.height-33,64,32);
			Rect rec2 = new Rect(-65,Screen.height-32,64,32);
			Rect rec3 = new Rect(-64,Screen.height-32,64,32);
			Rect rec4 = new Rect(-65,Screen.height-33,64,32);
			lifeShaker += Time.deltaTime;
			if(lifeShaker >= 0.4)
			{
				healthTextureEmpty.pixelInset = rec1; // position de la texture vide de la barre de vie
				healthTextureEmpty2.pixelInset = rec2; // position de la texture vide de la barre de vie
				healthTexture.pixelInset = rec1; // position de la texture de la barre de vie actuelle du joueur
				healthTexture2.pixelInset = rec2; // position de la texture de la barre de vie actuelle du joueur
				lifeShaker = 0;
			}
			else if(lifeShaker >= 0.3)
			{
				healthTextureEmpty.pixelInset = rec2; // position de la texture vide de la barre de vie
				healthTextureEmpty2.pixelInset = rec3; // position de la texture vide de la barre de vie
				healthTexture.pixelInset = rec2; // position de la texture de la barre de vie actuelle du joueur
				healthTexture2.pixelInset = rec3; // position de la texture de la barre de vie actuelle du joueur
			}
			else if(lifeShaker >= 0.2)
			{
				healthTextureEmpty.pixelInset = rec3; // position de la texture vide de la barre de vie
				healthTextureEmpty2.pixelInset = rec4; // position de la texture vide de la barre de vie
				healthTexture.pixelInset = rec3; // position de la texture de la barre de vie actuelle du joueur
				healthTexture2.pixelInset = rec4; // position de la texture de la barre de vie actuelle du joueur
			}
			else if(lifeShaker >= 0.1)
			{
				healthTextureEmpty.pixelInset = rec4; // position de la texture vide de la barre de vie
				healthTextureEmpty2.pixelInset = rec1; // position de la texture vide de la barre de vie
				healthTexture.pixelInset = rec4; // position de la texture de la barre de vie actuelle du joueur
				healthTexture2.pixelInset = rec1; // position de la texture de la barre de vie actuelle du joueur
			}
		}
		
		healthTextureEmpty.border = new RectOffset(320,0,0,0);
		healthTextureEmpty2.border = new RectOffset(320,0,0,0);
		healthTexture.border = new RectOffset(hpBarPxl,0,0,0);
		healthTexture2.border = new RectOffset(hpBarPxl,0,0,0);
		
		
		
		// actualisation de la barre de mana
		double manaBar = (double)mana * (256/(double)manaMax);
		int manaBarPxl = (int)manaBar + 256;
		
		
	}
}
