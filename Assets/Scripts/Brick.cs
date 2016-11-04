using UnityEngine;
using UnityEngine.UI;
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
                c = new Color(c.r * 0.5f, c.g * 0.5f, c.b * 0.5f, c.a * 0.25f);
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
                    int bonus = ComputeBonus();
                    ScoreManager.instance.Add(bonus);
                    if (bonus > 0) {
                        GameObject.Find("Bonus").GetComponent<Text>().text = "Time Bonus: " + bonus;
                    }
                    Debug.Log("Time Bonus: " + bonus);
                    FindObjectOfType<Ball>().Stop();
                    FindObjectOfType<LevelManager>().LevelUp();
                }
                Destroy(gameObject);
            } else {
                GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit];
            }
        }
    }

    int ComputeBonus() {
        float time = Time.timeSinceLevelLoad;
        StatisticsManager.instance.LogStats("Level_" + LevelManager.current_level + ".time");
        float q3Time = StatisticsManager.instance.GetQ3();
        float q1Time = StatisticsManager.instance.GetQ1();
        float medTime = StatisticsManager.instance.GetMed();
        int bonus = ((int)((q3Time - time) / 10)) * 10 + 10;

        Debug.Log("Time: " + time);
        Debug.Log("Ok: " + q3Time);
        Debug.Log("Par:" + medTime);
        Debug.Log("Good: " + q1Time);

        if (bonus < 0) {
            return 0;
        }

        int bonusAdder = ((int)((q1Time - time) / 10)) * 30 + 30;
        if (bonusAdder > 0) {
            bonus += bonusAdder;
        }
        return bonus;
    }
}
