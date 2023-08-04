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
        // ����Ƿ���ESC��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // �л���Ϸ��ͣ״̬
    void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f; // ����Ϸʱ����������Ϊ1�����ָ�����
            isPaused = false;
            menu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f; // ����Ϸʱ����������Ϊ0������ͣ
            isPaused = true;
            menu.SetActive(true);
        }
    }
}
