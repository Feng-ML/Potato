using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Relic : MonoBehaviour
{
    public string relicName;
    [TextArea]
    public string relicInfo;             //√Ë ˆ

    [Range(0, 1f)] public float attackProbability;
    [Range(0, 1f)] public float damageProbability;
    public PlayerStatus playerStatus;

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

    public void OnGetRelic()
    {
        GetRelic();
    }

    public virtual void Attack(Quaternion rotation, Vector2 pos, int bulletIndex) { }
    public virtual void Damage() { }
    public virtual void GetRelic() { }
}
