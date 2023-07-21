using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "���ݿ�/���״̬")]
public class PlayerStatus : ScriptableObject
{
    [Header("Ѫ��")]
    public int maxHealth;      //�������ֵ
    public int health;      //��ǰ����ֵ

    [Header("����")]
    public uint level;      //�ȼ�
    public int maxExp;      //����ֵ����
    public int currentExp;      //��ǰ����ֵ

    [Header("����")]
    public int attack;        // �����ӳ�
    public float attackSpeed;       //�����ٶ�

    [Header("����")]
    public float speed;       //�ƶ��ٶ�
    public int gold;        //���
}
