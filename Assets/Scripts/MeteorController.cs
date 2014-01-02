using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour 
{
	public Transform particule;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter(Collision col)
	{
		GameObject.Instantiate(particule, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}
}
