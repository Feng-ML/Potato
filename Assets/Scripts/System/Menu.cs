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
        gameStatus.status = GameStatus.gameStatusEnum.playing;
        SceneManager.LoadScene("Fight");
    }

    public void BackToTitle()
    {
        gameStatus.status = GameStatus.gameStatusEnum.start;
        SceneManager.LoadScene("Begin");
    }
}
