using UnityEngine;
using System.Collections;

public class CryptController : MonoBehaviour 
{
	private PlayerController player;
	
	private EnemyController[] enmies;
	
	private int rangePlayer = 5;
	private int rangeEnemies = 20;

	// Use this for initialization
	void Start () 
	{
		player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
	}
	
	bool isInCrpit(int range, Transform ts)
	{
		return (ts.transform.position.x-range <= transform.position.x+range &&
				ts.transform.position.x+range >= transform.position.x-range &&
				ts.transform.position.z-range <= transform.position.z+range &&
				ts.transform.position.z+range >= transform.position.z-range);
	}
		
	// Update is called once per frame
	void Update () 
	{
		enmies = FindObjectsOfType(System.Type.GetType("EnemyController")) as EnemyController[];
		if(isInCrpit(rangePlayer, player.transform))
		{
			for(int i=0; i<enmies.Length; i++)
			{
				if(isInCrpit(rangeEnemies, enmies[i].transform))
					enmies[i].setInCrypts(true);	
				else
					enmies[i].setInCrypts(false);
			} 	
		}
		else
		{
			for(int i=0; i<enmies.Length; i++)
				enmies[i].setInCrypts(false);
		}
	}
}
