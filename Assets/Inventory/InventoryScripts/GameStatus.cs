using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStatus", menuName = "数据库/游戏状态")]
public class GameStatus : ScriptableObject
{
    public uint wave;       //第几波
}
