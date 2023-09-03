using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameStatus gameStatus;
    public PlayerStatus playerStatus;
    public Bag bag;
    public GameObject settingUI;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        gameStatus.Begin();
        playerStatus.Init();
        bag.Init();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Select");
    }

    public void BackToTitle()
    {
        gameStatus.status = GameStatus.gameStatusEnum.start;
        SceneManager.LoadScene("Begin");
    }

    public void OpenSetting()
    {
        settingUI.SetActive(true);
    }

    public void CancelSetting()
    {
        settingUI.SetActive(false);
    }
}
