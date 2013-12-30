using UnityEngine;
using System.Collections;

public class SoundHitManager : MonoBehaviour {

	//Sound Hit
	public AudioClip[] soundCoups;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playHitSound()
	{
		if(soundCoups.Length != 0)
			audioSource.PlayOneShot(soundCoups[(int)Random.Range(0, soundCoups.Length-1)]);
	}
}
