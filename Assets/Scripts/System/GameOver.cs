using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TMP_Text title;
    public GameStatus gameStatus;

    public void Active(bool isWin = false)
    {
        Time.timeScale = 0f;
        title.text = isWin ? "victory£¡" : "Game Over";
        gameObject.SetActive(true);
        gameStatus.status = isWin ? GameStatus.gameStatusEnum.victory : GameStatus.gameStatusEnum.failure;
    }
}
