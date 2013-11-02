using UnityEngine;
using System.Collections;

public class MenuItemControl : MonoBehaviour {
	
	public bool isQuitButton=false;
	
	void OnMouseEnter() {
		renderer.material.color = Color.red;
	}
	
	void OnMouseExit() {
		renderer.material.color = Color.white;
	}
	
	void OnMouseUp()
    {
       if(isQuitButton)
       {
       		Application.Quit();  
       }
       else
       {
       		 Application.LoadLevel(1);
 
       }
    }
}

