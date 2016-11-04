using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SFXVolume : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Slider>().value = SoundFXManager.instance.volume;
	}
	
	public void UpdateVolume() {
        SoundFXManager.instance.volume = GetComponent<Slider>().value;
    }
}
