using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "数据库/玩家状态")]
public class PlayerStatus : ScriptableObject
{
    [Header("血量")]
    public int maxHealth;      //最大生命值
    public int health;      //当前生命值

    [Header("经验")]
    public uint level;      //等级
    public int maxExp;      //经验值上限
    public int currentExp;      //当前经验值

    [Header("攻击")]
    public int attack;        // 攻击加成
    public float attackSpeed;       //攻击速度

    [Header("其他")]
    public float speed;       //移动速度
    public int gold;        //金币
}
