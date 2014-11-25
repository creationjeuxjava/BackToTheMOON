using UnityEngine;
using System.Collections;
//using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

//inspiré de http://unitynoobs.blogspot.fr/2011/02/xml-loading-data-from-xml-file.html

/*
 * Responsable du chargement des GameOject du niveau joué !!
 */
public class LoadLevelcontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//loadLevel (1);
	}

	/*
	 * Charge le bon monde !!
	 */
	public void loadLevel(int levelNum){

		TextAsset textAsset = (TextAsset)Resources.Load("XML/testXML", typeof(TextAsset));
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.LoadXml ( textAsset.text );//on charge le fichier
		
		XmlNodeList levelsList = xmldoc.GetElementsByTagName("level"); // tableau des noeuds "level"
		
		foreach (XmlNode levelInfo in levelsList) {

			if(int.Parse(levelInfo.Attributes["indice"].Value).Equals(levelNum)){

				XmlNodeList levelcontent = levelInfo.ChildNodes;//récup de ses enfants !!
				foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
				{
					
					Debug.Log("***************** on lit : "+levelsItens.Name);
					Debug.Log("Contenu du noeud : "+levelsItens.InnerText);
					
					if(levelsItens.Name == "object")
					{
						int number = int.Parse(levelsItens.Attributes["nombre"].Value);
						string name = levelsItens.Attributes["name"].Value;
						Debug.Log("Valeur de attribut name: "+name);
						Debug.Log("nbre à créer: "+number);
						
						XmlNodeList objectcontent = levelsItens.ChildNodes;//récup de ses enfants !!
						foreach (XmlNode child in objectcontent) 
						{														
							if(child.Name == "Altitude")
							{
								Debug.Log("--> MAX : "+child.Attributes["max"].Value);
								Debug.Log("--> MIN : "+child.Attributes["min"].Value);
								int max = int.Parse(child.Attributes["max"].Value);
								int min = int.Parse(child.Attributes["min"].Value);

								createSpritesWorld(number,new Vector2 (-15 ,15) ,new Vector2(min,max),name,levelNum);
								
							}
						}
						
					}
				}
			}

		}
	}

	/*** méthode de création d'éléments du World *****/
	private void createSpritesWorld(int number,Vector2 xRange,Vector2 yRange,string nomPrefab,int numLevel){

		for(int i = 0 ; i < number ; i++){
			Object prefab = Resources.Load<Object>("Prefabs/world"+numLevel+"/"+nomPrefab);		
			Vector3 pos = new Vector3 (Random.Range(xRange.x,xRange.y),Random.Range(yRange.x,yRange.y),-4.6f);
			Quaternion spawnRotation =  Quaternion.identity;;//Quaternion.Euler(0,0, Random.Range(0, 360) ); //Quaternion.identity;
			GameObject clone = Instantiate(prefab, pos, spawnRotation) as GameObject;

			this.GetComponent<GameController>().addGameObjectInWorld(clone);
			//Vector3 spawnPosition = new Vector3 (Random.Range(xRange.x,xRange.y),Random.Range(yRange.x,yRange.y),-4.6f);
			//GameObject sprite = Instantiate(objectToInstantiate, spawnPosition, spawnRotation) as GameObject;

			
		}
		
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
