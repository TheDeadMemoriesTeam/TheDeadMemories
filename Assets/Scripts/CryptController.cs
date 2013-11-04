using UnityEngine;
using System.Collections;

public class CryptController : MonoBehaviour 
{
	private PlayerController player;
	
	private int range = 5;

	// Use this for initialization
	void Start () 
	{
		player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
	}
	
	bool isInCrpit()
	{
		return (player.transform.position.x-range <= transform.position.x+range &&
				player.transform.position.x+range >= transform.position.x-range &&
				player.transform.position.z-range <= transform.position.z+range &&
				player.transform.position.z+range >= transform.position.z-range);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isInCrpit())
		{
			player.setInCrypt(true);
		}
		else
		{
			player.setInCrypt(false);	
		}
	}
}
