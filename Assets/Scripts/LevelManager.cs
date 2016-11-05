﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    FlexibleMusicManager musicManager;

    public int current_level = 0;

    public int numBricks = -1;

    public Texture[] backgrounds = null;

    public Texture2D[] levels = null;

    float gameTime, q3Time, q1Time, medTime;

    void Start() {
        if (numBricks == 0) {
            numBricks = -1;
        }
        musicManager = FindObjectOfType<FlexibleMusicManager>();
        backgrounds = Resources.LoadAll<Texture>("Backgrounds");
        levels = Resources.LoadAll<Texture2D>("Levels");
    }

    public void LoadGameLevel(float time) {
        Invoke("LoadGameLevel", time);
    }

    public void LoadGameLevel() {
        LoadLevel("GameLevel");
    }


    public void LevelUp() {
        int bonus = ComputeBonus();
        ScoreManager.instance.Add(bonus);

        gameTime = Mathf.Round(gameTime * 100) / 100;
        medTime = Mathf.Round(medTime * 100) / 100;
        GameObject.Find("Bonus").GetComponent<Text>().text =
              "\n      Time: " + gameTime
            + "\n       Par: " + medTime
            + "\n<color=green>Time Bonus: " + bonus + "</color>";

        FindObjectOfType<Ball>().Stop();
        SoundFXManager.instance.play(0);
        StatisticsManager.instance.AddValue(Time.timeSinceLevelLoad, "Level_" + LevelManager.instance.current_level + ".time");
        StatisticsManager.instance.LogStats("Level_" + LevelManager.instance.current_level + ".time");
        Invoke("StartNextLevel", 3f);
    }

    void StartNextLevel() {
        SceneManager.LoadScene("GameLevel");
        current_level++;
        //TODO remove this when done testing
        if (current_level > levels.Length) {
            current_level = 1;
        }
        musicManager.Next();
    }

    public void LoadLevel(string name) {
        Debug.Log("New Level load: " + name);
        SceneManager.LoadScene(name);
    }

    public void StartMenu() {
        SceneManager.LoadScene("Start Menu");
    }

    public void GameOver() {
        ResetGame();
        SceneManager.LoadScene("Game Over");
    }

    public void ResetGame() {
        if (current_level != 0) {
            ScoreManager.instance.Save();
        }
        current_level = 0;
        if (!musicManager) {
            musicManager = FindObjectOfType<FlexibleMusicManager>();
        }
        if (musicManager.CurrentTrackNumber() > 0) {
            musicManager.Rewind();
        }
    }


    int ComputeBonus() {
        gameTime = Time.timeSinceLevelLoad;
        StatisticsManager.instance.LogStats("Level_" + LevelManager.instance.current_level + ".time");
        q3Time = StatisticsManager.instance.GetQ3();
        q1Time = StatisticsManager.instance.GetQ1();
        medTime = StatisticsManager.instance.GetMed();
        int bonus = ((int)((q3Time - gameTime) / 10)) * 50 + 10;

        Debug.Log("Ok: " + q3Time);
        Debug.Log("Par:" + medTime);
        Debug.Log("Good: " + q1Time);

        if (bonus < 0) {
            return 0;
        }

        int bonusAdder = ((int)((q1Time - gameTime) / 10)) * 150 + 10;
        if (bonusAdder > 0) {
            bonus += bonusAdder;
        }
        return bonus;
    }


    #region Singleton
    private static LevelManager _instance;

    public static LevelManager instance {
        get {
            if (_instance == null) {//in case not awake yet
                _instance = FindObjectOfType<LevelManager>();
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
