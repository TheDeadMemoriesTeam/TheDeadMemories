using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : HumanoidController 
{
	// Speeds
	public float walkSpeed = 6.0F;
	private float sprintSpeed;
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
	public float sprintAugmentation = 0.75f;	// Pourcentage d'accélération du joueur en sprint
	
    private Vector3 moveDirection = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	
	private CharacterController controller;
	
	private int xp = 20000;
	
	// Variables servants aux achievements
	public AchievementManager achievementManager;
	
	// Inventaire du joueur
	public Inventory inventory;

	// Particule magie
	public Transform fireball;
	public Transform firezone;
	public Transform iceball;
	public Transform icezone;
	public Transform propulsion;
	public Transform tornade;
	// Compteur de temps
	private float magicTime;
	// Type de magie
	enum magicTypes{Fire=5, Ice=8, Wind=11};
	magicTypes currentMagicType = magicTypes.Fire;
	
	private bool pause = false;

	// Animations
	private Animator anim;
	private PlayerHashIDs hash;

	// Manager de sauvegarde
	SaveManager saveManager;

	// Use this for initialization
	protected override void Start () 
	{
		controller = GetComponent<CharacterController>();
		skillManager.setBasePvMax(200f);
		skillManager.setBaseManaMax(100f);
		skillManager.setBasePhysicalResistance(0f);
		skillManager.setBaseMagicResistance(0f);
		skillManager.setBasePhysicAttack(1f);
		skillManager.setBaseMagicAttack(5f);
		skillManager.setDistancePhysicAttack(4f);
		skillManager.setDistanceMagicAttack(4f);
		
		timeRegen = 2;
		
		// Affecte la valeur du sprint de sprintAugmentation% de plus que la marche normale
		sprintSpeed = walkSpeed + sprintAugmentation*walkSpeed;
		sprintTimeStart = Time.time;
		isSprinting = false;
		updateSpeed(isSprinting);
		
		//arbre de competence Survie
		skillManager.addSkill(new PassiveSkills("Survie", 0, null, 200, 200, 5f, 5f, "pv+", "mana+"));
		skillManager.addSkill(new PassiveSkills("Resistance", 0, skillManager.getSkill(0), 200, 200, 1f, 1f, "degPhysique-", "degMagic-")); 
		skillManager.addSkill(new InvincibleSkill("Invincible", 3000, skillManager.getSkill(1), 0, 30, null, 5));

		//arbre de competence Attaque
		skillManager.addSkill(new PassiveSkills("Attaque de base", 0, null, 200, 200, 5f, 5f, "degCac+", "degMag+"));
		skillManager.addSkill(new FurieSkills("Furie", 3000, skillManager.getSkill(3), 0, 30, null, 5f, 1.5f));
		
		//arbre de competence Feu
		skillManager.addSkill(new PorteeSkills("Boule de feu", 1000, null, 0f, 10, fireball, 10f, 200, 200, "Degat+", "Portee+", 20f));
		skillManager.addSkill(new ZoneSkills("Lance flames", 1000, skillManager.getSkill(5), 1f, 15, firezone, 15f, 200, 200, "Degat+", "Zone+", 10f));
		skillManager.addSkill(new SuperSkills("Meteor", 3000, skillManager.getSkill(6), 2f, 20, null, 20f, 10f, 10f)); 
		
		//arbre de competence Glace
		skillManager.addSkill(new PorteeSkills("Glacon", 1000, null, 0f, 10, iceball, 10f, 200, 200, "Degat+", "Portee+", 20f));
		skillManager.addSkill(new ZoneSkills("Iceberg", 1000, skillManager.getSkill(8), 1f, 15, icezone, 15f, 200, 200, "Degat+", "Zone+", 10f));
		skillManager.addSkill(new SuperSkills("Ere glaciere", 3000, skillManager.getSkill(9), 2f, 20, null, 20f, 10f, 10f));
		
		//arbre de competence Vent
		skillManager.addSkill(new PorteeSkills("Soufle", 1000, null, 0f, 10, propulsion, 10f, 200, 200, "Degat+", "Portee+", 20f));
		skillManager.addSkill(new ZoneSkills("Bourasque", 1000, skillManager.getSkill(11), 1f, 15, tornade, 15f, 200, 200, "Degat+", "Zone+", 10f));
		skillManager.addSkill(new SuperSkills("Tornade", 3000, skillManager.getSkill(12), 2f, 20, null, 20f, 10f, 10f));

		// Animations
		hash = GetComponent<PlayerHashIDs>();
		anim = GetComponentInChildren<Animator>();
		anim.SetLayerWeight(0,1f);

		saveManager = new SaveManager(achievementManager, skillManager);
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
				anim.SetBool(hash.isJumping, false);
				// Moves forward, left, right, backward
	            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	            moveDirection = transform.TransformDirection(moveDirection);
	            moveDirection *= speed;
				// Handle jumps
	            if (Input.GetButtonDown("Jump"))
				{
					Vector3 startRay = new Vector3(transform.position.x,
					                               transform.position.y + 2,
					                               transform.position.z);

					RaycastHit rayToSkyHit;
					Ray rayToSky = new Ray(startRay, Vector3.up);


					if (Physics.Raycast(rayToSky, out rayToSkyHit, 1))
					{
						if (rayToSkyHit.collider.tag != "InvisibleCeiling")
							moveDirection.y = jumpSpeed;
					}
					else
						moveDirection.y = jumpSpeed;
					anim.SetBool(hash.isJumping, true);
				}
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
			achievementManager.setPlayerPos(transform.position);
	
			// Rotation
			rotation = new Vector3(0, Input.GetAxis("Rotation")+Input.GetAxis("Mouse X"), 0);
			rotation *= rotationFactor * Time.timeScale;
			transform.Rotate(rotation);
			
			mouseHandler();

			// Permet le changement de type de magie
			if (Input.GetKeyDown(KeyCode.F1))
				currentMagicType = magicTypes.Fire;
			else if (Input.GetKeyDown(KeyCode.F2))
				currentMagicType = magicTypes.Ice;
			else if (Input.GetKeyDown(KeyCode.F3))
				currentMagicType = magicTypes.Wind;
			if (isSprinting)
				Debug.Log("sprint !!");

			AnimationManager();

			// Test du saveManager
			if (Input.GetKeyDown(KeyCode.KeypadMultiply))
				saveManager.save();
			if (Input.GetKeyDown(KeyCode.KeypadDivide))
				saveManager.load();
		}
	}

	// Récupère les évènements souris et agis en fonction
	void mouseHandler()
	{
		anim.SetBool(hash.isHitting, false);
		anim.SetBool(hash.isSmallSummoning, false);
		// Si le joueur effectue une attaque physique
		if (Input.GetButtonDown("Fire1"))
		{
			anim.SetBool(hash.isHitting, true);
			EnemyController[] targets = FindObjectsOfType(System.Type.GetType("EnemyController")) as EnemyController[];
			for (int i=0; i<targets.Length; i++)
			{
				Vector3 distance = transform.position-targets[i].transform.position;
				if(distance.magnitude <= skillManager.getDistancePhysicAttack())
				{
					var targetDir = targets[i].transform.position - transform.position;
					var playerDir = transform.forward;
					var angle = Vector3.Angle(targetDir, playerDir);
					if (angle>=-45 && angle<=45)
					{
						float damage = -skillManager.getPhysicAttack() + (-skillManager.getPhysicAttack()/100 * targets[i].getSkillManager().getPhysicalResistance());
						targets[i].healthUpdate(damage);
					}
				}	
			}
		}
		// Si le joueur lance une attaque magique
		else if (Input.GetButtonUp("Fire2"))
		{
			float duration = Time.time - magicTime;
			anim.SetBool(hash.isSmallSummoning, true);

			//recuperation des skills celon le type selectionné
			PorteeSkills porteeSkill = skillManager.getSkill((int)currentMagicType) as PorteeSkills;
			ZoneSkills zoneSkill = skillManager.getSkill((int)currentMagicType + 1) as ZoneSkills;
			SuperSkills superSkill = skillManager.getSkill((int)currentMagicType + 2) as SuperSkills;

			if(porteeSkill.getIsBought())
			{
				//determination de la skill en fonction du temp de maintien du bouton droit
				//attaque de portee
				if(duration >= porteeSkill.getTimeIncantation() && duration < zoneSkill.getTimeIncantation())
				{
					//test si la mana du joeur est sufisante pour la skill choisi
					if(skillManager.getMana() > porteeSkill.getManaCost())
					{
						//decrementation de la mana du coup de la skill
						skillManager.setMana(skillManager.getMana() - porteeSkill.getManaCost());

						//execution de la skill
						porteeSkill.launch(transform.position, transform.forward, skillManager.getMagicAttack());
					}
				}
				//determination de la skill en fonction du temp de maintien du bouton droit
				//attaque de zone
				else if(zoneSkill.getIsBought())
				{
					if(duration >= zoneSkill.getTimeIncantation() && duration < superSkill.getTimeIncantation())
					{
						//test si la mana du joeur est sufisante pour la skill choisi
						if(skillManager.getMana() > zoneSkill.getManaCost())
						{
							//decrementation de la mana du coup de la skill
							skillManager.setMana(skillManager.getMana() - zoneSkill.getManaCost());

							//execution de la skill
							zoneSkill.launch(transform.position);

							//on inflige des degas au ennemis si il sont dans la zone 
							EnemyController[] targets = FindObjectsOfType(System.Type.GetType("EnemyController")) as EnemyController[];
							for (int i=0; i<targets.Length; i++)
							{
								Vector3 distance = transform.position-targets[i].transform.position;
								if(distance.magnitude <= skillManager.getDistanceMagicAttack() + zoneSkill.getAd())
								{
									float damage = -skillManager.getMagicAttack()+(porteeSkill.getDamage()*porteeSkill.getLvlDamage()) + (-skillManager.getMagicAttack()+(porteeSkill.getDamage()*porteeSkill.getLvlDamage())/100 * targets[i].getSkillManager().getMagicResistance());
									targets[i].healthUpdate(damage);
								}
							}
						}
					}
					//determination de la skill en fonction du temp de maintien du bouton droit
					//super attaque
					else if(superSkill.getIsBought())
					{
						if(duration >= superSkill.getTimeIncantation())
						{
							//test si la mana du joeur est sufisante pour la skill choisi
							if(skillManager.getMana() > superSkill.getManaCost())
							{
								//decrementation de la mana du coup de la skill
								skillManager.setMana(skillManager.getMana() - superSkill.getManaCost());

								//execution de la skill
								//zoneSkill.launch(transform.position);
							}
						}
					}
				}
			}
		}
		// Si le joueur commence à préparer une attaque magique
		else if (Input.GetButtonDown("Fire2"))
			magicTime = Time.time;
	}

	void AnimationManager()
	{
		if(Input.GetAxis("Horizontal") != 0f || 
		   Input.GetAxis("Vertical") != 0f)
		{
			anim.SetFloat(hash.speed, 5.5f);
		}
		else
			// Otherwise set the speed parameter to 0.
			anim.SetFloat(hash.speed, 0);
	}
	
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "ManaPotion"
		   || other.gameObject.tag == "Bone")
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
	}
	
	public void experienceUpdate(int change)
	{
		xp += change;
		if (change > 0)
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
