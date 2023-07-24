using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    //玩家属性
    public enum character
    {
        maxHealth,
        healthRecover,
        attack,
        attackSpeed,
        criticalRate,
        criticalDamage,
        attackRange,
        armor,
        dodgeRate,
        speed,
        pickUpRange
    }

    // 品质
    public enum QualityLevel
    {
        common,
        rare,
        epic,
        legendary,
        mythic
    }
}
