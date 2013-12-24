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

	// Son de Noel
	public AudioClip NoelTrack;
	private AudioClip lastPlayingTrack;

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
	void Update () 
	{
		// EASTER EGG NOEL
		if (Input.GetKeyUp(KeyCode.N))
		{
			if (System.DateTime.Now.Month == 12
			    && (System.DateTime.Now.Day >= 24 && System.DateTime.Now.Day <= 26))
			{
				//if (!audio.isPlaying)
				//audio.Stop();

				if (/*audio.isPlaying && */audio.clip != NoelTrack)
				{
					lastPlayingTrack = audio.clip;

					audio.Stop();
					audio.clip = NoelTrack;
					audio.Play();
				}
				else if (audio.clip == NoelTrack) 
				{
					audio.Stop();
					audio.clip = lastPlayingTrack;
					audio.Play();
				}
				//audio.
				//audio.PlayOneShot(NoelTrack);

			}
		}
	}
}
