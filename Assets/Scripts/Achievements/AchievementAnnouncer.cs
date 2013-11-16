using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class AchievementAnnouncer : MonoBehaviour
{	
	private int compteurAchiev;
	private float compteurTemps;
	private List<string> achievList;
	private bool textApparu;
	
	public GUITexture texture1;
	public GUITexture texture2;
	public GUIText text1;
	public GUIText text2;
	
	// Son Achievement
	public AudioClip soundAchievement;
	
	// Use this for initialization
	void Start ()
	{
		compteurTemps = 0;
		achievList = new List<string>();	
		achievList.Clear();
		textApparu = false;
		
		// on cache les texture
		texture1.pixelInset = new Rect(100, -64, 256, 64);
		texture2.pixelInset = new Rect(100, -64, 256, 64);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( achievList.Count > 0 )
		{
			if(compteurTemps == 0)
			{
				audio.PlayOneShot(soundAchievement);
			}
			
			// On actualise le temps actuel de l'affichage
			compteurTemps += Time.deltaTime;
			
			// apparition en fondu de la texture
			if(compteurTemps < 1)
			{
				
				// on réaffecte les nouvelle couleurs au texture
				texture1.color = new Color(0.5f,0.5f,0.5f,0.5f*compteurTemps);
				texture2.color = new Color(0.5f,0.5f,0.5f,0.5f*compteurTemps);
				
				texture1.pixelInset = new Rect(Screen.width/2 - 192, 75, 256, 64);
				texture2.pixelInset = new Rect(Screen.width/2 - 64, 50, 256, 64);
				
			}
			
			// apparition du texte
			if(compteurTemps > 1 && !textApparu)
			{
				// on place le texte
				text1.pixelOffset = new Vector2(Screen.width/2 - 170, 120);
				text2.pixelOffset = new Vector2(Screen.width/2 - 54, 90);
				// on assigne le nom de l'achievement
				text2.text = achievList[0];;
				
				text1.color = new Color(0.2f,0.5f,0f,0.5f);
				text2.color = new Color(0.7f,0f,0f,0.5f);
				
				textApparu = true;
			}
			
			// mouvement de la texture
			if(compteurTemps > 1 && compteurTemps < 3)
			{
				// fait bouger la texture 
				texture1.pixelInset = new Rect(Screen.width/2 - 128 - compteurTemps*64, 75, 256, 64);
				texture2.pixelInset = new Rect(Screen.width/2 - 128 + compteurTemps*64, 50, 256, 64);
			}
			
			// disparition en fondu de la texture + fin du mouvement
			if(compteurTemps > 3 && compteurTemps < 5)
			{
				
				// fait bouger la texture 
				texture1.pixelInset = new Rect(Screen.width/2 - 128 - compteurTemps*64, 75, 256, 64);
				texture2.pixelInset = new Rect(Screen.width/2 - 128 + compteurTemps*64, 50, 256, 64);
				
				// fait disparaitre la texture
				float factor = compteurTemps - 3f;
				texture1.color = new Color(0.5f,0.5f,0.5f,0.5f - 0.25f*factor);
				texture2.color = new Color(0.5f,0.5f,0.5f,0.5f - 0.25f*factor);
				
			}
			
			// disparition en fondu du texte
			if(compteurTemps > 5 && compteurTemps < 6)
			{
				
				// fait disparaitre le texte
				float factor = compteurTemps - 5f;
				text1.color = new Color(0.2f,0.5f,0f,0.5f - 0.5f*factor);
				text2.color = new Color(0.7f,0f,0f,0.5f - 0.5f*factor);
				
			}
			
			
			if(compteurTemps > 6)
			{
				achievList.RemoveAt(0);
				
				// on remet le compteur à 0
				compteurTemps = 0;
				
				// on reinitialise l'apparition du text
				textApparu = false;
				
				// on replace les texture cachées
				texture1.pixelInset = new Rect(100, -64, 256, 64);
				texture2.pixelInset = new Rect(100, -64, 256, 64);
			}
			
		}
	}
	
	
	public void addAchiev(string achiev)
	{

		this.achievList.Add(achiev);
		
	}
}
