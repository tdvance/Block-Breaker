using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    FlexibleMusicManager musicManager;

    public static int current_level = 0;

    public static int numBricks = -1;

    public static Texture[] backgrounds = null;

    public static Texture2D[] levels = null;

    void Start() {
        if (numBricks == 0) {
            numBricks = -1;
        }
        musicManager = FindObjectOfType<FlexibleMusicManager>();
        if (backgrounds == null) {
            backgrounds = Resources.LoadAll<Texture>("Backgrounds");
        }

        if (levels == null) {
            levels = Resources.LoadAll<Texture2D>("Levels");
        }
    }

    public void StartGame() {
        ScoreManager.instance.Reset();
        LoadLevel("Level_01");
    }

    public void LevelUp() {
        SoundFXManager.instance.play(0);
        SaveStatistics("Level_" + current_level);
        StatisticsManager.instance.LogStats("Level_" + current_level + ".time");
        Invoke("LoadNextLevel", 1f);
    }

    void LoadNextLevel() {
        LoadLevel("Level_01");
    }


    public void LoadLevel(string name) {
        Debug.Log("New Level load: " + name);
        SceneManager.LoadScene(name);
        if (name.Contains("Level")) {
            current_level++;
            //TODO remove this when done testing
            if (current_level > levels.Length) {
                current_level = 1;
            }
            musicManager.Next();
        } else if (name.Contains("Start Menu") || name.Contains("Win") || name.Contains("Lose")) {
            Ball.numBalls = 3;
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
    }

    public void QuitRequest() {
        LoadLevel("Start Menu");
        Debug.Log("Quit requested");
        Application.Quit();
    }


    private void SaveStatistics(string prefix) {
        StatisticsManager.instance.AddValue(Time.timeSinceLevelLoad, prefix + ".time");
    }

}
