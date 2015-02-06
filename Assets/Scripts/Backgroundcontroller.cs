using UnityEngine;
using System.Collections;


/*
 * Gestion d'un dégradé à trois couleurs 
 * */

public class Backgroundcontroller : MonoBehaviour {

	public Color col1,col2,col3;
	//Material planMat;
	Color colorFinale = new Color(1,1,1,1);
	Color color = new Color(1,1,1,1); //couleur du differentiel pour debug !!
	float r,g,b,r2,g2,b2;//teintes du différentiel entre les couleurs
	public int altCght = 40000;
	public int altFin = 80000;
	SpriteRenderer renderer;

	void Start(){
		//if (levelNum == 1) {
				
			/*color.r = (float)161/255;
			color.g = (float)198/255;
			color.b = (float)230/255;*/

			/*col1 = new Color((float)161/255,(float)198/255,(float)230/255);
			col2 = new Color((float)203/255,(float)140/255,(float)131/255);
			col3 = new Color(0,0,0,1);*/


			r = (float)((col2.r - col1.r));
			g = (float)((col2.g - col1.g));
			b = (float)((col2.b - col1.b));
			r2 = (float)((col3.r - col2.r));
			g2 = (float)((col3.g - col2.g));
			b2 = (float)((col3.b - col2.b));

			color.r = r;
			color.g = g;
			color.b = b;

		//planMat = GetComponent<MeshRenderer> ().materials [0];
		renderer = GetComponent<SpriteRenderer> ();

	}

	void Update () {

		/*float bleu = 1 - GameController.altitude / 50000;
		float vert = 0.8f - GameController.altitude / 30000;
		color.r = 0;
		color.g = vert;
		color.b = bleu;
		color.a = 1;*/
		//Color color = new Color(0,vert,bleu,1);
		//planMat.color = color;

		if(GameController.altitude < altCght){

			colorFinale.r = (float)(col1.r + r * GameController.altitude/ altCght);
			colorFinale.g = (float)(col1.g + g * GameController.altitude/ altCght);
			colorFinale.b = (float)(col1.b + b * GameController.altitude/ altCght);
		}
		else if(GameController.altitude >= altCght){
			colorFinale.r = (float)(col2.r + r2 * (GameController.altitude - altCght )/ altFin);
			colorFinale.g = (float)(col2.g + g2 * (GameController.altitude - altCght )/ altFin);
			colorFinale.b = (float)(col2.b + b2 * (GameController.altitude - altCght )/ altFin);

		}
		renderer.color = colorFinale;
	}

}
