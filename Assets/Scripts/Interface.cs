using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour 
{
	
	public PlayerController player;
	
	public GUIText experienceText;
	public GUIText gameOverText;
	public GUIText manaText;
	public GUITexture healthTextureEmpty;
	public GUITexture healthTexture;
	public GUITexture manaTextureEmpty;
	public GUITexture manaTexture;
	
	// Use this for initialization
	void Start () 
	{
		experienceText.color = new Color(0,0,255);
		manaText.color = Color.magenta;
		gameOverText.enabled=false;
		
		healthTextureEmpty.pixelInset = new Rect(-32,Screen.height-32,32,32); // position de la texture vide de la barre de vie
		healthTexture.pixelInset = new Rect(-32,Screen.height-32,32,32); // position de la texture de la barre de vie actuelle du joueur
		
		manaTextureEmpty.pixelInset = new Rect(0,Screen.height-70,256,32); // position de la texture vide de la barre de mana
		manaTexture.pixelInset = new Rect(-256,Screen.height-70,256,32); // position de la texture de la barre de mana actuelle du joueur
		
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		
		int hp = player.getHitPoints();
		int hpMax = player.getMaxHitPoints();
		int mana = player.getMana();
		int manaMax = player.getManaMax();
		
		if(hp <= 0)
		{
			hp = 0;
			gameOverText.enabled=true;
			Screen.showCursor = true;
			Screen.lockCursor = false;
		}
		
		
		// actualisation de la barre de vie
		double hpBar = (double)hp * (256/(double)hpMax);
		int hpBarPxl = (int)hpBar + 32;
		
		healthTextureEmpty.border = new RectOffset(288,0,0,0);
		healthTexture.border = new RectOffset(hpBarPxl,0,0,0);
		
		
		
		// actualisation de la barre de mana
		double manaBar = (double)mana * (256/(double)manaMax);
		int manaBarPxl = (int)manaBar + 256;
		
		manaTextureEmpty.border = new RectOffset(256,0,0,0);
		manaTexture.border = new RectOffset(manaBarPxl,0,0,0);
	
		experienceText.text = player.getExperience().ToString();
		manaText.text = player.getMana().ToString() + " / " + player.getManaMax().ToString();
	}
}
