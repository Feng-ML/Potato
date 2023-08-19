using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "数据库/玩家状态")]
public class PlayerStatus : ScriptableObject
{
    [Header("血量")]
    public int maxHealth = 100;         //最大生命值
    public int health;                  //当前生命值
    public int healthRecover;           //生命恢复量

    [Header("经验")]
    public uint level = 1;              //等级
    public int maxExp;                  //经验值上限
    public int currentExp;              //当前经验值

    [Header("攻击")]
    public float attack;                //伤害加成%
    public float attackSpeed;           //攻击速度%
    public float criticalRate;          //暴击率%
    public float criticalDamage = 50;   //暴击伤害%
    public float attackRange;           //攻击范围%

    [Header("防御")]
    public float armor;                 //护甲
    public float dodgeRate;             //闪避几率%

    [Header("其他")]
    public float speed;                 //移动速度%
    public float pickUpRange;           //拾取范围%
    public int gold;                    //金币


    // 修改属性
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

    //伤害计算
    public int GetDamageNum(int damage, ref bool isCritical)
    {
        var DamageNum = damage * (100 + attack) / 100;              //普通伤害
        // 暴击
        isCritical = false;
        var critical = criticalRate / 100;
        if (UnityEngine.Random.value < critical)
        {
            DamageNum = (DamageNum * (100 + criticalDamage) / 100);     //暴击伤害计算
            isCritical = true;
        }
        return (int)DamageNum;
    }

    //攻击范围计算
    public float GetAttackRange(float range)
    {
        return range * (100 + attackRange) / 100;
    }

    //攻击间隔计算
    public float GetAttackTime(float time)
    {
        return 1 / ((100 + attackSpeed) / (time * 100));
    }

    //是否闪避
    public bool GetIsDodge()
    {
        var dodge = dodgeRate / 100;
        dodge = dodge > 0.6f ? 0.6f : dodge;

        return UnityEngine.Random.value < dodge;
    }

    //移速计算
    public float GetSpeed(float norSpeed)
    {
        return (100 + speed) * norSpeed / 100;
    }

    //拾取范围
    public float GetPickUpRange(float norRange)
    {
        return norRange * (100 + pickUpRange) / 100;
    }
}
