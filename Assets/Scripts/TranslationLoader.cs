using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class TranslationLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		loadLanguage("fr");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * Charge le bon monde !!
	 */
	public void loadLanguage(string codeLanguage){
				Debug.Log (this + " on charge la langue " + codeLanguage);
				TextAsset textAsset = (TextAsset)Resources.Load ("XML/GUI_" + codeLanguage, typeof(TextAsset));
				XmlDocument xmldoc = new XmlDocument ();
				xmldoc.LoadXml (textAsset.text);//on charge le fichier
		
				XmlNodeList languagesList = xmldoc.GetElementsByTagName ("language"); // tableau des noeuds "level"

				XmlNode languageInfos = languagesList [0];
				//Debug.Log (this + " on recup  " + languageInfos.InnerXml);

				XmlNodeList languageInfoscontent = languageInfos.ChildNodes;//récup de ses enfants !!
				foreach (XmlNode objectInfo in languageInfoscontent) {
		
					//float gravity = float.Parse(objectInfo.Attributes["gravity"].Value);
					string nameWorld = objectInfo.Attributes["titre"].Value;
					string descriptif = objectInfo.InnerText;
					Debug.Log (this + " on recup  "+ nameWorld +" : " + descriptif);
		
		
				}
	}
}
