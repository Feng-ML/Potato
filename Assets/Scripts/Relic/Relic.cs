using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Relic : MonoBehaviour
{
    public string relicName;
    [TextArea]
    public string relicInfo;             //描述

    [Range(0, 1f)] public float attackProbability;
    [Range(0, 1f)] public float damageProbability;


    public void OnAttack(Quaternion rotation, Vector2 pos, int bulletIndex)
    {
        float t = Random.value;
        if (t <= attackProbability)
        {
            Attack(rotation, pos, bulletIndex);
        }
    }

    public void OnDamage(int damage)
    {
        float t = Random.value;
        if (t <= damageProbability)
        {
            Damage();
        }
    }

    public void OnGetRelic(PlayerManager player)
    {
        GetRelic();
    }

    public virtual void Attack(Quaternion rotation, Vector2 pos, int bulletIndex)
    {
        Debug.Log("攻击特效");
    }

    public virtual void Damage()
    {
        Debug.Log("受伤特效");
    }

    public virtual void GetRelic()
    {
        Debug.Log("获得特效");
    }
}
