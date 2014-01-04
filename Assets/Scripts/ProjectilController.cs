using UnityEngine;
using System.Collections;

public class ProjectilController : MonoBehaviour 
{

	private float m_speed;
	private float m_distance;

	private float m_damage;

	private Vector3 m_origin;
	private Vector3 direction;

	// Use this for initialization
	void Start () 
	{
		m_origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(transform.position.x + direction.x * m_speed * Time.deltaTime,
		                                 transform.position.y,
		                                 transform.position.z + direction.z * m_speed * Time.deltaTime);
		Vector3 newDistance = transform.position-m_origin;
		if (newDistance.x>m_distance || newDistance.y>m_distance || newDistance.z>m_distance)
			Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			HumanoidController enemy = other.gameObject.GetComponent<HumanoidController>() as HumanoidController;
			float damage = -m_damage + (-m_damage/100 * enemy.getSkillManager().getMagicResistance());
			enemy.healthUpdate(damage);
			Destroy(gameObject);
		}
		else if(other.gameObject.tag == "Player")
		{
			PlayerController player = other.gameObject.GetComponent<PlayerController>() as PlayerController;
			float damage = -m_damage + (-m_damage/100 * player.getSkillManager().getMagicResistance());
			player.healthUpdate(damage);
			Destroy(gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	public void init(float speed, float distance, float damage, Vector3 forward)
	{
		m_speed = speed;
		m_distance = distance;
		m_damage = damage;
		direction = forward;
	}
}
