using UnityEngine;
using System.Collections;

// Permet de gérer la vie
public class HumanoidController : MonoBehaviour 
{	
	//regen de mana
	private float regen = 0;
	protected float timeRegen;
	
	//manager des skills
	protected SkillManager skillManager;
	
	void Awake()
	{
		skillManager = GetComponent<SkillManager>();	
	}

	// Use this for initialization
	void Start () 
	{	
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (regen >= timeRegen)
		{
			manaUpdate(1);	
			regen = 0;
		}
		regen += Time.deltaTime;
	}
	
	public virtual void healthUpdate(int change)
	{
		skillManager.setPv(skillManager.getPv() + change);
		if (skillManager.getPv() > skillManager.getPvMax())
			skillManager.setPv(skillManager.getPvMax());
	}
	
	public int getHitPoints()
	{
		return skillManager.getPv();
	}
	
	public int getMaxHitPoints()
	{
		return skillManager.getPvMax();
	}
	
	public virtual void manaUpdate(int change)
	{
		skillManager.setMana(skillManager.getMana() + change);
		if (skillManager.getMana()>skillManager.getManaMax())
			skillManager.setMana(skillManager.getManaMax());
	}
	
	public int getMana()
	{
		return skillManager.getMana();	
	}
	
	public int getManaMax()
	{
		return skillManager.getManaMax();	
	}
}
