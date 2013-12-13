using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour 
{
	
	public PlayerController player;
	
	public GUIText gameOverText;
	public float lifeShaker;

	public GUITexture healthManaTextureEmpty;

	public GUITexture ManaTexture;
	public GUITexture VieTexture;

	// Use this for initialization
	void Start ()
	{
		gameOverText.enabled=false;
		
		lifeShaker = 0;

		healthManaTextureEmpty.pixelInset = new Rect(Screen.width/2-260, -256, 521, 256);
		healthManaTextureEmpty.border = new RectOffset(0,0,0,511);

		ManaTexture.pixelInset = new Rect(Screen.width/2-260, -256, 521, 256);
		ManaTexture.border = new RectOffset(0,0,0,511);
		
		VieTexture.pixelInset = new Rect(Screen.width/2-260, -256, 521, 256);
		VieTexture.border = new RectOffset(0,0,0,511);

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
		double hpBar = (double)hp * (129/(double)hpMax);
		int hpBarPxl = (int)hpBar + 284;
		
		VieTexture.border = new RectOffset(0,0,0,hpBarPxl);
		
		
		
		// actualisation de la barre de mana
		double manaBar = (double)mana * (132/(double)manaMax);
		int manaBarPxl = (int)manaBar + 288;
		
		ManaTexture.border = new RectOffset(0,0,0,manaBarPxl);

	}
}
