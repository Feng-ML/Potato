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
    public int attack;                  //伤害加成%
    public float attackSpeed;           //攻击速度%
    public int criticalRate;            //暴击率%
    public int criticalDamage = 50;     //暴击伤害%
    public int attackRange;             //攻击范围%

    [Header("防御")]
    public int armor;                   //护甲
    public int dodgeRate;               //闪避几率%

    [Header("其他")]
    public float speed;                 //移动速度%
    public int pickUpRange;             //拾取范围%
    public int gold;                    //金币
}
