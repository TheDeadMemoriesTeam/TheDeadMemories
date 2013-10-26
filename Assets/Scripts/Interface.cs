using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour {
	
	public PlayerController player;
	
	public GUIText healthText;
	public GUIText experienceText;
	public GUIText gameOverText;
	
	// Use this for initialization
	void Start () {
		experienceText.color = new Color(0,0,255);
		gameOverText.enabled=false;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		int hp = player.getHitPoints();
		int hpMax = player.getMaxHitPoints();
		
		if (hp <= 0)
		{
			healthText.color = new Color(255,0,0);
        	hp = 0;
			gameOverText.enabled=true;
		}
        else if (hp <= hpMax * 1/4)
		{
			healthText.color = new Color(255,0,0);
		}
		else if (hp <= hpMax * 3/4)
		{
			healthText.color = new Color(125,125,0);
		}
		else
		{
			healthText.color = new Color(0,255,0);
			
		}
		healthText.text = hp.ToString();
	
		experienceText.text = player.getExperience().ToString();
	}
}
