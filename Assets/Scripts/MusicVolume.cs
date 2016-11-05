using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicVolume : MonoBehaviour {

    // Use this for initialization
    void Start() {
        GetComponent<Slider>().value = FlexibleMusicManager.instance.volume;
    }

    public void UpdateVolume() {
        FlexibleMusicManager.instance.volume = GetComponent<Slider>().value;
    }
}

