using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // playSingle executes the Game on single player mode
    public void playSingle()
    {
        SceneManager.LoadScene("Game");
        GameSettings.gameMode = "Single";
    }

    // playMulti executes the Game on multiplayer mode
    public void playMulti()
    {
        SceneManager.LoadScene("Game");
        GameSettings.gameMode = "Multi";
    }

    // quit the game
    public void quitTheGame()
    {
        Application.Quit();
    }
}
