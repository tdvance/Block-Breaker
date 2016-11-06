using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

    // Use this for initialization
    void Start() {
        if (PlayerPrefs.HasKey("MusicVolume")) {
            FlexibleMusicManager.instance.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.HasKey("SFXVolume")) {
            SoundFXManager.instance.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
        LevelManager.instance.StartMenu();
    }

    // Update is called once per frame
    void Update() {

    }
}
