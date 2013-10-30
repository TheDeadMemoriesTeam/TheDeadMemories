using UnityEngine;
using System.Collections;

public class AchivementManager : MonoBehaviour {
	
	// Texture achivement
	public Texture texture; 
	
	// Son achivement
	public AudioClip soundAchivement; 
	
	// Booléens des achivements
	private bool firstMove = false;
	private bool firstKill = false;
	private bool tenKills = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void FirstMoveAchievement()
	{
		if (!firstMove)
		{
			Debug.Log("Achivement First Move !");
			unlockAchivement(texture);
			firstMove = !firstMove;
		}
	}
	
	public void oneKillAchievement()
	{
		if (!firstKill)
		{
			Debug.Log("Achivement First Blood !");
			unlockAchivement(texture);
			firstKill = !firstKill;
		}
	}
	
	public void tenKillsAchievement()
	{
		if (!tenKills)
		{
			Debug.Log("Achivement Ten kills !");
			unlockAchivement(texture);
			tenKills = !tenKills;
		}
	}
	
	void unlockAchivement(Texture textureAchivement)
	{
		/*GameObject displayAchivement = new GameObject ("Achivement Object");
		displayAchivement.transform.position = new Vector3(0, 0, 0);
		GUITexture textureToApply = new GUITexture();
		textureToApply = (GUITexture)displayAchivement.AddComponent(System.Type.GetType("GUITexture"));
		textureToApply.texture = textureAchivement;
		/*textureToApply = displayAchivement.AddComponent(System.Type.GetType("GUITexture"));
		textureToApply.texture = textureAchivement;
		textureToApply.pixelInset.width = textureAchivement.width;
		textureToApply.pixelInset.height = textureAchivement.height;
		textureToApply.pixelInset.x = textureAchivement.width;
		textureToApply.pixelInset.y = textureAchivement.height;*/
		
		/*displayAchivement.AddComponent(System.Type.GetType("AchivementShow"));*/
		
		audio.PlayOneShot(soundAchivement);
	}
}