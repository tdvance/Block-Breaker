using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    private GameObject paddle;


    public Vector2 startVelocity = new Vector2(30, 30);

    private Vector3 paddleToBallVector;

    int left = -32;
    int right = 32;
    int top = 18;
    int bottom = -18;
    float minXVelocity = -30;
    float maxXVelocity = 30;
    float minYVelocity = -20;
    float maxYVelocity = 20;
    float minVelocityMag = 14f;
    float minXMag = 5;
    float minYMag = 5;

    private bool inPlay = false;

    Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        paddle = GameObject.Find("Paddle");
        paddle.GetComponent<Paddle>().ball = this;
        paddleToBallVector = transform.position - paddle.transform.position;
    }

    // Update is called once per frame
    void Update() {

        if (inPlay) {
            if (rb.velocity.magnitude < minVelocityMag) {
                rb.velocity *= 1.5f;
            } else if (rb.velocity.x < minXMag && rb.velocity.x > -minXMag) {
                rb.velocity = new Vector2(rb.velocity.x * 1.5f, 0.5f + rb.velocity.y);
            } else if (rb.velocity.y < minYMag && rb.velocity.y > -minYMag) {
                rb.velocity = new Vector2(rb.velocity.x, 0.5f + rb.velocity.y * 1.5f);
            }
            if (transform.position.y < bottom - 2) {
                FindObjectOfType<LevelManager>().LoadLevel("Lose Screen");
            } else if (transform.position.y > top) {
                rb.velocity = new Vector2(rb.velocity.x, -Mathf.Abs(rb.velocity.y));
            }
            if (transform.position.x > right) {
                rb.velocity = new Vector2(-Mathf.Abs(rb.velocity.x), rb.velocity.y);
            } else if (transform.position.x < left) {
                rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x), rb.velocity.y);
            }
            if (rb.velocity.x < minXVelocity) {
                rb.velocity = new Vector2(minXVelocity, rb.velocity.y);
            } else if (rb.velocity.x > maxXVelocity) {
                rb.velocity = new Vector2(maxXVelocity, rb.velocity.y);
            }
            if (rb.velocity.y < minYVelocity) {
                rb.velocity = new Vector2(rb.velocity.x, minYVelocity);
            } else if (rb.velocity.y > maxYVelocity) {
                rb.velocity = new Vector2(rb.velocity.x, maxYVelocity);
            }
        } else {
            transform.position = paddleToBallVector + paddle.transform.position;
        }
    }

    public void Launch() {
        inPlay = true;
        rb.velocity = startVelocity;
    }

    public bool IsInPlay() {
        return inPlay;
    }


}
