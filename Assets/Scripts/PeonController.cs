using UnityEngine;
using System.Collections;

public class PeonController : EnemyController {

	// Use this for initialization
	void Start () {
		target = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
		gameObject.renderer.material.color = new Color(0.725F, 0.478F, 0.341F);
		agent = GetComponent<NavMeshAgent>();
		
		pvMax = 5;
		pv = pvMax;
	}
	
	protected override void Update ()
	{
		base.Update();
	}
}
