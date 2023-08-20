using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TMP_Text title;

    public void Active(string str = "Game Over")
    {
        Time.timeScale = 0f;
        title.text = str;
        gameObject.SetActive(true);
    }
}
