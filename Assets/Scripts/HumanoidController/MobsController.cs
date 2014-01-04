using UnityEngine;
using System.Collections;

public class MobsController
{
	private float m_xpPlayer = 0;
	EnemyController[] enemis;

	public void incXp(int xp)
	{
		m_xpPlayer += xp;

		enemis = GameObject.FindObjectsOfType<EnemyController>() as EnemyController[];
		for(int i=0; i<enemis.Length; i++)
		{
			if(enemis[i].getXp() == 0)
			{
				enemis[i].setXp((int)(m_xpPlayer*(Random.value * 0.2+0.9)));
			}
			else
				enemis[i].setXp((int)(enemis[i].getXp() + (xp*(Random.value * 0.2+0.9))));
		}
	}

	public void upMob()
	{
		for(int i=0; i<enemis.Length; i++)
		{
			//code a faire
		}
	}
}
