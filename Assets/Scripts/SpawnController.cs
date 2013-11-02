using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour 
{
	public EnemyController meleePrefab;
	public EnemyController distancePrefab;
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
	public void spawn()
	{
		int randX = Random.Range(-range, range);
		int randZ = Random.Range(-range, range);
		Vector3 position = new Vector3 (transform.position.x-randX, 0, transform.position.z-randZ);
		if(Random.Range(1,3)%2==0)
			Instantiate(distancePrefab, position, Quaternion.identity);
		else
			Instantiate(meleePrefab, position, Quaternion.identity);
	}
}