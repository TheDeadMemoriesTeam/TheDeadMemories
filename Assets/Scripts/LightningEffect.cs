using UnityEngine;
using System.Collections;

public class LightningEffect : MonoBehaviour {
	
	public bool thunder = true;
	public Light lightning;
	
	private float timeForRefresh;
	private float lastTime;
	private float lightningLenght = 0.3f;	// Durée d'un éclair
	private float thresh = 0.7f;	// Pourcentage d'éclair
	
	// Types d'orage
	private int velocity = 1; // 1 = petit | 2 = moyen | 3 = tempete
	
	public PlayerController player;
	
	// Use this for initialization
	void Start () {
		ResetTimeForRefresh();
	}
	
	// Update is called once per frame
	void Update () {
		// Déclenche des éclair que si l'on est hors pause et que l'orage est activé
		if (!thunder || player.getPause())
			return;
		
		if (timeForRefresh <= 0)
		{
			if (Random.value <= thresh)
			{
				lastTime = Time.time;
				lightning.intensity = Random.Range(0.4f, 3f);
				lightning.enabled = true;
			}
			else
				lightning.enabled = false;
			
			ResetTimeForRefresh();
		}
		else
		{
			if (lightning.enabled && (Time.time - lastTime > lightningLenght))
				lightning.enabled = !lightning.enabled;
		}
		timeForRefresh -= Time.deltaTime;
	}
	
	private void ResetTimeForRefresh()
	{
		if (velocity == 1)
			timeForRefresh = Random.Range(15, 19);
		else if (velocity == 15)
			timeForRefresh = Random.Range(10, 14);
		else
			timeForRefresh = Random.Range(5, 9);
	}
	
	public void setVelocity(int v)
	{
		velocity = v;
	}
	
	public void setThunder(bool b)
	{
		thunder = b;
	}
}
