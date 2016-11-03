using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Paddle : MonoBehaviour {

    public bool autoPlay = false;
    public float autoPlayFudge = 0.5f;

    public bool useMouse = true;
    public float paddleVelocity = 50f;
    public float mouseVelocity = 100f;
    float left = -30;
    float right = 30;
    Rigidbody2D rb;

    float dx;

    internal Ball ball;

    // Use this for initialization
    void Start() {
        float widthMultiplier = (float)Screen.width / 1340f;
        float heightMultiplier = (float)Screen.height / 754f;
        left *= widthMultiplier;
        right *= widthMultiplier;
        transform.localScale = new Vector3(widthMultiplier, heightMultiplier, 1);
       
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rb.velocity = Vector2.right * dx;
    }

    void Update() {
        if ((CrossPlatformInputManager.GetButtonDown("Fire1") || CrossPlatformInputManager.GetButtonDown("Jump")) && ball && !ball.IsInPlay()) {
            ball.Launch();
        } if(autoPlay && !ball.IsInPlay()) {
            ball.Launch();
        }
        if (autoPlay) {
            ProcessAutoInput();
        } else if (useMouse) {
            ProcessMouseInput();
        } else {
            ProcessOtherInput();
        }
    }

    void ProcessAutoInput() {
        float x = ball.transform.position.x;
        dx = Mathf.Sign(x - transform.position.x) * mouseVelocity;
        if (x < left) {
            transform.position = new Vector2(left, transform.position.y);
            dx = 0;
        } else if (x > right) {
            transform.position = new Vector2(right, transform.position.y);
            dx = 0;
        } else if (Mathf.Abs(x - transform.position.x) < 5f) {
            transform.position = new Vector2(x, transform.position.y);
            dx = 0;
        }
    }

    void ProcessMouseInput() {
        float x = (CrossPlatformInputManager.mousePosition.x / Screen.width * (right - left) + left);
        dx = Mathf.Sign(x - transform.position.x) * mouseVelocity;
        if (x < left) {
            transform.position = new Vector2(left, transform.position.y);
            dx = 0;
        } else if (x > right) {
            transform.position = new Vector2(right, transform.position.y);
            dx = 0;
        } else if (Mathf.Abs(x - transform.position.x) < 5f) {
            transform.position = new Vector2(x, transform.position.y);
            dx = 0;
        }
    }

    void ProcessOtherInput() {
        float multiplier = 1f;
        if (CrossPlatformInputManager.GetButton("Fire2")) {
            multiplier = 2f;
        } else if (CrossPlatformInputManager.GetButton("Fire3")) {
            multiplier = 0.5f;
        }
        dx = CrossPlatformInputManager.GetAxis("Horizontal") * paddleVelocity * multiplier;
        if (transform.position.x < left) {
            transform.position = new Vector2(left, transform.position.y);
            dx = 0;
        } else if (transform.position.x > right) {
            transform.position = new Vector2(right, transform.position.y);
            dx = 0;
        }
    }

}
