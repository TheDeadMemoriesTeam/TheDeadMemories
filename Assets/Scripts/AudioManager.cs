using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioClip Track1;
	public AudioClip Track2;
	public AudioClip Track3;
	public AudioClip Track4;
	public AudioClip Track5;
	public AudioClip Track6;
	public AudioClip Track7;
	public AudioClip Track8;
	public AudioClip Track9;
	private int randomInt;


	// Use this for initialization
	void Start () {
		randomInt = Random.Range(1,9)%1;
		switch(randomInt)
		{
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			break;
		case 5:
			break;
		case 6:
			break;
		case 7:
			break;
		case 8:
			break;
		case 9:
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
