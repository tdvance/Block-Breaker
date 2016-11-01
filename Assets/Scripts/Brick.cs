using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
    public int maxHits = 1;

    private int timesHit = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Ball>()) {
            timesHit++;
            if (timesHit >= maxHits) {
                Destroy(gameObject);
                LevelManager.numBricks--;
                if (LevelManager.numBricks <= 0) {
                    FindObjectOfType<LevelManager>().LevelUp();
                }
            }
        }
    }
}
