using UnityEngine;
using System.Collections;

public class ResponsiveController : MonoBehaviour {

	public float targetaspectUp,targetaspectDown;
	private static float largeur_ecran;
	private static float hauteur_ecran;
	
	public static float largeurEcranRepere = 800;
	public static float  hauteurEcranRepere = 1280;

	private float  ratioX,ratioY;

	// Use this for initialization
	void Start () {
	
		/******************* on force le mode portrait *********************/
		/********************************************************************/
		Screen.autorotateToPortrait = false;		
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = false;		
		//Screen.orientation = ScreenOrientation.AutoRotation;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Screen.orientation = ScreenOrientation.Portrait;


		Debug.Log("ResponsiveController: le device est en mode "+Screen.orientation);

		/************************ récupération des dimensions du terminal *************************/
		/*******************************************************************************************/

		//largeur_ecran = Screen.currentResolution.width;
		//hauteur_ecran = Screen.currentResolution.height;
		largeur_ecran = Screen.width;
		hauteur_ecran = Screen.height;
		ratioX = largeur_ecran/largeurEcranRepere;
		ratioY = hauteur_ecran/hauteurEcranRepere;
		Debug.Log("ResponsiveController currentResolution: "+Screen.currentResolution.width+" x "+Screen.currentResolution.height);


		
		/************************ modification du viewport de Caméra *************************/
		/*******************************************************************************************/
		// set the desired aspect ratio 
		float targetaspect = targetaspectUp / targetaspectDown;
		
		// determine the game window's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;
		
		// current viewport height should be scaled by this amount
		float scaleheight = windowaspect / targetaspect;
		
		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera>();
		
		// if scaled height is less than current height, add letterbox
		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;
			
			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;
			
			camera.rect = rect;
		}
		else // add pillarbox
		{
			float scalewidth = 1.0f / scaleheight;
			
			Rect rect = camera.rect;
			
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;
			
			camera.rect = rect;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	/************** méthodes de placement de la GUI en fonction du terminal   ****************/
	/*******************************************************************************************/
	public static float adaptCoordX(float x){

		//Debug.Log("ResponsiveController: on va adapter les coordX ou width de la GUI");
		float xFinal = x*largeur_ecran/largeurEcranRepere;
		return xFinal;
	}

	public static float adaptCoordY(float y){
		
		//Debug.Log("ResponsiveController: on va adapter les coordY ou Height de la GUI");
		float yFinal =  y*hauteur_ecran/hauteurEcranRepere;
		return  yFinal;
	}

	public float getRatioX(){return ratioX;}
	public float getRatioY(){return ratioY;}

}
