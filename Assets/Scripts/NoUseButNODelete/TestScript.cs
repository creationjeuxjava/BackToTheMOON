using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

    public GUIStyle myStyle ;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUILayout.Button(" lol", myStyle);
    }


}
