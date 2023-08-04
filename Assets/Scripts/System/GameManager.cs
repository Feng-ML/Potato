using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 检测是否按下ESC键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // 切换游戏暂停状态
    void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f; // 将游戏时间缩放设置为1，即恢复正常
            isPaused = false;
            menu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f; // 将游戏时间缩放设置为0，即暂停
            isPaused = true;
            menu.SetActive(true);
        }
    }
}
