using UnityEngine;
using System.Collections;

public abstract class Skills 
{
	private string name;
	private int price;
	private bool isBought = false;
	private bool isUnlock = false;
	
	private Skills father = null;
	
	//acsessor
	public void setName(string str)
	{
		name = str;	
	}
	
	public string getName()
	{
		return name;	
	}
	
	public void setPrice(int p)
	{
		price = p;	
	}
	
	public int getPrice()
	{
		return price;	
	}
	
	public void setIsBought(bool b)
	{
		isBought = b;	
	}
	
	public bool getIsBought()
	{
		return isBought;	
	}
	
	public void setIsUnlock(bool b)
	{
		isUnlock = b;	
	}
	
	public bool getIsUnlock()
	{
		return isUnlock;	
	}
	
	public void setFather(Skills ptr)
	{
		father = ptr;	
	}
	
	public Skills getFather()
	{
		return father;
	}
	
	// Use this for initialization
	protected virtual void Start () 
	{
		
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
	
	}
}
