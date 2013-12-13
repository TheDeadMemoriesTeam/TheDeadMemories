using UnityEngine;
using System.Collections;

public class PlayerHashIDs : MonoBehaviour {

	// Animations
	public int baseState;
	public int movingState;

	// Variables
	public int speed;
	public int isHitting;
	public int isJumping;
	public int isDead;

	void Awake()
	{
		// Animation
		baseState = Animator.StringToHash("Base.Base");
		movingState = Animator.StringToHash("Base.Mouvement");

		// Variable
		speed = Animator.StringToHash("speed");
		isHitting = Animator.StringToHash("isHitting");
		isJumping = Animator.StringToHash("isJumping");
		isDead = Animator.StringToHash("isDead");
	}
}
