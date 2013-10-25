using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	private Transform target;
	
	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		gameObject.renderer.material.color = new Color(0.725F, 0.478F, 0.341F);
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<NavMeshAgent>().destination = target.transform.position;
	}
}