using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//using UnityEditor;

/*
 * * gère la logique du jeu et la création du niveau par le biais de LoadLevelController, mais aussi d'objets
 *    de pooling !!
 * */




public class GameController : MonoBehaviour {

	public GameObject player;

	public static bool hasSaved ; //A t'il au moins joué une fois ? 

	public static GameObject world;//conteneur du world
	private static bool isWorldMoving = false;
	private static bool isGameInPause = false;
	private static bool isOverGUIPause = false;
	public static bool isInGame,isGameOver;
	public static bool isInSpace;
	private GameObject backLayer, middleLayer, foreLayer,backBackLayer;

	public static float altitude = 0;

	public  static int nbrePieces = 0;
	public static int nbreDiamond = 0;
	public static int score = 0;
	public const int DIAMOND_FOR_PHOENIX_EFFECT = 5;
	public const int PHOENIX_EFFECT_COST = 5;

	private int coeffAltitude = 100;
	private float altMaxForWinLevel = 150000f;//80000f ou 150000f ;
	private float altBeginOfSpace = 41000f; //10000f; test   //41000f;  realité

	private int coeffVitesse = 1 * 3600; 
	public static int currentLevel = 1; //par défaut le premier niveau
	private float gravityLevel;
	public static Vector3 lastPlayerSpeed,playerSpeed;
	public static float vitessePlayerTransormee;

	private const float TIME_TO_GAME_OVER = 3f;
	public static float timeLeftToGameOver = TIME_TO_GAME_OVER;

	private Dictionary<GameObject,string> dicoObjectPoolers = new Dictionary<GameObject,string>();

	// création initiale
	void Start () {
        Debug.Log(File.Exists(Application.persistentDataPath + "/playerInfo.bttm"));
		//Application.targetFrameRate = 40;
        if (File.Exists(Application.persistentDataPath + "/playerInfo.bttm"))
        {
            GameController.Load();
        }
        else Debug.Log("File doesn't exist or cant reache the file");
		
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		resetGame ();
		//Debug.Log (this.name + " méthode strat");
	}
	
	// Update is called once per frame
	void Update () {

		/** a-t-on besoin d'activer des GO depuis les ObjectPooler ****/
		foreach (KeyValuePair<GameObject, string> entry in dicoObjectPoolers)
		{
			string layerName = entry.Value; 

			//Vector2 minMax = listeObjectPoolers[i].GetComponent<ObjectPooler>().getMinMax();
			Vector2 minMax = entry.Key.GetComponent<ObjectPooler>().getMinMax();
			//Debug.Log("Position du ForeLayer : "+foreLayer.transform.position.y*(-1)+" et min "+minMax.x+" et max "+minMax.y +" pour GO "+listeObjectPoolers[i].name);
			//if(foreLayer.transform.position.y * (-1) > minMax.x && foreLayer.transform.position.y * (-1 )< minMax.y){

			GameObject obj = null;
			if(altitude > minMax.x && altitude < minMax.y){
				obj = entry.Key.GetComponent<ObjectPooler>().GetPooledObject();
				if(obj != null){
					//Debug.Log(listeObjectPoolers[i].name +" crée !");
					
					//obj.transform.position = new Vector3(Random.Range(-3,3),Random.Range(20,200),-4.6f);
					obj.transform.position = new Vector3(Random.Range(-3,3),Random.Range(0,100),-4.6f);
					obj.SetActive(true);
					if(layerName == "foreGround")
						obj.transform.parent = foreLayer.transform;
					else if(layerName == "middleLayer")
						obj.transform.parent = middleLayer.transform;
					else if(layerName == "backGround")
						obj.transform.parent = backLayer.transform;
					else if(layerName == "backBackGround")
						obj.transform.parent = backBackLayer.transform;
				}
				
			}
		}


		if (isWorldMoving == true && !isGameInPause && isInGame) {

			if(!isInSpace){
				playerSpeed = PlayerController.vitesse * Time.smoothDeltaTime * 50;
				lastPlayerSpeed = new Vector3(playerSpeed.x,playerSpeed.y,playerSpeed.z);

				//Debug.Log("Vitesse globale avt control: "+playerSpeed.y);
				//controlMaxVitessePlayer();
			}
			else{
				/*playerSpeed.x = PlayerController.vitesse.x * Time.deltaTime * 50 ;
				playerSpeed.y = PlayerController.vitesse.y * Time.deltaTime * 50;
				playerSpeed.z = PlayerController.vitesse.z * Time.deltaTime * 50;*/
				playerSpeed = PlayerController.vitesse * Time.smoothDeltaTime * 50;

			}
			//Debug.Log(this+"Time delta : "+Time.deltaTime);


			//chaque partie avance à une vitesse différente == parallax
			backBackLayer.transform.Translate (playerSpeed / 5f);
			backLayer.transform.Translate (playerSpeed / 4f);
			middleLayer.transform.Translate (playerSpeed / 3f);
			foreLayer.transform.Translate (playerSpeed/2f);

			//calcul de l'altitude
			altitude = foreLayer.transform.position.y * -1 * coeffAltitude;
			//vitessePlayerTransormee = PlayerController.vitesse.y*-1 * coeffVitesse;
			//Debug.Log("ISINSpace :"+isInSpace +" Altitude :"+altitude+"Vitesse Player : "+vitesse+" km/h et nbre pièces : "+nbrePieces);
			//Debug.Log (this+" vitesse joueur "+playerSpeed.y+" et isInGame : "+isInGame+" et isOverGUI "+isOverGUI());
			//Debug.Log ("***************"+this+" vitesse joueur transformée"+vitessePlayerTransormee);


			if(playerSpeed.y >= 0){
				checkTimeToGameOverLeft();
			}
			else{
				timeLeftToGameOver = TIME_TO_GAME_OVER;
			}
			if(altitude >= altMaxForWinLevel){
				isInGame = false;
				currentLevel++;
				GameController.Save();
				currentLevel--;
			}
			if(altitude >= altBeginOfSpace && !isInSpace) { 
				isInSpace = true;
				PlayerController.setVitesseEnterInSpace(lastPlayerSpeed.y);
			}
			//ActionButtonManager.updateIcon();
		}
	}


	/*** vérifie s'il reste du temps avant de lancer le gameOVER ...utile qd le perso redescend ****/
	private void checkTimeToGameOverLeft(){
		if(!audio.isPlaying && !isGameOver)audio.Play ();
		timeLeftToGameOver -= Time.deltaTime;
		if(timeLeftToGameOver < 0)
		{
			isInGame = false;
			isGameOver = true;
			GameController.Save();
			audio.Stop();
			//Debug.Log (this.name + " on aperdu !!!!!!!!!!!!!!!!!!!!!!!!!!");
		}
	}
	/** méthode d'appel por la création d'un niveau *****/
	void createWorld(){

		Debug.Log ("on charge le niveau :" + currentLevel);
		this.GetComponent<LoadLevelcontroller> ().loadLevel (currentLevel);
	}

	public void addGameObjectInWorld(GameObject obj,string layerName){

		if(layerName == "foreGround")
			obj.transform.parent = foreLayer.transform;
		else 
			obj.transform.parent = middleLayer.transform;


		/*if(obj.name == "nuage(clone)")
			obj.transform.parent = middleLayer.transform;
		else obj.transform.parent = foreLayer.transform;*/
	}
	public void addObjectPoolerInWorld(GameObject obj,string layerName){
		//listeObjectPoolers.Add (obj);
		dicoObjectPoolers.Add (obj, layerName);
	}

	public void setLevelGravity(float gravity){
		gravityLevel = gravity;	
		//Debug.Log("Gravité du Level  : "+ gravityLevel);
	}

	/*** méthode de remise à zéro de toutes les variables pour pouvoir rejouer sur une meme session ****/
	// à ce stade le nbre de pièces n'est pas remis à jour
	public void resetGame(){
		//altMaxForWinLevel = 8000f * coeffAltitude;
		//altBeginOfSpace = 50f*coeffAltitude;//4000f à remettre

		if (hasSaved) {
			GameController.Load ();
				}
		isWorldMoving = false;
		isGameInPause = false;
		isOverGUIPause = false;
		isInGame = true;
		isGameOver = false;
		isInSpace = false;

		altitude = 0;
		world = GameObject.FindGameObjectWithTag ("World");
		backLayer = GameObject.FindGameObjectWithTag ("BackLayer");
		middleLayer = GameObject.FindGameObjectWithTag ("MiddleLayer");
		foreLayer = GameObject.FindGameObjectWithTag ("ForeLayer");
		backBackLayer = GameObject.FindGameObjectWithTag ("BackBackLayer");

		playerSpeed = new Vector3(0,0,0);

		timeLeftToGameOver = TIME_TO_GAME_OVER;
		createWorld ();
	
	}



	//lancer le joueur et donc faire avancer le monde !!
	public void LaunchPlayer (){
		player.GetComponent <PlayerController>().launchIntheAir (gravityLevel);
		isWorldMoving = true;
	}

	public void makePhoenixEffect(){
		
		isInGame = true;
		isGameOver = false;
		isOverGUIPause = false;
		timeLeftToGameOver = TIME_TO_GAME_OVER;
		nbreDiamond -= PHOENIX_EFFECT_COST;
		PlayerController.setVitesseEnterInSpace(-0.3f);
		this.GetComponent<SoundsManagerController> ().playSound ("phoenix", 10f);

		GameObject.Find ("Player").GetComponent<PlayerController> ().setInvicibility ();
	}

	public static int getDiamondForPhoenix(){
		return DIAMOND_FOR_PHOENIX_EFFECT;
	} 
	public static void addPiece(){
		nbrePieces++;
		GameController.addScore (10);
		//Debug.Log("au ajoute une pièce  "+ nbrePieces);
	}

	public static void addDiamond(){
		nbreDiamond++;
		//Debug.Log("au ajoute une pièce  "+ nbrePieces);
	}

	public static void addScore(int value){
		score += value;
		//Debug.Log("au ajoute une pièce  "+ nbrePieces);
	}

	//mettre le jeu en pause ou le remettre
	public static void tooglePauseGame(){
		isGameInPause = !isGameInPause;
	}

	//permet de savoir si a cliqué sur un élément GUI
	public void OverGUI(bool value){		
		isOverGUIPause = value;
		//Debug.Log("on est sur un element UI ? "+isOverGUIPause);
	}


	public static bool isGamePaused(){return isGameInPause;}
	public static bool isOverGUI(){return isOverGUIPause;}

	void Awake() {
		/*if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize ("22375");
		} else {
			Debug.Log("Platform not supported");
		}*/
	}
/*	void OnGUI() {
				if (!isInGame) {
						//if (GUI.Button (new Rect (140, 40, 150, 50), Advertisement.isReady () ? "Montrer ADS" : "En chargement ADS...")) {
								// Show with default zone, pause engine and print result to debug log
				/*Advertisement.Show (null, new ShowOptions {
				pause = true,
				resultCallback = result => {
					Debug.Log(result.ToString());
				}
			});*/
						//}
		//		}
		//}







	public  static void  Save(){

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.bttm");
		
		//Ajout des choses a serializer 
		PlayerData playerData = new PlayerData (); 
		playerData.coins = nbrePieces;
		playerData.diamonds = nbreDiamond;
		playerData.doneLevels [currentLevel+1] = true;
		//playerData.hasSavedGame = hasSaved;
		bf.Serialize (file, playerData);
		Debug.Log ("Saving game into " + Application.persistentDataPath);
		file.Close();
		hasSaved = true;
		}



	public static void Load(){
		if(File.Exists(Application.persistentDataPath + "/playerInfo.bttm"));
		{
						BinaryFormatter bf = new BinaryFormatter ();
						FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.bttm", FileMode.Open);
						PlayerData playerData = (PlayerData) bf.Deserialize(file);
						nbrePieces = playerData.coins;
						nbreDiamond = playerData.diamonds;
						//hasSaved = playerData.hasSavedGame;
						file.Close ();
						Debug.Log ("Loading game from " + Application.persistentDataPath);
			   			
		}
	}





}
