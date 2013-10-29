using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour 
{
	public PeonController prefab;
	private PlayerController player;
	
	private int range;
	
	// Use this for initialization
	void Start () 
	{
		player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		
		range = 20;
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
		Instantiate(prefab, position, Random.rotation);
	}
}