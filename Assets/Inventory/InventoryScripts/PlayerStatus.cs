using System;
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


    // �޸�����
    public void AttrsChange(List<AttrObj> attrList)
    {
        foreach (var item in attrList)
        {
            var value = item.value;

            Dictionary<string, Action> setArrt = new Dictionary<string, Action>()
            {
                { Enums.character.maxHealth.ToString(), ()=> maxHealth += value  },
                { Enums.character.healthRecover.ToString(), ()=> healthRecover += value  },
                { Enums.character.attack.ToString(), ()=> attack += value  },
                { Enums.character.attackSpeed.ToString(), ()=> attackSpeed += value  },
                { Enums.character.criticalRate.ToString(), ()=> criticalRate += value  },
                { Enums.character.criticalDamage.ToString(), ()=> criticalDamage += value  },
                { Enums.character.attackRange.ToString(), ()=> attackRange += value  },
                { Enums.character.armor.ToString(), ()=> armor += value  },
                { Enums.character.dodgeRate.ToString(), ()=> dodgeRate += value  },
                { Enums.character.speed.ToString(), ()=> speed += value  },
                { Enums.character.pickUpRange.ToString(), ()=> pickUpRange += value  },
            };

            setArrt[item.Attr.ToString()].Invoke();
        }
    }
}
