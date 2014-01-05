using UnityEngine;
using System.Collections;

public class SuperSkillController : MonoBehaviour 
{
	public Transform particule;

	private float m_distance;
	private float m_damage;
	private bool m_furie;
	private float m_damageFurie;
	private float m_factorCritique;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		ParticleSystem particuleSystem = GetComponent<ParticleSystem>() as ParticleSystem;
		if(particuleSystem)
		{
			if(particuleSystem.isStopped)
			{
				Destroy(this.gameObject);
			}
			else if(particuleSystem.isPlaying)
			{
				//on inflige des degas au ennemis si il sont dans la zone 
				EnemyController[] targets = FindObjectsOfType(System.Type.GetType("EnemyController")) as EnemyController[];
				for (int i=0; i<targets.Length; i++)
				{
					Vector3 distance = transform.position-targets[i].transform.position;
					if(distance.magnitude <= m_distance)
					{
						float damage = -m_damage + (-m_damage)/100 * targets[i].getSkillManager().getMagicResistance();
						//gestion de la furie
						if(m_furie)
							damage += damage/100 * m_damageFurie;
						//gestion des critique
						if(m_factorCritique/100 < Random.value)
							damage *= 2;
						targets[i].healthUpdate(damage);
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(particule)
			GameObject.Instantiate(particule, transform.position, Quaternion.identity);

		//on inflige des degas au ennemis si il sont dans la zone 
		EnemyController[] targets = FindObjectsOfType(System.Type.GetType("EnemyController")) as EnemyController[];
		for (int i=0; i<targets.Length; i++)
		{
			Vector3 distance = transform.position-targets[i].transform.position;
			if(distance.magnitude <= m_distance)
			{
				float damage = -m_damage + (-m_damage)/100 * targets[i].getSkillManager().getMagicResistance();
				//gestion de la furie
				if(m_furie)
					damage += damage/100 * m_damageFurie;
				//gestion des critique
				if(m_factorCritique/100 < Random.value)
					damage *= 2;
				targets[i].healthUpdate(damage);
			}
		}

		Destroy(this.gameObject);
	}

	public void init(float distance, float damage, bool furie, float damageFurie, float factorCritique)
	{
		m_distance = distance;
		m_damage = damage;
		m_furie = furie;
		m_damageFurie = damageFurie;
		m_factorCritique = factorCritique;
	}
}
