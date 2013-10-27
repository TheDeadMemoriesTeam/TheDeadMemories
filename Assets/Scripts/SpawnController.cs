using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour 
{
	public static int nbEnemies = 0;
	private int nbEnemiesMax;
	
	public PeonController prefab;
	private PlayerController player;
	
	private float time;
	private int pas;
	private bool isActive;
	private float pasTime;
	
	// Use this for initialization
	void Start () 
	{
		nbEnemiesMax = 20;
		
		player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		
		time = Time.time;
		pas = 20;
		isActive = true;
		pasTime = 1.0F;
 	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time-time>pasTime)
		{	
			time = Time.time;
			
			if(SpawnController.nbEnemies >= nbEnemiesMax)
				return;
			
			//desactivation du spawner par le player
			if(player.transform.position.x-pas <= transform.position.x+pas &&
				player.transform.position.x+pas >= transform.position.x-pas &&
				player.transform.position.z-pas <= transform.position.z+pas &&
				player.transform.position.z+pas >= transform.position.z-pas)
				isActive = false;
			else
				isActive = true;
			
			//rayon d'action du spawner
			if(isActive)
			{
				int rand = Random.Range(-pas, pas);
				Vector3 position = new Vector3 (transform.position.x-rand, 0, transform.position.z-rand);
				Instantiate( prefab, position, Random.rotation);
				SpawnController.nbEnemies++;
			}
		}
	}
}