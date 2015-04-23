using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class TranslationLoader : MonoBehaviour {


	private const string ROOT_PATH = "XML/GUI_";
	private XmlNode languageInfos;

	// Use this for initialization
	void Start () {
		loadLanguage("fr");//fr-en-es  :les 3 codes de langue 

		loadMenuTranslation ();//juste pour la scene Menu + Levels
		loadButtonsTranslation ();// pour tous les boutons de la GUI, et autre élmts UI
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * Charge le bon monde !!
	 */
	public void loadLanguage(string codeLanguage){
				Debug.Log (this + " on charge la langue " + codeLanguage);
				TextAsset textAsset = (TextAsset)Resources.Load (ROOT_PATH + codeLanguage, typeof(TextAsset));
				XmlDocument xmldoc = new XmlDocument ();
				xmldoc.LoadXml (textAsset.text);//on charge le fichier
		
				XmlNodeList languagesList = xmldoc.GetElementsByTagName ("language"); // tableau des noeuds "level"

				languageInfos = languagesList [0];
				//Debug.Log (this + " on recup  " + languageInfos.InnerXml);

				
	}

	public void loadMenuTranslation(){

		XmlNodeList languageInfoscontent = languageInfos.ChildNodes;//récup de ses enfants !!
		foreach (XmlNode objectInfo in languageInfoscontent) {
			
			if(objectInfo.Attributes["type"].Value.Equals("GUILevelBlock")){
				string nameWorld = objectInfo.Attributes["titre"].Value;
				string descriptif = objectInfo.InnerText;
				Debug.Log (this + " on recup  "+ nameWorld +" : " + descriptif);
				
			}
			
			
			
		}
	}

	public void loadButtonsTranslation(){

		XmlNodeList languageInfoscontent = languageInfos.ChildNodes;//récup de ses enfants !!
		foreach (XmlNode objectInfo in languageInfoscontent) {
			
			if(objectInfo.Attributes["type"].Value.Equals("GUIOneShot")){
				string type = objectInfo.Attributes["type"].Value;
				string descriptif = objectInfo.InnerText;
				Debug.Log (this + " on recup  "+ type +" : " + descriptif);
				
			}
			
			
			
		}

	}
}
