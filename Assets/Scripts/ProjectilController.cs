using UnityEngine;
using System.Collections;

public class ProjectilController : MonoBehaviour {

	private float m_speed;
	private float m_distance;

	private int m_damage;

	private Vector3 m_origin;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		m_origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x + direction.x * m_speed * Time.deltaTime,
		                                  1.5f,
		                                 transform.position.z + direction.z * m_speed * Time.deltaTime);
		Vector3 newDistance = transform.position-m_origin;
		if (newDistance.x>m_distance || newDistance.y>m_distance || newDistance.z>m_distance)
			Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		//if(other.gameObject.tag == "Enemy")
		if(other.gameObject.tag == "Enemy")
		{
			HumanoidController enemy = other.gameObject.GetComponent<HumanoidController>() as HumanoidController;
			enemy.healthUpdate(m_damage);
			Destroy(gameObject);
		}
	}

	public void init(float speed, float distance, int damage, Vector3 forward)
	{
		m_speed = speed;
		m_distance = distance;
		m_damage = damage;
		direction = forward;
	}
}
