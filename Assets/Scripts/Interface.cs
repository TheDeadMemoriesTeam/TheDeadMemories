﻿using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour 
{
	
	public PlayerController player;
	
	public GUIText gameOverText;
	public GUITexture healthTextureEmpty;
	public GUITexture healthTexture;
	public GUITexture manaTextureEmpty;
	public GUITexture manaTexture;
	public GUITexture scorePanel;
	public GUITexture scoreUnite;
	public GUITexture scoreDizaine;
	public GUITexture scoreCentaine;
	public GUITexture scoreMillier;
	public GUITexture scoreDizMillier;
	
	// Use this for initialization
	void Start () 
	{
		gameOverText.enabled=false;
		
		healthTextureEmpty.pixelInset = new Rect(-32,Screen.height-32,32,32); // position de la texture vide de la barre de vie
		healthTexture.pixelInset = new Rect(-32,Screen.height-32,32,32); // position de la texture de la barre de vie actuelle du joueur
		
		manaTextureEmpty.pixelInset = new Rect(0,Screen.height-70,256,32); // position de la texture vide de la barre de mana
		manaTexture.pixelInset = new Rect(-256,Screen.height-70,256,32); // position de la texture de la barre de mana actuelle du joueur
		
		scorePanel.pixelInset = new Rect(0,Screen.height-140,128,64);
		scoreUnite.pixelInset = new Rect(88,Screen.height-124,16,32);
		scoreDizaine.pixelInset = new Rect(72,Screen.height-124,16,32);
		scoreCentaine.pixelInset = new Rect(56,Screen.height-124,16,32);
		scoreMillier.pixelInset = new Rect(40,Screen.height-124,16,32);
		scoreDizMillier.pixelInset = new Rect(24,Screen.height-124,16,32);
		
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		
		int hp = player.getHitPoints();
		int hpMax = player.getMaxHitPoints();
		int mana = player.getMana();
		int manaMax = player.getManaMax();
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
		int hpBarPxl = (int)hpBar + 32;
		
		healthTextureEmpty.border = new RectOffset(288,0,0,0);
		healthTexture.border = new RectOffset(hpBarPxl,0,0,0);
		
		
		
		// actualisation de la barre de mana
		double manaBar = (double)mana * (256/(double)manaMax);
		int manaBarPxl = (int)manaBar + 256;
		
		manaTextureEmpty.border = new RectOffset(256,0,0,0);
		manaTexture.border = new RectOffset(manaBarPxl,0,0,0);
	
		
		
		
		//// ***     EXPERIENCE     *** ////
		
		// actualisation de l'affichage de l'expérience
		scorePanel.border = new RectOffset(128,0,0,0);
		scoreUnite.texture = Resources.Load("ScoreUn") as Texture2D;
		
		// calcule des chiffres de l'expérience
		int uni = exp - (exp/10)*10;
		int diz = exp/10 - (exp/100)*10;
		int cen = exp/100 - (exp/1000)*10;
		int mil = exp/1000 - (exp/10000)*10;
		int dmi = exp/10000 - (exp/100000)*10;
		
		switch(uni)
		{
		case 0:
			scoreUnite.texture = Resources.Load("ScoreZero") as Texture2D;
			break;
		case 1:
			scoreUnite.texture = Resources.Load("ScoreUn") as Texture2D;
			break;
		case 2:
			scoreUnite.texture = Resources.Load("ScoreDeux") as Texture2D;
			break;
		case 3:
			scoreUnite.texture = Resources.Load("ScoreTrois") as Texture2D;
			break;
		case 4:
			scoreUnite.texture = Resources.Load("ScoreQuatre") as Texture2D;
			break;
		case 5:
			scoreUnite.texture = Resources.Load("ScoreCinq") as Texture2D;
			break;
		case 6:
			scoreUnite.texture = Resources.Load("ScoreSix") as Texture2D;
			break;
		case 7:
			scoreUnite.texture = Resources.Load("ScoreSept") as Texture2D;
			break;
		case 8:
			scoreUnite.texture = Resources.Load("ScoreHuit") as Texture2D;
			break;
		case 9:
			scoreUnite.texture = Resources.Load("ScoreNeuf") as Texture2D;
			break;
		}
		
		if(exp > 9) // dans le cas ou l'expérience est plus grande que 9 on affecte une texture au chiffre des dizaines
		{
			switch(diz)
			{
			case 0:
				scoreDizaine.texture = Resources.Load("ScoreZero") as Texture2D;
				break;
			case 1:
				scoreDizaine.texture = Resources.Load("ScoreUn") as Texture2D;
				break;
			case 2:
				scoreDizaine.texture = Resources.Load("ScoreDeux") as Texture2D;
				break;
			case 3:
				scoreDizaine.texture = Resources.Load("ScoreTrois") as Texture2D;
				break;
			case 4:
				scoreDizaine.texture = Resources.Load("ScoreQuatre") as Texture2D;
				break;
			case 5:
				scoreDizaine.texture = Resources.Load("ScoreCinq") as Texture2D;
				break;
			case 6:
				scoreDizaine.texture = Resources.Load("ScoreSix") as Texture2D;
				break;
			case 7:
				scoreDizaine.texture = Resources.Load("ScoreSept") as Texture2D;
				break;
			case 8:
				scoreDizaine.texture = Resources.Load("ScoreHuit") as Texture2D;
				break;
			case 9:
				scoreDizaine.texture = Resources.Load("ScoreNeuf") as Texture2D;
				break;
			}
			
			if(exp > 99)// dans le cas ou l'expérience est plus grande que 99 on affecte une texture au chiffre des centaines
			{
				switch(cen)
				{
				case 0:
					scoreCentaine.texture = Resources.Load("ScoreZero") as Texture2D;
					break;
				case 1:
					scoreCentaine.texture = Resources.Load("ScoreUn") as Texture2D;
					break;
				case 2:
					scoreCentaine.texture = Resources.Load("ScoreDeux") as Texture2D;
					break;
				case 3:
					scoreCentaine.texture = Resources.Load("ScoreTrois") as Texture2D;
					break;
				case 4:
					scoreCentaine.texture = Resources.Load("ScoreQuatre") as Texture2D;
					break;
				case 5:
					scoreCentaine.texture = Resources.Load("ScoreCinq") as Texture2D;
					break;
				case 6:
					scoreCentaine.texture = Resources.Load("ScoreSix") as Texture2D;
					break;
				case 7:
					scoreCentaine.texture = Resources.Load("ScoreSept") as Texture2D;
					break;
				case 8:
					scoreCentaine.texture = Resources.Load("ScoreHuit") as Texture2D;
					break;
				case 9:
					scoreCentaine.texture = Resources.Load("ScoreNeuf") as Texture2D;
					break;
				}
				
				if(exp > 999)// dans le cas ou l'expérience est plus grande que 999 on affecte une texture au chiffre des milliers
				{
					switch(mil)
					{
					case 0:
						scoreMillier.texture = Resources.Load("ScoreZero") as Texture2D;
						break;
					case 1:
						scoreMillier.texture = Resources.Load("ScoreUn") as Texture2D;
						break;
					case 2:
						scoreMillier.texture = Resources.Load("ScoreDeux") as Texture2D;
						break;
					case 3:
						scoreMillier.texture = Resources.Load("ScoreTrois") as Texture2D;
						break;
					case 4:
						scoreMillier.texture = Resources.Load("ScoreQuatre") as Texture2D;
						break;
					case 5:
						scoreMillier.texture = Resources.Load("ScoreCinq") as Texture2D;
						break;
					case 6:
						scoreMillier.texture = Resources.Load("ScoreSix") as Texture2D;
						break;
					case 7:
						scoreMillier.texture = Resources.Load("ScoreSept") as Texture2D;
						break;
					case 8:
						scoreMillier.texture = Resources.Load("ScoreHuit") as Texture2D;
						break;
					case 9:
						scoreMillier.texture = Resources.Load("ScoreNeuf") as Texture2D;
						break;
					}
					
					if(exp > 9999)// dans le cas ou l'expérience est plus grande que 9999 on affecte une texture au chiffre des dizaines de milliers
					{
						switch(dmi)
						{
						case 0:
							scoreDizMillier.texture = Resources.Load("ScoreZero") as Texture2D;
							break;
						case 1:
							scoreDizMillier.texture = Resources.Load("ScoreUn") as Texture2D;
							break;
						case 2:
							scoreDizMillier.texture = Resources.Load("ScoreDeux") as Texture2D;
							break;
						case 3:
							scoreDizMillier.texture = Resources.Load("ScoreTrois") as Texture2D;
							break;
						case 4:
							scoreDizMillier.texture = Resources.Load("ScoreQuatre") as Texture2D;
							break;
						case 5:
							scoreDizMillier.texture = Resources.Load("ScoreCinq") as Texture2D;
							break;
						case 6:
							scoreDizMillier.texture = Resources.Load("ScoreSix") as Texture2D;
							break;
						case 7:
							scoreDizMillier.texture = Resources.Load("ScoreSept") as Texture2D;
							break;
						case 8:
							scoreDizMillier.texture = Resources.Load("ScoreHuit") as Texture2D;
							break;
						case 9:
							scoreDizMillier.texture = Resources.Load("ScoreNeuf") as Texture2D;
							break;
						}
					}
				}	
			}
		}
		///////////////////////////////////////////////////////
		
		
	}
}
