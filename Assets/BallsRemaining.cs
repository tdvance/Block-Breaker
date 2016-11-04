using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallsRemaining : MonoBehaviour {
    private Text ballsRemainingText;

    // Use this for initialization
    void Start() {
        ballsRemainingText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update() {
        ballsRemainingText.text =  "Balls: " +  Ball.numBalls.ToString();

    }
}
