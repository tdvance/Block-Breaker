using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    FlexibleMusicManager musicManager;

    public static int current_level = 0;

    public static Texture[] backgrounds = null;

    void Start() {
        musicManager = FindObjectOfType<FlexibleMusicManager>();
        if (backgrounds == null) {
            backgrounds = Resources.LoadAll<Texture>("Backgrounds");
        }
    }

    public void LoadLevel(string name) {
        Debug.Log("New Level load: " + name);
        SceneManager.LoadScene(name);
        if (name.Contains("Level")) {
            current_level++;
            musicManager.Next();
        } else if (name.Contains("Start Menu") || name.Contains("Win") || name.Contains("Lose")) {
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
        Debug.Log("Quit requested");
        Application.Quit();
    }

    


}
