using UnityEngine;
using System.Collections;

public class roueMoulin : MonoBehaviour 
{
	private float m_vitesse;
	// Use this for initialization
	void Start () 
	{
		m_vitesse = Random.Range(15, 20) * PlayerPrefs.GetFloat("windSpeed", 0.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(new Vector3(0, m_vitesse, 0) * Time.deltaTime);
	}
}
