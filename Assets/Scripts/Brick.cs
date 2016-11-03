using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    public Sprite[] hitSprites;

    public GameObject smokePrefab;

    public int maxHits = 1;

    private int timesHit = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Ball>()) {
            timesHit++;
            if (timesHit >= maxHits) {
                SoundFXManager.instance.play(3);
                GameObject smoke = Instantiate<GameObject>(smokePrefab);
                Destroy(smoke, 5f);
                smoke.transform.position = transform.position;
                Color c = GetComponent<SpriteRenderer>().color;
                c = new Color(c.r*0.5f, c.g*0.5f, c.b*0.5f, c.a*0.25f);
                smoke.GetComponent<ParticleSystem>().startColor = c;
                LevelManager.numBricks--;

                ScoreManager.instance.Add(10);


                if (timesHit > 1) {
                    ScoreManager.instance.Add(20);
                    if (timesHit > 2) {
                        ScoreManager.instance.Add(25);
                    }
                }

                if (LevelManager.numBricks <= 0) {
                    ScoreManager.instance.Add(100);
                   FindObjectOfType<Ball>().Stop();
                    FindObjectOfType<LevelManager>().LevelUp();
                }
                Destroy(gameObject);
            } else {
                GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit];
            }
        }
    }
}
