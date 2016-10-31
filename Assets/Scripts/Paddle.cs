using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Paddle : MonoBehaviour {

    public bool useMouse = true;
    public float paddleVelocity = 50f;
    public float mouseVelocity = 100f;
    int left = -28;
    int right = 28;
    Rigidbody2D rb;


    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (useMouse) {
            ProcessMouseInput();
        } else {
            ProcessOtherInput();
        }
    }

    void ProcessMouseInput() {
        float x = (CrossPlatformInputManager.mousePosition.x / Screen.width * (right - left) + left);
        float dx = x - transform.position.x;
        if (x < left) {
            transform.position = new Vector2(left, transform.position.y);
            rb.velocity = Vector2.zero;
        } else if (x > right) {
            transform.position = new Vector2(right, transform.position.y);
            rb.velocity = Vector2.zero;
        } else if (Mathf.Abs(dx) < 5f) {
            transform.position = new Vector2(x, transform.position.y);
            rb.velocity = Vector2.zero;
        } else {
            rb.velocity = Vector2.right * Mathf.Sign(dx) * mouseVelocity;
        }
    }

    void ProcessOtherInput() {
        float multiplier = 1f;
        if (CrossPlatformInputManager.GetButton("Fire1")) {
            multiplier = 2f;
        } else if (CrossPlatformInputManager.GetButton("Fire3")) {
            multiplier = 0.5f;
        }
        float dx = CrossPlatformInputManager.GetAxis("Horizontal") * paddleVelocity * multiplier;
        if (transform.position.x < left) {
            transform.position = new Vector2(left, transform.position.y);
            dx = 0;
        } else if (transform.position.x > right) {
            transform.position = new Vector2(right, transform.position.y);
            dx = 0;
        }

        rb.velocity = Vector2.right * dx;
    }

}
