using UnityEngine;
using System.Collections;
using System;
using System.Xml;
using System.IO;

//inspiré de http://unitynoobs.blogspot.fr/2011/02/xml-loading-data-from-xml-file.html

/*
 * Responsable du chargement des GO du niveau joué !!
 */
public class LoadLevelcontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TextAsset textAsset = (TextAsset)Resources.Load("XML/testXML", typeof(TextAsset));
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.LoadXml ( textAsset.text );//on charge le fichier

		XmlNodeList levelsList = xmldoc.GetElementsByTagName("level"); // tableau des noeuds "level"

		foreach (XmlNode levelInfo in levelsList) {
				
			XmlNodeList levelcontent = levelInfo.ChildNodes;//récup de ses enfants !!
			foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
			{

				Debug.Log("***************** on lit : "+levelsItens.Name);
				Debug.Log("Contenu du noeud : "+levelsItens.InnerText);

				if(levelsItens.Name == "object")
				{
					/*switch(levelsItens.Attributes["name"].Value)
					{*/
						/*case "Cube": obj.Add("Cube",levelsItens.InnerText);break; // put this in the dictionary.
						case "Cylinder":obj.Add("Cylinder",levelsItens.InnerText); break; // put this in the dictionary.
						case "Capsule":obj.Add("Capsule",levelsItens.InnerText); break; // put this in the dictionary.
						case "Sphere": obj.Add("Sphere",levelsItens.InnerText);break; // put this in the dictionary.
						*/
						Debug.Log("Valeur de attribut name: "+levelsItens.Attributes["name"].Value);
					//}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
