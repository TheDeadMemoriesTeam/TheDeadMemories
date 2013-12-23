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
	
	private int xp = 200000;
	
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
	enum magicTypes{Fire=6, Ice=9, Wind=12};
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
		skillManager.setCriticPhysic(0f);
		skillManager.setDistancePhysicAttack(4f);
		skillManager.setDistanceMagicAttack(4f);
		
		timeRegen = 2;
		
		// Affecte la valeur du sprint de sprintAugmentation% de plus que la marche normale
		sprintSpeed = walkSpeed + sprintAugmentation*walkSpeed;
		sprintTimeStart = Time.time;
		isSprinting = false;
		updateSpeed(isSprinting);

		// Liste des descriptions des compétences
		List<string> skillsDescriptions = initSkillsDescriptions();

		//arbre de competence Survie
		skillManager.addSkill(new PassiveSkills("Survie", skillsDescriptions[0], 0, null, 200, 200, 5f, 5f, "pv+", "mana+", skillsDescriptions[1], skillsDescriptions[2]));
		skillManager.addSkill(new PassiveSkills("Resistance", skillsDescriptions[3], 0, skillManager.getSkill(0), 200, 200, 1f, 1f, "degPhysique-", "degMagic-", skillsDescriptions[4], skillsDescriptions[5])); 
		skillManager.addSkill(new InvincibleSkill("Invincible", skillsDescriptions[6], 3000, skillManager.getSkill(1), 0, 30, null, 5));

		//arbre de competence Attaque
		skillManager.addSkill(new PassiveSkills("Attaque de base", skillsDescriptions[7], 0, null, 200, 200, 5f, 5f, "degCac+", "degMag+", skillsDescriptions[8], skillsDescriptions[9]));
		skillManager.addSkill(new PassiveSkills("Critique", skillsDescriptions[10], 0, skillManager.getSkill(3), 200, 200, 1f, 1f, "criCac+", "cricMag+", skillsDescriptions[11], skillsDescriptions[12]));
		skillManager.addSkill(new FurieSkills("Furie", skillsDescriptions[13], 3000, skillManager.getSkill(4), 0, 30, null, 5f, 1.5f));

		//arbre de competence Feu
		skillManager.addSkill(new PorteeSkills("Boule de feu", skillsDescriptions[14], 1000, null, 0f, 10, fireball, 10f, 200, 200, "Degat+", "Portee+", skillsDescriptions[15], skillsDescriptions[16], 20f));
		skillManager.addSkill(new ZoneSkills("Lance flames", skillsDescriptions[17], 1000, skillManager.getSkill(6), 1f, 15, firezone, 15f, 200, 200, "Degat+", "Zone+", skillsDescriptions[18], skillsDescriptions[19], 10f));
		skillManager.addSkill(new SuperSkills("Meteor", skillsDescriptions[20], 3000, skillManager.getSkill(7), 2f, 20, null, 20f, 10f, 10f)); 
		
		//arbre de competence Glace
		skillManager.addSkill(new PorteeSkills("Glacon", skillsDescriptions[21], 1000, null, 0f, 10, iceball, 10f, 200, 200, "Degat+", "Portee+", skillsDescriptions[22], skillsDescriptions[23], 20f));
		skillManager.addSkill(new ZoneSkills("Iceberg", skillsDescriptions[24], 1000, skillManager.getSkill(9), 1f, 15, icezone, 15f, 200, 200, "Degat+", "Zone+", skillsDescriptions[25], skillsDescriptions[26], 10f));
		skillManager.addSkill(new SuperSkills("Ere glaciere", skillsDescriptions[27], 3000, skillManager.getSkill(10), 2f, 20, null, 20f, 10f, 10f));
		
		//arbre de competence Vent
		skillManager.addSkill(new PorteeSkills("Soufle", skillsDescriptions[28], 1000, null, 0f, 10, propulsion, 10f, 200, 200, "Degat+", "Portee+", skillsDescriptions[29], skillsDescriptions[30], 20f));
		skillManager.addSkill(new ZoneSkills("Bourasque", skillsDescriptions[31], 1000, skillManager.getSkill(12), 1f, 15, tornade, 15f, 200, 200, "Degat+", "Zone+", skillsDescriptions[32], skillsDescriptions[33], 10f));
		skillManager.addSkill(new SuperSkills("Tornade", skillsDescriptions[34], 3000, skillManager.getSkill(13), 2f, 20, null, 20f, 10f, 10f));

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
					Vector3 targetDir = targets[i].transform.position - transform.position;
					Vector3 playerDir = transform.forward;
					float angle = Vector3.Angle(targetDir, playerDir);
					if (angle>=-45 && angle<=45)
					{
						float damage = -skillManager.getPhysicAttack() + (-skillManager.getPhysicAttack()/100 * targets[i].getSkillManager().getPhysicalResistance());
						//gestion des critique
						//if
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
						porteeSkill.launch(transform.position, transform.forward, skillManager.getMagicAttack(), skillManager.getCriticPhysic());
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
									//gestion des critique
									//if
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
								zoneSkill.launch(transform.position);
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
		achievementManager.setPause(pause);
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

	List<string> initSkillsDescriptions()
	{
		List<string> list = new List<string>();
		list.Add("TODO1");
		list.Add("Augmente la quantité de points de vie maximale");
		list.Add("Augmente la quantité de points de mana maximale");
		list.Add("TODO4");
		list.Add("Augmente la résistance physique de Georges");
		list.Add("Augmente la résistance aux sorts de Georges");
		list.Add("Rend Georges invincible pendant une courte période");
		list.Add("TODO8");
		list.Add("Augmente les dégats physique infligés à chaque coups");
		list.Add("Augmente la puissance des sorts");
		list.Add("Permet d'effectuer des coup critique");
		list.Add("Augmente les dégats de coup critique physique");
		list.Add("Augmente les dégats de coup critique magique");
		list.Add("Laisser sortir la furie qui est en vous!");
		list.Add("Permet à Georges de lancer des boules de feu");
		list.Add("Augmente les dégats infligés par les boules de feu");
		list.Add("Augmente la portée des boules de feu");
		list.Add("Permet à Georges de lancer un sort de zone de feu");
		list.Add("Augmente les dégats du sort lance flamme");
		list.Add("Augmente la zone de feu du sort lance flamme");
		list.Add("Invoquez la puissance céleste des étoiles !");
		list.Add("Permet à George de lancer des morceaux de glace");
		list.Add("Augmente les dégats infligés par les glaçons");
		list.Add("Augmente la portée des glaçons");
		list.Add("Permet à Georges de geler les ennemis proches");
		list.Add("Augmente les dégats infligés par le sort iceberg");
		list.Add("Augmente la zone de glace du sort iceberg");
		list.Add("C'est Noel ! Enfin, pour vous");
		list.Add("Permet à Georges de repousser ces ennemis");
		list.Add("Augmente les dégats infligés par le souffle");
		list.Add("Augmente la portée du souffle");
		list.Add("Permet à Georges de repousser les ennemis qui l'entoure");
		list.Add("Augmente les dégats infligés par le sort Bourrasque");
		list.Add("Augmente la zone de souffle du sort Bourrasque");
		list.Add("Faite disparaitre vos ennemis dans les airs en un clin d'oeil !");

		return list;
	}
}
