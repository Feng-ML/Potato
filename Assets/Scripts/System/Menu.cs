using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameStatus gameStatus;
    public PlayerStatus playerStatus;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        gameStatus.wave = 1;
        playerStatus.Init();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Fight");
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Begin");
    }
}
