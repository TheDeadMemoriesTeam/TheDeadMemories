using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour 
{
	public EnemyController meleePrefab;
	public EnemyController distancePrefab;
	public EnemyController bossPrefab;
	private PlayerController player;
	
	private int range = 15;
	
	// Use this for initialization
	void Start () 
	{
		player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
 	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	// The Spawn Point won't be active if the player is around
	public bool isActive()
	{
		return !(player.transform.position.x-range <= transform.position.x+range &&
				player.transform.position.x+range >= transform.position.x-range &&
				player.transform.position.z-range <= transform.position.z+range &&
				player.transform.position.z+range >= transform.position.z-range);
	}
	
	// Create an ennemy
	public EnemyController spawn()
	{
		int randX = Random.Range(-range, range);
		int randZ = Random.Range(-range, range);
		Vector3 position = new Vector3 (transform.position.x-randX, transform.position.y, transform.position.z-randZ);
		// Determine la hauteur de spawn
		RaycastHit raycastHit;
		Physics.Raycast(position, Vector3.down, out raycastHit);
		position.y = position.y - raycastHit.distance + 1;
		
		float proba = Random.value;
		EnemyController enemy;
		if (proba <= 0.02)
			enemy = Instantiate(bossPrefab, position, Quaternion.identity) as EnemyController;
		else if (proba <= 0.37)
			enemy = Instantiate(distancePrefab, position, Quaternion.identity) as EnemyController;
		else
			enemy = Instantiate(meleePrefab, position, Quaternion.identity) as EnemyController;
		enemy.transform.parent = transform;
		return enemy;
	}
}