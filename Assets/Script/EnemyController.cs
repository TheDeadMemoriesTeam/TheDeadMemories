using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	private Transform target;
	
	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<NavMeshAgent>().destination = target.transform.position;
	}
}