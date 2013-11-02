using UnityEngine;
using System.Collections;

public class InternationalizerFor3DText : MonoBehaviour {
	
	public string i18nNamespace;
	
	// Use this for initialization
	void Start () {
		TextMesh tm = GetComponent<TextMesh>();
		string textKey = i18nNamespace + '.' + tm.text.Replace(" ", "");
		tm.text = LanguageManager.Instance.GetTextValue(textKey);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
