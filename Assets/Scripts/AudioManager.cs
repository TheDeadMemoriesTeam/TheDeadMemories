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
	public AudioClip Track10;
	public AudioClip Track11;

	private AudioClip curTrack;
	private float trackLength;

	private AudioSource audioSource;
	private bool isPlay;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		changeTrack();
		trackLength = 100;
		isPlay = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying && isPlay == true)
		{
			changeTrack();
		}
	}

	public void changeTrack()
	{
		if(audioSource.isPlaying)
		{
			audioSource.Stop();
		}
		int randomInt = Random.Range(1,11);
		switch(randomInt)
		{
		case 1:
			curTrack = Track1;
			break;
		case 2:
			curTrack = Track2;
			break;
		case 3:
			curTrack = Track3;
			break;
		case 4:
			curTrack = Track4;
			break;
		case 5:
			curTrack = Track5;
			break;
		case 6:
			curTrack = Track6;
			break;
		case 7:
			curTrack = Track7;
			break;
		case 8:
			curTrack = Track8;
			break;
		case 9:
			curTrack = Track9;
			break;
		case 10:
			curTrack = Track10;
			break;
		case 11:
			curTrack = Track11;
			break;
		}
		audioSource.clip = curTrack;
		audioSource.Play ();
	}

	public void stopTrack()
	{
		audioSource.Stop ();
	}

	public void changePlayState()
	{
		isPlay = !isPlay;
	}

	public bool getPlayState()
	{
		return isPlay;
	}
}
