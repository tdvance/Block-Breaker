using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LastScore : MonoBehaviour {

    private Text scoreText;

    // Use this for initialization
    void Start() {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        scoreText.text = "Last Score: " + ScoreManager.instance.lastScore.ToString();
    }
}
