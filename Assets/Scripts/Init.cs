using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LevelManager.instance.LoadLevel("Start Menu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
