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
	
	private int xp=0;
	
	// Variables servants aux achievements
	public AchivementManager achivementManager;
	private int cptEnemyKilled = 0;
	private float timeNotTouched = 0;
	private float timeSurvived = 0;
	private bool assassin = true;
	private float travel = 0;
	
	private Hashtable inv;
	
	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
		pvMax = 200;
		pv = pvMax;
		manaMax = 100;
		mana = manaMax;
		
		inv = new Hashtable(2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pv <= 0) {
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
				achivementManager.FirstMoveAchievement();
        }
		// Applies move
        moveDirection.y -= gravity * Time.deltaTime;
		Vector3 vec = transform.position;
        controller.Move(moveDirection * Time.deltaTime);
		updateTravel(vec, transform.position);

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
				if(distance.magnitude <= 4f)
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
				if(distance.magnitude <= 4f)
				{
					var targetDir = targets[i].transform.position - transform.position;
					var playerDir = transform.forward;
					var angle = Vector3.Angle(targetDir, playerDir);
					if (angle>=-45 && angle<=45)
						targets[i].healthUpdate(-5);
				}	
			}
		}
		
		timedAchievements();
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
		else if (other.gameObject.tag == "Weapon")
		{
			other.gameObject.SetActive(false);
			healthUpdate(-50);
			experienceUpdate(40);
		}
	}
	
	public void experienceUpdate(int change)
	{
		xp += change;
		
		killsAchievements();
	}
	
	public int getExperience()
	{
		return xp;
	}
	
	public void setTimeNotTouched(float time)
	{
		timeNotTouched = time;
		assassin = false;
	}
	
	private void updateTravel(Vector3 fromWhere, Vector3 to)
	{
		travel += Vector3.Distance(fromWhere, to);
		
		if (travel >= 1000)		// 1 km parcourut
			achivementManager.sundayWalkerAchievement();
		if (travel >= 10000)	// 10 km parcourut
			achivementManager.dailyJoggingAchievement();
		if (travel >= 42195)	// 42,195 km parcourut
			achivementManager.marathonAchievement();
		if (travel >= 100000)	// 100 km parcourut
			achivementManager.healthWalkAchievement();
		if (travel >= 1000000)	// 1.000 km parcourut
			achivementManager.athleticAchievement();
		if (travel >= 1000000000)// 1.000.000 km parcourut
			achivementManager.dopedAddictAchievement();
	}
	
	private void timedAchievements()
	{
		timeNotTouched += Time.deltaTime;
		timeSurvived += Time.deltaTime;
		// Débloque les achievements non touché pendant x temps
		if (timeNotTouched >= 60)	// 1 min
			achivementManager.uncatchableAchievement();
		if (timeNotTouched >= 300)	// 5 mins
			achivementManager.reallyUncatchableAchievement();
		
		// Débloque les achievements survivre x temps
		if (timeSurvived >= 60)	// 1 min
			achivementManager.surviveOneMinuteAchievement();
		if (timeSurvived >= 1200)	// 20 mins
			achivementManager.surviveTwentyMinutesAchievement();
		if (timeSurvived >= 3600)	// 1 h
			achivementManager.surviveOneHourAchievement();
		if (timeSurvived >= 14400)	// 4 h
			achivementManager.surviveFourHoursAchievement();
		if (timeSurvived >= 43200)	// 12 h
			achivementManager.surviveTwelveHoursAchievement();
	}
	
	private void killsAchievements()
	{
		// Compteur d'ennemis tués, débloque les achievements avec un certain nombre
		cptEnemyKilled++;
		// Achievements de la série tuer x ennemis
		if (cptEnemyKilled == 1)
			achivementManager.firstBloodAchievement();
		else if (cptEnemyKilled == 10)
			achivementManager.littleKillerAchievement();
		else if (cptEnemyKilled == 100)
			achivementManager.killerAchievement();
		else if (cptEnemyKilled == 1000)
			achivementManager.serialKillerAchievement();
		
		// Achievements de la série assassin
		if (assassin)
		{
			if (cptEnemyKilled == 5)
				achivementManager.assassinAchievement();
			else if (cptEnemyKilled == 50)
				achivementManager.masterAssassinAchievement();
		}
	}

	public Hashtable getInv()
	{
		return inv;	
	}
}
