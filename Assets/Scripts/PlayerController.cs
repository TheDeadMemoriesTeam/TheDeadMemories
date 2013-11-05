using UnityEngine;
using System.Collections;

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
	public AchivementManager achivementManager;
	
	private Hashtable inv;
	
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
		
		inv = new Hashtable(2);
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
				
				// Débloque l'achivement premier pas
				if (moveDirection != Vector3.zero)
					achivementManager.firstMoveAchievement();
	        }
			// Applies move
	        moveDirection.y -= gravity * Time.deltaTime;
			Vector3 vec = transform.position;
	        controller.Move(moveDirection * Time.deltaTime);
			achivementManager.updateTravel(vec, transform.position);
	
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
			
			achivementManager.timedAchievements();
		}
	}
	
	
	
	void OnTriggerEnter (Collider other)
	{
		// Collects items
		if (other.gameObject.tag == "Bone")
		{
			if(!inv.ContainsKey("Bone"))
				inv.Add ("Bone",1);
			else
				inv["Bone"] = (int)inv["Bone"]+1;
			DestroyObject(other.gameObject);
			return;
		}
		else if (other.gameObject.tag == "ManaPotion")
		{
			if(!inv.ContainsKey("ManaPotion"))
				inv.Add ("ManaPotion",1);
			else
				inv["ManaPotion"] = (int)inv["ManaPotion"]+1;
			DestroyObject(other.gameObject);
			return;
		}
	}
	
	public void experienceUpdate(int change)
	{
		xp += change;
		
		achivementManager.killsAchievements(change);
	}
	
	public int getExperience()
	{
		return xp;
	}

	public Hashtable getInv()
	{
		return inv;	
	}
	
	public void onPause()
	{
		pause = !pause;
	}
}
