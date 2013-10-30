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
	
	// Use this for initialization
	void Start () 
	{
		experienceText.color = new Color(0,0,255);
		//manaText.color = Color.magenta;
		gameOverText.enabled=false;
		healthTextureEmpty.pixelInset = new Rect(-32,Screen.height-32,32,32); // position de la texture vide de la barre de vie
		healthTexture.pixelInset = new Rect(-32,Screen.height-32,32,32); // position de la texture de la barre de vie actuelle du joueur
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		
		int hp = player.getHitPoints();
		
		if(hp <= 0)
		{
			hp = 0;
			gameOverText.enabled=true;
		}
		
		double hpBar = (double)hp * 1.12;
		int hpBarPxl = (int)hpBar + 32;
		
		healthTextureEmpty.border = new RectOffset(256,0,0,0);
		healthTexture.border = new RectOffset(hpBarPxl,0,0,0);
	
		experienceText.text = player.getExperience().ToString();
		//manaText.text = player.getMana().ToString() + " / " + player.getManaMax().ToString();
	}
}
