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
        criticalDamage = 0;
        attackRange = 0;
        armor = 0;
        dodgeRate = 0;
        speed = 0;
        pickUpRange = 0;
        gold = 0;
    }

    #region 属性修改
    // 宝物修改属性
    public void AttrsChange(List<AttrObj> attrList)
    {
        foreach (var item in attrList)
        {
            var value = item.value;

            Dictionary<string, Action> setArrt = new Dictionary<string, Action>()
            {
                { MyEnums.character.maxHealth.ToString(), ()=> maxHealth += value  },
                { MyEnums.character.healthRecover.ToString(), ()=> healthRecover += value  },
                { MyEnums.character.attack.ToString(), ()=> attack += value  },
                { MyEnums.character.attackSpeed.ToString(), ()=> attackSpeed += value  },
                { MyEnums.character.criticalRate.ToString(), ()=> criticalRate += value  },
                { MyEnums.character.criticalDamage.ToString(), ()=> criticalDamage += value  },
                { MyEnums.character.attackRange.ToString(), ()=> attackRange += value  },
                { MyEnums.character.armor.ToString(), ()=> armor += value  },
                { MyEnums.character.dodgeRate.ToString(), ()=> dodgeRate += value  },
                { MyEnums.character.speed.ToString(), ()=> speed += value  },
                { MyEnums.character.pickUpRange.ToString(), ()=> pickUpRange += value  },
            };

            setArrt[item.Attr.ToString()].Invoke();
        }
    }

    //添加经验
    public void AddExp(int num)
    {
        currentExp += num;
        //升级
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

    #region 计算相关
    //生命恢复计算
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

    //伤害计算
    public int GetDamageNum(int damage, ref bool isCritical)
    {
        var DamageNum = damage * (100 + attack) / 100;              //普通伤害
        // 暴击
        isCritical = false;
        var critical = criticalRate / 100;
        if (UnityEngine.Random.value < critical)
        {
            DamageNum = (DamageNum * (150 + criticalDamage) / 100);     //暴击伤害计算
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

    //护甲减伤
    public int GetDamageReduce(int damage)
    {
        var reduce = armor / (50 + armor);

        return (int)(damage * (1 - reduce));
    }
    #endregion

}
