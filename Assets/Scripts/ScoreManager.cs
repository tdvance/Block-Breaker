using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScoreManager : MonoBehaviour {

    int _score = 0;
    int _lastScore = 0;

    public int score {
        get {
            return _score;
        }
    }

    public int lastScore {
        get {
            return _lastScore;
        }
    }
    
    public List<int> topScores = new List<int>();

    public string persistanceTag = "HighScores";

    public void Reset() {
        _lastScore = score;
        _score = 0;
    }

    public void Add(int amount = 10) {
        _score += amount;
    }

    public void Set(int amount = 0) {
        _score = amount;
    }

    public void Save() {
        if (score > topScores[topScores.Count - 1]) {
            topScores[topScores.Count - 1] = score;
            topScores.Sort();
            topScores.Reverse();
            PlayerPrefs.SetInt(persistanceTag + "." + "Count", topScores.Count);
            for (int i = 0; i < topScores.Count; i++) {
                PlayerPrefs.SetInt(persistanceTag + "." + "score[" + i + "]", topScores[i]);
            }
        }
        PlayerPrefs.SetInt(persistanceTag + "." + "LastScore", score);
    }

    void Start() {
        LoadTopScoresIfPresent();
    }

    bool LoadTopScoresIfPresent() {
        _lastScore = PlayerPrefs.GetInt(persistanceTag + "." + "LastScore");
        if (PlayerPrefs.HasKey(persistanceTag + "." + "Count")) {
            int numberOfScores = PlayerPrefs.GetInt(persistanceTag + "." + "Count");
            topScores = new List<int>();
            for (int i = 0; i < numberOfScores; i++) {
                topScores.Add(PlayerPrefs.GetInt(persistanceTag + "." + "score[" + i + "]"));
            }
            topScores.Sort();
            topScores.Reverse();
            return true;
        }
        return false;
    }


    #region Singleton
    private static ScoreManager _instance;

    public static ScoreManager instance {
        get {
            if (_instance == null) {//in case not awake yet
                _instance = FindObjectOfType<ScoreManager>();
            }
            return _instance;
        }
    }

    void Awake() {
        if (_instance != null && _instance != this) {
            Debug.LogError("Duplicate singleton " + this.gameObject + " created; destroying it now");
            Destroy(this.gameObject);
        }

        if (_instance != this) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
    #endregion
}
