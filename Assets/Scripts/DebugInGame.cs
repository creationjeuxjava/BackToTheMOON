using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugInGame : MonoBehaviour {

	private int startY;
	Text isFly,isCask,isShoe,translation,vitesse,fpsText, isGameFinish,OSInfos;

	/*** pour le FPS ****/
	public float updateInterval = 0.5F;	
	private float accum = 0; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	private string format;//valeur à afficher


	/*** pour draw calls ****/
	//private int drawcalls = 0;
	
	//GameObject[] allObjects;// = new GameObject[]();
	//List<GameObject> allObjects = new List<GameObject>();

	void Start () {
		OSInfos = GameObject.Find ("operating system").GetComponent<Text> ();
		OSInfos.text = "OS : " + SystemInfo.operatingSystem+" \n GraphicsMemory :"+SystemInfo.graphicsMemorySize+"\n systemMemory :"+SystemInfo.systemMemorySize
			+"\n support processor: "+SystemInfo.processorType
				+"\n maxText: "+SystemInfo.maxTextureSize+" Device :"+SystemInfo.deviceName
				+"\n deviceModel: "+SystemInfo.deviceModel+" DeviceType :"+SystemInfo.deviceType;
		/*SystemInfo.maxTextureSize;
		SystemInfo.deviceName;
		SystemInfo.deviceModel;*/
		//print(SystemInfo.graphicsMemorySize);
		/*print(SystemInfo.deviceModel);*/
		//GameObject.f
		//allObjects = GameObject.FindObjectsOfType (GameObject);

		startY = 80;
		isFly = GameObject.Find ("isFlyBegin").GetComponent<Text> ();
		isCask = GameObject.Find ("isWithCask").GetComponent<Text> ();
		isShoe = GameObject.Find ("isWithShoe").GetComponent<Text> ();
		translation = GameObject.Find ("translation").GetComponent<Text> ();
		vitesse = GameObject.Find ("vitesse").GetComponent<Text> ();
		fpsText = GameObject.Find ("fps").GetComponent<Text> ();
		isGameFinish = GameObject.Find ("isGameFinish").GetComponent<Text> ();


		timeleft = updateInterval;
	}

	void Update(){
		updateFPS ();

		isFly.text = "isBeginFly : "+PlayerController.isFlyBegin;
		isCask.text = "isWithCask : "+PlayerController.isWithCask;
		isShoe.text = "state : "+PlayerController.state; //"isWithShoe : "+PlayerController.isWithShoe;
		translation.text = "translation :" + PlayerController.translation;
		vitesse.text = "vitesse : " + GameController.lastPlayerSpeed.y;
		fpsText.text = "FPS : "+format;
		isGameFinish.text = "IsGameFinish : " + !GameController.isInGame;

		/*foreach(GameObject obj in allObjects){
			Renderer rend = obj.GetComponent(Renderer);
			if(rend && rend.isVisible){
				drawcalls++;
			}
		}*/
		
		//print drawcalls
		//Debug.Log(drawcalls);
		
		//reset drawcalls every update
		//drawcalls = 0;
	}

	private void updateFPS(){
		
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if (timeleft <= 0.0)
		{
			// display two fractional digits (f2 format)
			float fps = accum / frames;
			format = System.String.Format("{0:F2} FPS", fps);
			//guiText.text = format;
			
			if (fps < 30)
				fpsText.color = Color.yellow;
			else
				if (fps < 10)
					fpsText.color = Color.red;
			else
				fpsText.color = Color.green;
			//	DebugConsole.Log(format,level);
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}

	}

	private int getNextY(int indice){
		int nextY = startY + 20 * indice;
		return nextY;
	}
}
