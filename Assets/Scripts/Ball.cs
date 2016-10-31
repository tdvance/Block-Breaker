using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    int left = -32;
    int right = 32;
    int top = 18;
    int bottom = -18;
    float minXVelocity = -75;
    float maxXVelocity = 75;
    float minYVelocity = -75;
    float maxYVelocity = 75;

    Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y < bottom - 10) {
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
    }
}
