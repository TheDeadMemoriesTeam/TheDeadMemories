using UnityEngine;
using System.Collections;

public class EnemyHashIDs : MonoBehaviour {
		
	// Variables
	public int isHitting;
	public int isWalking;
	
	void Start()
	{
		// Variable
		isHitting = Animator.StringToHash("isHitting");
		isWalking = Animator.StringToHash("isWalking");
	}
}
