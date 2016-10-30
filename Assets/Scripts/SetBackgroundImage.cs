using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetBackgroundImage : MonoBehaviour {

    // Use this for initialization
    void Start() {
        int level = LevelManager.current_level;
        Texture[] backgrounds = LevelManager.backgrounds;
        Debug.Log("Current Level:  " + level);
        if (level>0 && level - 1 < backgrounds.Length) {
            GetComponent<RawImage>().texture = backgrounds[level - 1];
        }

    }

    // Update is called once per frame
    void Update() {

    }
}
