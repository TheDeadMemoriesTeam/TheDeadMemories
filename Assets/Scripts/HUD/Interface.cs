using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour 
{
	
	public PlayerController player;
	public SkillManager playerSkill;
	
	public GUIText gameOverText;

	public GUITexture healthManaTextureEmpty;

	public GUITexture ManaTexture;
	public GUITexture VieTexture;

	public GUITexture FeuOmbres;
	public GUITexture GlaceOmbres;
	public GUITexture VentOmbres;

	public GUITexture FeuCache;
	public GUITexture GlaceCache;
	public GUITexture VentCache;

	// Use this for initialization
	void Start ()
	{
		gameOverText.enabled=false;

		healthManaTextureEmpty.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		healthManaTextureEmpty.border = new RectOffset(0,0,0,511);

		ManaTexture.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		ManaTexture.border = new RectOffset(0,0,0,511);
		
		VieTexture.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		VieTexture.border = new RectOffset(0,0,0,511);
		
		FeuOmbres.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		FeuOmbres.border = new RectOffset(0,0,0,0);
		GlaceOmbres.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		GlaceOmbres.border = new RectOffset(0,0,0,511);
		VentOmbres.pixelInset = new Rect(Screen.width/2-260, -256, 512, 256);
		VentOmbres.border = new RectOffset(0,0,0,511);

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


		if( playerSkill.getSkill(6).getIsBought() )
		{
			FeuCache.border = new RectOffset(0,0,0,0);
		}
		if( playerSkill.getSkill(9).getIsBought() )
		{
			GlaceCache.border = new RectOffset(0,0,0,0);
		}
		if( playerSkill.getSkill(12).getIsBought() )
		{
			VentCache.border = new RectOffset(0,0,0,0);
		}

		// Permet le changement de type de magie
		if (Input.GetKeyDown(KeyCode.F1))
		{
			FeuOmbres.border = new RectOffset(0,0,0,0);
			GlaceOmbres.border = new RectOffset(0,0,0,511);
			VentOmbres.border = new RectOffset(0,0,0,511);
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{	
			FeuOmbres.border = new RectOffset(0,0,0,511);
			GlaceOmbres.border = new RectOffset(0,0,0,0);
			VentOmbres.border = new RectOffset(0,0,0,511);
		}
		else if (Input.GetKeyDown(KeyCode.F3))
		{
			FeuOmbres.border = new RectOffset(0,0,0,511);
			GlaceOmbres.border = new RectOffset(0,0,0,511);
			VentOmbres.border = new RectOffset(0,0,0,0);
		}


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
		double hpBar = (double)hp * (129/(double)hpMax);
		int hpBarPxl = (int)hpBar + 284;
		
		VieTexture.border = new RectOffset(0,0,0,hpBarPxl);
		
		
		
		// actualisation de la barre de mana
		double manaBar = (double)mana * (122/(double)manaMax);
		int manaBarPxl = (int)manaBar + 288;
		
		ManaTexture.border = new RectOffset(0,0,0,manaBarPxl);

	}
}
