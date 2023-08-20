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
    public float attack;                //�˺��ӳ�%
    public float attackSpeed;           //�����ٶ�%
    public float criticalRate;          //������%
    public float criticalDamage = 50;   //�����˺�%
    public float attackRange;           //������Χ%

    [Header("����")]
    public float armor;                 //����
    public float dodgeRate;             //���ܼ���%

    [Header("����")]
    public float speed;                 //�ƶ��ٶ�%
    public float pickUpRange;           //ʰȡ��Χ%
    public int gold;                    //���


    public void Init()
    {
        maxHealth = 50;
        healthRecover = 0;
        level = 1;
        maxExp = 100;
        currentExp = 0;
        attack = 0;
        attackSpeed = 0;
        criticalRate = 0;
        criticalDamage = 50;
        attackRange = 0;
        armor = 0;
        dodgeRate = 0;
        speed = 0;
        pickUpRange = 0;
        gold = 0;
    }

    #region �����޸�
    // �����޸�����
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

    //��Ӿ���
    public void AddExp(int num)
    {
        currentExp += num;
        //����
        if (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            level++;
            maxExp += 20;
            maxHealth++;
            health++;
        }
    }
    #endregion

    #region �������
    //�����ָ�����
    public int GetHealthRecover()
    {
        var range = maxHealth - health;
        if (healthRecover < range)
        {
            health += healthRecover;
            return healthRecover;
        }
        else
        {
            health += range;
            return range;
        }
    }

    //�˺�����
    public int GetDamageNum(int damage, ref bool isCritical)
    {
        var DamageNum = damage * (100 + attack) / 100;              //��ͨ�˺�
        // ����
        isCritical = false;
        var critical = criticalRate / 100;
        if (UnityEngine.Random.value < critical)
        {
            DamageNum = (DamageNum * (100 + criticalDamage) / 100);     //�����˺�����
            isCritical = true;
        }
        return (int)DamageNum;
    }

    //������Χ����
    public float GetAttackRange(float range)
    {
        return range * (100 + attackRange) / 100;
    }

    //�����������
    public float GetAttackTime(float time)
    {
        return 1 / ((100 + attackSpeed) / (time * 100));
    }

    //�Ƿ�����
    public bool GetIsDodge()
    {
        var dodge = dodgeRate / 100;
        dodge = dodge > 0.6f ? 0.6f : dodge;

        return UnityEngine.Random.value < dodge;
    }

    //���ټ���
    public float GetSpeed(float norSpeed)
    {
        return (100 + speed) * norSpeed / 100;
    }

    //ʰȡ��Χ
    public float GetPickUpRange(float norRange)
    {
        return norRange * (100 + pickUpRange) / 100;
    }

    //���׼���
    public int GetDamageReduce(int damage)
    {
        var reduce = armor / (50 + armor);

        return (int)(damage * (1 - reduce));
    }
    #endregion

}
