using UnityEngine;
using System.Collections;

public class PlayerHashIDs : MonoBehaviour {

	// Animations
	public int baseState;
	public int movingState;

	// Variables
	public int speed;
	public int isHitting;
	public int isDead;

	void Awake()
	{
		// Animation
		baseState = Animator.StringToHash("Base.Base");
		movingState = Animator.StringToHash("Base.Mouvement");

		// Variable
		speed = Animator.StringToHash("speed");
		isHitting = Animator.StringToHash("isHitting");
		isDead = Animator.StringToHash("isDead");
	}
}
