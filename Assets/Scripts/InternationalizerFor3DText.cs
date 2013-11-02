using UnityEngine;
using System.Collections;

public class InternationalizerFor3DText : MonoBehaviour {
	
	public string i18nNamespace;
	
	// Use this for initialization
	void Start () {
		// Set the localized text the item menu.
		TextMesh tm = GetComponent<TextMesh>();
		string textKey = i18nNamespace + '.' + tm.text.Replace(" ", "");
		tm.text = LanguageManager.Instance.GetTextValue(textKey);
		
		// Recreate a BoxCollider of the correct size
		// Be careful: The whole configuration of the previous BoxCollider is lost
		DestroyImmediate(GetComponent<BoxCollider>());
		gameObject.AddComponent<BoxCollider>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
