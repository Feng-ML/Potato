using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameStatus gameStatus;
    private bool isPaused;
    private AudioSource BGM;

    private void Awake()
    {
        BGM = gameObject.GetComponent<AudioSource>();
        BGM.volume = Seting.Instance.GetMusic() / 100f;

        Seting.OnSetingChanged += ChangeSeting;
    }

    void Update()
    {
        // 检测是否按下ESC键
        if (Input.GetKeyDown(KeyCode.Escape) && gameStatus.status == GameStatus.gameStatusEnum.playing)
        {
            TogglePause();
        }
    }

    private void OnDestroy()
    {
        Seting.OnSetingChanged -= ChangeSeting;
    }

    // 切换游戏暂停状态
    void TogglePause()
    {
        if (isPaused)
        {
            BGM?.Play();
            Time.timeScale = 1f; // 将游戏时间缩放设置为1，即恢复正常
            isPaused = false;
            menu.SetActive(false);
        }
        else
        {
            BGM?.Pause();
            Time.timeScale = 0f; // 将游戏时间缩放设置为0，即暂停
            isPaused = true;
            menu.SetActive(true);
        }
    }

    // 设置改变 订阅
    private void ChangeSeting(JsonData obj)
    {
        BGM.volume = Seting.Instance.GetMusic() / 100f;
    }
}
