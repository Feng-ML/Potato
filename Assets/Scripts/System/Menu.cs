using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameStatus gameStatus;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        gameStatus.wave = 1;
        SceneManager.LoadScene("Fight");
    }
}
