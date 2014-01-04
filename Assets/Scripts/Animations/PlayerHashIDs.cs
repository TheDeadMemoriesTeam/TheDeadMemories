using UnityEngine;
using System.Collections;

public class PlayerHashIDs : MonoBehaviour {

	// Animations
	public int baseState;
	public int beginMovingState;
	public int movingState;
	public int endMovingState;

	// Variables
	public int speed;
	public int isHitting;
	public int isSmallSummoning;
	public int isJumping;
	public int isDead;
	public int isSprinting;

	void Start()
	{
		// Animation
		baseState = Animator.StringToHash("Base.Base");
		beginMovingState = Animator.StringToHash("Base.MouvementBegin");
		movingState = Animator.StringToHash("Base.Mouvement");
		endMovingState = Animator.StringToHash("Base.MouvementEnd");

		// Variable
		speed = Animator.StringToHash("speed");
		isHitting = Animator.StringToHash("isHitting");
		isSmallSummoning = Animator.StringToHash("isSmallSummoning");
		isJumping = Animator.StringToHash("isJumping");
		isDead = Animator.StringToHash("isDead");
		isSprinting = Animator.StringToHash("isSprinting");
	}
}
