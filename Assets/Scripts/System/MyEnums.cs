using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnums
{
    //�������
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

    // Ʒ��
    public enum QualityLevel
    {
        common = 1,
        rare,
        epic,
        legendary,
        mythic
    }
}
