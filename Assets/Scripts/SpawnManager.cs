using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour 
{
	public DayNightCycleManager dayNightCycle;
	
	private SpawnController[] spawns;
	
	private float timeLastSpawn;
	private float timeStep = 7.0F;
	private float spawnDelay;
	
	private int _nbEnnemies = 0;// Should be modified only using the NbEnnemies property
	private int NbEnnemies {
		get {
            return _nbEnnemies;
        }
        set {
            _nbEnnemies = value;

			float timeS = timeStep;

			if (dayNightCycle != null) {
				float dayTime = dayNightCycle.dayTime - 0.7f; // Add an offset of -0.7 hour on the current time to have a better result.
				float coef = Mathf.Cos(dayTime/24f * 2*Mathf.PI);
				coef = Mathf.Sign(coef)*Mathf.Pow(Mathf.Sign(coef)*coef, 0.3f);
				coef = coef/2 + 0.5f; // To range [0, 1]
				coef += 0.9f; // To range [0.9, 1.9]
				timeS *= coef;
			}

			spawnDelay = timeS - timeS * 1/Mathf.Sqrt((_nbEnnemies+5F)/5F);
        }
	}
	
	public PlayerController player;
	
	// Use this for initialization
	void Start () 
	{
		spawns = FindObjectsOfType(System.Type.GetType("SpawnController")) as SpawnController[];
		
		// Start with 20 ennemies
		for (int i=0; i < 20; ++i)
			addEnnemy();
		
		timeLastSpawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Manage the pause state
		if (player.getPause())
			timeLastSpawn += Time.deltaTime;
		
		// Create an ennemy if the specified time is elapsed
		if (Time.time - timeLastSpawn >= spawnDelay)
		{
			timeLastSpawn += spawnDelay;
			addEnnemy();
		}
	}
	
	private void addEnnemy()
	{
		// Add an ennemy at a random spawn point.
		int P = Random.Range(0, spawns.Length);
		int i = P;
		do { // Add the ennemy at the first active spawn point
			SpawnController sp = spawns[i];
			if (sp.isActive())
			{
				sp.spawn();
				NbEnnemies++;
				break;
			}
			i = (i + 1) % spawns.Length;
		} while (i != P);
	}
	
	public void decNbEnnemies()
	{
		NbEnnemies--;
	}
}
