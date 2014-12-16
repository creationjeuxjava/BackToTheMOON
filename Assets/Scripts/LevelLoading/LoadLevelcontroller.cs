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


	/*
	 * Charge le bon monde !!
	 */
	public void loadLevel(int levelNum){

		TextAsset textAsset = (TextAsset)Resources.Load("XML/testXML", typeof(TextAsset));
		XmlDocument xmldoc = new XmlDocument ();
		xmldoc.LoadXml ( textAsset.text );//on charge le fichier
		
		XmlNodeList levelsList = xmldoc.GetElementsByTagName("level"); // tableau des noeuds "level"
		
		foreach (XmlNode levelInfo in levelsList) {

			//est-ce le level désiré ??
			if(int.Parse(levelInfo.Attributes["indice"].Value).Equals(levelNum)){
				//récupération de la gravité du level !!
				float gravity = float.Parse(levelInfo.Attributes["gravity"].Value);

				XmlNodeList levelcontent = levelInfo.ChildNodes;//récup de ses enfants !!
				foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
				{
					
					//Debug.Log("***************** on lit : "+levelsItens.Name);
					//Debug.Log("Contenu du noeud : "+levelsItens.InnerText);


					if(levelsItens.Name == "object")
					{
						int number = int.Parse(levelsItens.Attributes["nombre"].Value);
						string name = levelsItens.Attributes["name"].Value;
						string isPoolNeeded = levelsItens.Attributes["pool"].Value;
						bool needPooling;

						if(isPoolNeeded == "true")	needPooling = true;//Convert.ToBoolean();
						else needPooling = false;
						//Debug.Log("Valeur de attribut name: "+name);
						//Debug.Log("nbre à créer: "+number);
						
						XmlNodeList objectcontent = levelsItens.ChildNodes;//récup de ses enfants !!
						foreach (XmlNode child in objectcontent) 
						{														
							if(child.Name == "Altitude")
							{
								//Debug.Log("--> MAX : "+child.Attributes["max"].Value);
								//Debug.Log("--> MIN : "+child.Attributes["min"].Value);
								int max = int.Parse(child.Attributes["max"].Value);
								int min = int.Parse(child.Attributes["min"].Value);

								if(!needPooling)
									createSpritesWorld(number,new Vector2 (-15 ,15) ,new Vector2(min,max),name,levelNum);
								else{
									Object prefab = Resources.Load<Object>("Prefabs/"+name);
									Object pooler = Resources.Load<Object>("Prefabs/ObjectPooler");

									Vector3 pos = new Vector3 (Random.Range(-15,15),Random.Range(min,max),-4.6f);
									Quaternion rotation =  Quaternion.identity;
									GameObject poolerObject = Instantiate(pooler, pos, rotation) as GameObject;
									poolerObject.name = "poolerObjectFor"+name;
									poolerObject.GetComponent<ObjectPooler>().pooledObject = (GameObject)prefab;
									poolerObject.GetComponent<ObjectPooler>().setMinMaxAlt(min,max);
									this.GetComponent<GameController>().addObjectPoolerInWorld(poolerObject);
								}
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
			Object prefab = Resources.Load<Object>("Prefabs/"+nomPrefab);		
			Vector3 pos = new Vector3 (Random.Range(xRange.x,xRange.y),Random.Range(yRange.x,yRange.y),-4.6f);
			Quaternion spawnRotation =  Quaternion.identity;
			GameObject clone = Instantiate(prefab, pos, spawnRotation) as GameObject;

			this.GetComponent<GameController>().addGameObjectInWorld(clone);
		}
		
	}

	public void spawnAsteroid(GameObject obj){
		Vector3 posToSpawn = obj.transform.position;
		obj.SetActive (false);
		for(int i = 0 ; i < 5 ; i++){
			Object prefab = Resources.Load<Object>("Prefabs/World1/miniAsteroid");		
			Vector3 pos = new Vector3 (Random.Range(posToSpawn.x+1,posToSpawn.x-1),
			                           	Random.Range(posToSpawn.y+1,posToSpawn.y-1),-4.6f);
			Quaternion spawnRotation =  Quaternion.identity;
			GameObject clone = Instantiate(prefab, pos, spawnRotation) as GameObject;
			
			this.GetComponent<GameController>().addGameObjectInWorld(clone);

		}


	}

}
