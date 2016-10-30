using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    int bottom = -18;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.position.y < bottom) {
            FindObjectOfType<LevelManager>().LoadLevel("Lose Screen");
        }
	}
}
