using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : HumanoidController 
{
	// Speeds
	public float walkSpeed = 6.0F;
	public float sprintSpeed;
    public float jumpSpeed = 8.0F;
	private float speed = 6.0F;		// CurrentSpeed
	
	// Others move
	public float rotationFactor = 5.0F;
    public float gravity = 20.0F;
	
	// Gestion Sprint
	private bool isSprinting;
	private float sprintTimeStart;
	public float maxTimeSprinting = 10f; // temps de sprint maximum
	private float pauseAfterSprint;
	private float sprintAugmentation = 0.75f;	// Pourcentage d'accélération du joueur en sprint
	
    private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	
	private CharacterController controller;
	
	private int xp = 0;
	
	// Variables servants aux achievements
	public AchievementManager achievementManager;
	
	// Inventaire du joueur
	public Inventory inventory;

	// Particule magie
	public Transform fireball;
	
	private bool pause = false;

	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
		skillManager.setBasePvMax(200);
		skillManager.setPv(skillManager.getPvMax());
		skillManager.setBaseManaMax(100);
		skillManager.setMana(skillManager.getManaMax());
		skillManager.setDistanceP(4f);
		skillManager.setDistanceM(4f);
		
		timeRegen = 2;
		
		// Affecte la valeur du sprint de sprintAugmentation% de plus que la marche normale
		sprintSpeed = walkSpeed + sprintAugmentation*walkSpeed;
		sprintTimeStart = Time.time;
		isSprinting = false;
		updateSpeed(isSprinting);
		
		//arbre de competence Survie
		skillManager.addSkill(new SurvieSkills("Survie", 0, null, 200, 200, 5, 5));
		skillManager.addSkill(new ResistanceSkills("Resistance", 0, skillManager.getSkill(0), 200, 200, 5, 5)); 
		skillManager.addSkill(new InvincibleSkill("Invincible", 3000, skillManager.getSkill(1), 0, 30, 5));
		
		//arbre de competence Attaque
		skillManager.addSkill(new BaseAttaqueSkills("Attaque de base", 0, null, 200, 200, 5, 5));
		skillManager.addSkill(new CounterAttaqueSkills("Contre attaque", 0, skillManager.getSkill(3), 200, 200, 5, 0.1f));
		skillManager.addSkill(new FurieSkills("Furie", 3000, skillManager.getSkill(4), 0, 30, 5f, 1.5f));
		
		//arbre de competence Feu
		skillManager.addSkill(new PorteeSkills("Boule de feu", 1000, null, 1f, 10, 10, 200, 200, 2f));
		skillManager.addSkill(new ZoneSkills("Lance flames", 1000, skillManager.getSkill(6), 2f, 15, 15, 200, 200, 2f));
		skillManager.addSkill(new SuperSkills("Meteor", 3000, skillManager.getSkill(7), 3f, 20, 20, 10f, 10f)); 
		
		//arbre de competence Glace
		skillManager.addSkill(new PorteeSkills("Glacon", 1000, null, 1f, 10, 10, 200, 200, 2f));
		skillManager.addSkill(new ZoneSkills("Iceberg", 1000, skillManager.getSkill(9), 2f, 15, 15, 200, 200, 2f));
		skillManager.addSkill(new SuperSkills("Ere glaciere", 3000, skillManager.getSkill(10), 3f, 20, 20, 10f, 10f));
		
		//arbre de competence Vent
		skillManager.addSkill(new PorteeSkills("Soufle", 1000, null, 1f, 10, 10, 200, 200, 2f));
		skillManager.addSkill(new ZoneSkills("Bourasque", 1000, skillManager.getSkill(12), 2f, 15, 15, 200, 200, 2f));
		skillManager.addSkill(new SuperSkills("Tornade", 3000, skillManager.getSkill(13), 3f, 20, 20, 10f, 10f));
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		if(!pause)
		{
			base.Update();
			if (skillManager.getPv() <= 0) 
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
				if (Input.GetKeyDown(KeyCode.LeftShift) && pauseAfterSprint <= 0)
				{
					isSprinting = true;
					updateSpeed(isSprinting);				
				}
				if ((isSprinting && Time.time > sprintTimeStart + maxTimeSprinting) || (Input.GetKeyUp(KeyCode.LeftShift)))
				{
					isSprinting = false;
					updateSpeed(isSprinting);
				}
				updateStandOffTime();
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
					if(distance.magnitude <= skillManager.getDistanceP())
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
				// Création et initialisation du projectil
				Transform projectileTransform = (Transform)Instantiate(fireball,
												            new Vector3(transform.position.x + transform.forward.x, 
												            			transform.position.y + 1.5f + transform.forward.y, 
												            			transform.position.z + transform.forward.z),
												            Quaternion.identity);
				ProjectilController projectile = projectileTransform.GetComponent<ProjectilController>() as ProjectilController;
				projectile.init(10f, 20f, 200, transform.forward);
				EnemyController[] targets = FindObjectsOfType(System.Type.GetType("EnemyController")) as EnemyController[];
				for (int i=0; i<targets.Length; i++)
				{
					Vector3 distance = transform.position-targets[i].transform.position;
					if(distance.magnitude <= skillManager.getDistanceM())
					{
						var targetDir = targets[i].transform.position - transform.position;
						var playerDir = transform.forward;
						var angle = Vector3.Angle(targetDir, playerDir);
						if (angle>=-45 && angle<=45)
							targets[i].healthUpdate(-5);
					}	
				}
			}
			if (isSprinting)
				Debug.Log("sprint !!");
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
	
	void updateSpeed(bool isSprinting)
	{
		// Met à jour la vitesse du joueur selon son status (sprint ou non)
		if (isSprinting)
		{
			sprintTimeStart = Time.time;
			speed = sprintSpeed;
		}
		else
			speed = walkSpeed;
	}
	
	void updateStandOffTime()
	{
		// Met à jour le temps pendant lequel le joueur ne peut pas sprinter
		if (isSprinting)
			pauseAfterSprint = (Time.time - sprintTimeStart) * 1.5f;
		else
			pauseAfterSprint -= Time.deltaTime;
	}
}
