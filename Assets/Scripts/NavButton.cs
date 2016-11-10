using UnityEngine;
using System.Collections;

public class NavButton : MonoBehaviour {

    public void Quit() {
        LevelManager.instance.GameOver();
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public void StartGame() {
        ScoreManager.instance.Reset();
        Ball.numBalls = 3;
        LevelManager.instance.current_level = 1;
        FlexibleMusicManager.instance.ChangeTrack(1);
        LevelManager.instance.LoadLevel("GameLevel");
    }

    public void GameOver() {
        LevelManager.instance.GameOver();
    }


    public void MainMenu() {
        LevelManager.instance.StartMenu();

    }

    public void Options() {
        LevelManager.instance.LoadLevel("Options");

    }

}
