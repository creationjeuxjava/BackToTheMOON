using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	//public static ObjectPooler current;
	public GameObject pooledObject;
	public int pooledAmount = 5;
	public bool willGrow = false;

	List<GameObject> pooledObjects;
	int altMin;
	int altMax;

	void Awake(){
		//current = this;
	
	}

	void Start () {

		pooledObjects = new List<GameObject> ();
		
		for (int i = 0; i < pooledAmount; i++) {				
			GameObject obj = (GameObject) Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}

	}

	public void setMinMaxAlt(int min, int max){
		altMin = min;
		altMax = max;
	}
	public Vector2 getMinMax(){

		return new Vector2(altMin,altMax);
	}
	
	public GameObject GetPooledObject(){
		for (int i = 0; i < pooledObjects.Count; i++) {			
			//est-il inactif ?
			if(!pooledObjects[i].activeInHierarchy){
				return pooledObjects[i];
			}
		}
		if (willGrow) {
				
			GameObject obj = (GameObject)Instantiate(pooledObject);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;
	}

}
