using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "���ݿ�/���״̬")]
public class PlayerStatus : ScriptableObject
{
    [Header("Ѫ��")]
    public int maxHealth = 100;         //�������ֵ
    public int health;                  //��ǰ����ֵ
    public int healthRecover;           //�����ָ���

    [Header("����")]
    public uint level = 1;              //�ȼ�
    public int maxExp;                  //����ֵ����
    public int currentExp;              //��ǰ����ֵ

    [Header("����")]
    public int attack;                  //�˺��ӳ�%
    public float attackSpeed;           //�����ٶ�%
    public int criticalRate;            //������%
    public int criticalDamage = 50;     //�����˺�%
    public int attackRange;             //������Χ%

    [Header("����")]
    public int armor;                   //����
    public int dodgeRate;               //���ܼ���%

    [Header("����")]
    public float speed;                 //�ƶ��ٶ�%
    public int pickUpRange;             //ʰȡ��Χ%
    public int gold;                    //���
}
