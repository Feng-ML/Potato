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
        if (BGM) BGM.volume = Seting.Instance.GetMusic() / 100f;
    }

    void Update()
    {
        // ����Ƿ���ESC��
        if (Input.GetKeyDown(KeyCode.Escape) && gameStatus.status == GameStatus.gameStatusEnum.playing)
        {
            TogglePause();
        }
    }

    // �л���Ϸ��ͣ״̬
    void TogglePause()
    {
        if (isPaused)
        {
            BGM?.Play();
            Time.timeScale = 1f; // ����Ϸʱ����������Ϊ1�����ָ�����
            isPaused = false;
            menu.SetActive(false);
        }
        else
        {
            BGM?.Pause();
            Time.timeScale = 0f; // ����Ϸʱ����������Ϊ0������ͣ
            isPaused = true;
            menu.SetActive(true);
        }
    }
}
