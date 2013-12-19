using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour
{
	public float maximumDamage = 12f;					// The maximum potential damage per shot.
	public float minimumDamage = 7f;					// The minimum potential damage per shot.
	
	public float maximumTimeDelay = 1.5f;		        // The maximum time between shots.
	public float minimumTimeDelay = 0.8f;		        // The minimum time between shots.
	
	private EnemyController enemyController;            // Reference to the EnemyController script.
	private PlayerController player;                    // Reference to the player.
	//private bool shooting;							// A bool to say whether or not the enemy is currently shooting.
	private float scaledDamage;							// Amount of damage that is scaled by the distance from the player.
	private float scaledTimeDelay;						// maximumTimeDelay - minimumTimeDelay
	private float timeDelay;
	private float timeCount;

	void Awake ()
	{
		// The scaledDamage is the difference between the maximum and the minimum damage.
		scaledDamage = maximumDamage - minimumDamage;

		scaledTimeDelay = maximumTimeDelay - minimumTimeDelay;
		timeCount = 0;
		timeDelay = 0;
	}

	// Use this for initialization
	void Start () {
		enemyController = GetComponent<EnemyController>();
		player = (PlayerController)FindObjectOfType(System.Type.GetType("PlayerController"));
	}
	
	// Update is called once per frame
	void Update () {
		timeCount += Time.deltaTime;

		if (enemyController.isShooting())
			Shoot();
	}

	void Shoot ()
	{
		float d = Vector3.Distance(transform.position, player.transform.position);

		if (timeCount > timeDelay)
		{
			float manaMax = enemyController.getSkillManager().getManaMax();
			float mana = enemyController.getSkillManager().getMana();
			float manaAttackProbability = Mathf.Sqrt(mana/manaMax)*0.8f;

			if (Random.value < manaAttackProbability && enemyController.getSkillManager().getMana() >= -enemyController.getManaCost()) // Mana attack
			{
				// The fractional distance from the player, 1 is next to the player, 0.5 is at the max shooting distance.
				float fractionalDistance = (enemyController.shootDistance * 2 - d) / enemyController.shootDistance;
				// The damage is the scaled damage, scaled by the fractional distance, plus the minimum damage.
				// float damage = -damageAttack + (-damageAttack/100 * target.getSkillManager().getPhysicalResistance());
				float damage = scaledDamage * fractionalDistance + minimumDamage;
				player.healthUpdate(-damage);
				enemyController.manaUpdate(enemyController.getManaCost());
			}
			else // Melee attack
			{
				// The fractional distance from the player, 1 is next to the player, 0.5 is at the max shooting distance.
				float fractionalDistance = (enemyController.shootDistance * 2 - d) / enemyController.shootDistance;
				// The damage is the scaled damage, scaled by the fractional distance, plus the minimum damage.
				// float damage = -damageAttack + (-damageAttack/100 * target.getSkillManager().getPhysicalResistance());
				float damage = scaledDamage * fractionalDistance + minimumDamage;
				player.healthUpdate(-damage);
			}

			player.achievementManager.updateTimeNotTouched(0);
			timeDelay = Random.value*scaledTimeDelay + minimumTimeDelay;
			timeCount = 0;
		}
	}
}
