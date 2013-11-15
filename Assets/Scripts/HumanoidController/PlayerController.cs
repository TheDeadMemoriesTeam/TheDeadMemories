using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : HumanoidController 
{
	public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
	public float rotationFactor = 5.0F;
    public float gravity = 20.0F;
	
    private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	
	private CharacterController controller;
	
	private int xp = 0;
	
	// Variables servants aux achievements
	public AchievementManager achievementManager;
	
	// Inventaire du joueur
	public Inventory inventory;
	
	private bool pause = false;
	
	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
		pvMax = 200;
		pv = pvMax;
		manaMax = 100;
		mana = manaMax;
		distanceP = 4f;
		distanceM = 4f;
		
		timeRegen = 2;
		
		//inv = new Dictionary<item, int>();
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		if(!pause)
		{
			base.Update();
			if (pv <= 0) 
			{
				return;
			}
			
	        if (controller.isGrounded) 
			{
				// Moves forward, left, right, backward
	            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	            moveDirection = transform.TransformDirection(moveDirection);
	            moveDirection *= speed;
				// Handle jumps
	            if (Input.GetButton("Jump"))
	                moveDirection.y = jumpSpeed;
	        }
			// Applies move
	        moveDirection.y -= gravity * Time.deltaTime;
			Vector3 vec = transform.position;
	        controller.Move(moveDirection * Time.deltaTime);
			achievementManager.updateTravel(vec, transform.position);
	
			// Rotation
			rotation = new Vector3(0, Input.GetAxis("Rotation")+Input.GetAxis("Mouse X"), 0);
			rotation *= rotationFactor * Time.timeScale;
			transform.Rotate(rotation);
			
			if (Input.GetButtonDown("Fire1"))
			{
				EnemyController[] targets = FindObjectsOfType(System.Type.GetType("EnemyController")) as EnemyController[];
				for (int i=0; i<targets.Length; i++)
				{
					Vector3 distance = transform.position-targets[i].transform.position;
					if(distance.magnitude <= distanceP)
					{
						var targetDir = targets[i].transform.position - transform.position;
						var playerDir = transform.forward;
						var angle = Vector3.Angle(targetDir, playerDir);
						if (angle>=-45 && angle<=45)
							targets[i].healthUpdate(-1);
					}	
				}
			}
			else if (Input.GetButtonDown("Fire2") && getMana()>=10)
			{
				manaUpdate(-10);
				EnemyController[] targets = FindObjectsOfType(System.Type.GetType("EnemyController")) as EnemyController[];
				for (int i=0; i<targets.Length; i++)
				{
					Vector3 distance = transform.position-targets[i].transform.position;
					if(distance.magnitude <= distanceM)
					{
						var targetDir = targets[i].transform.position - transform.position;
						var playerDir = transform.forward;
						var angle = Vector3.Angle(targetDir, playerDir);
						if (angle>=-45 && angle<=45)
							targets[i].healthUpdate(-5);
					}	
				}
			}
		}
	}
	
	
	
	void OnTriggerEnter (Collider other)
	{
		try 
		{
			inventory.addItem(other.gameObject.tag);
			DestroyObject(other.gameObject);
		}
		catch (System.InvalidOperationException ex)
		{
			Debug.Log(ex);	
		}
	}
	
	public void experienceUpdate(int change)
	{
		xp += change;
		
		achievementManager.updateKills();
	}
	
	public int getExperience()
	{
		return xp;
	}
	
	public void onPause()
	{
		pause = !pause;
	}
	
	public bool getPause()
	{
		return pause;
	}
}
