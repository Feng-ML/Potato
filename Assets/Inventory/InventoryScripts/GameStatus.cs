using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStatus", menuName = "���ݿ�/��Ϸ״̬")]
public class GameStatus : ScriptableObject
{
    public gameStatusEnum status;
    public uint wave;       //�ڼ���

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
