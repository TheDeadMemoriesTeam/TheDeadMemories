using UnityEngine;
using System.Collections;

public class MobsController
{
	private float m_xpPlayer = 20000000;
	EnemyController[] enemis;

	public void incXp(int xp)
	{
		m_xpPlayer += xp;

		enemis = GameObject.FindObjectsOfType<EnemyController>() as EnemyController[];
		for(int i=0; i<enemis.Length; i++)
		{
			if(enemis[i].getXpE() == 0)
			{
				enemis[i].setXpE((int)(m_xpPlayer*(Random.value * 0.2+0.9)));
			}
			else
				enemis[i].setXpE((int)(enemis[i].getXpE() + (xp*(Random.value * 0.2+0.9))));
		}
	}

	public void upMob()
	{
		incXp(0);
		bool tropCher;
		for(int i=0; i<enemis.Length; i++)
		{
			tropCher = false;
			while(!tropCher)
			{
				int j = Random.Range(0, 4);
				//skill magique
				if(j == 0)
				{
					BaseSkills skill = enemis[i].getSkillManager().getSkill(j) as BaseSkills;
					j = Random.Range(0, 2);
					//damage
					if(j == 0)
					{
						if(enemis[i].getXpE() >= skill.getCostIncDamage())
						{
							enemis[i].setXpE(enemis[i].getXpE() - skill.getCostIncDamage());
							skill.setLvlDamage(skill.getLvlDamage()+1);
							enemis[i].incXp(skill.getCostIncDamage());
						}
						else
							tropCher = true;
					}
					//portee
					else
					{
						if(enemis[i].getXpE() >= skill.getCostIncAd())
						{
							enemis[i].setXpE(enemis[i].getXpE() - skill.getCostIncAd());
							skill.setLvlAd(skill.getLvlAd()+1);
							enemis[i].incXp(skill.getCostIncAd());
						}
						else
							tropCher = true;
					}
				}
				//les autres qui son passivent
				else
				{
					PassiveSkills skill = enemis[i].getSkillManager().getSkill(j) as PassiveSkills;
					j = Random.Range(0, 2);
					//premier ad
					if(j == 0)
					{
						if(enemis[i].getXpE() >= skill.getCostIncFirstAd())
						{
							enemis[i].setXpE(enemis[i].getXpE() - skill.getCostIncFirstAd());
							skill.setLvlFirstAd(skill.getLvlFirstAd()+1);
							enemis[i].incXp(skill.getCostIncFirstAd());
						}
						else
							tropCher = true;
					}
					//second ad
					else
					{
						if(enemis[i].getXpE() >= skill.getCostIncSecAd())
						{
							enemis[i].setXpE(enemis[i].getXpE() - skill.getCostIncSecAd());
							skill.setLvlSecAd(skill.getLvlSecAd()+1);
							enemis[i].incXp(skill.getCostIncSecAd());
						}
						else
							tropCher = true;
					}
				}
			}
			enemis[i].getSkillManager().updateSkill();
			enemis[i].getSkillManager().setPv(enemis[i].getSkillManager().getPvMax());
		}
	}
}
