using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStatus", menuName = "数据库/游戏状态")]
public class GameStatus : ScriptableObject
{
    public gameStatusEnum status;
    public uint wave;       //第几波

    public enum gameStatusEnum
    {
        start,
        playing,
        failure,
        victory,
        end
    }

    public void Begin()
    {
        wave = 1;
        status = gameStatusEnum.playing;
    }
}
