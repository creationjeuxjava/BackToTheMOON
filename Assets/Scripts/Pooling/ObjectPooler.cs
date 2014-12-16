using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	//public static ObjectPooler current;
	public GameObject pooledObject;
	public int pooledAmount = 1;
	public bool willGrow = false;
	//public float timeBetweenEach

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
			PooledObject poolObj; // declare la reference
			//myScript = thisGameObject.AddComponent( typeof ( PooledObject ) ) as PooledObject;
			//Or using Generics ( requires using System.Collections.Generics )
			poolObj = obj.AddComponent<PooledObject>();

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
		/*if (willGrow) {
				
			GameObject obj = (GameObject)Instantiate(pooledObject);
			pooledObjects.Add(obj);
			return obj;
		}*/

		return null;
	}

}

class PooledObject :MonoBehaviour{

	bool isPoolObject = true;
	public bool isPooledObject(){
		return isPoolObject;
	}
}
