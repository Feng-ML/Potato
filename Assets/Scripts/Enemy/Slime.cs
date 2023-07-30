using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyControl
{
    protected override void Die()
    {
        if (level >= 1)
        {
            //µôÂä
            var gold = GoldPool.Instance.Get();
            gold.transform.position = transform.position;
        }
        else
        {
            bornChild(transform.position + new Vector3(1f, 0f, 0f));
            bornChild(transform.position + new Vector3(-1f, 0f, 0f));
        }
        releaseAction.Invoke();
    }

    private void bornChild(Vector3 v3)
    {
        var newChild = getAction.Invoke();
        newChild.transform.localScale = new Vector2(7, 7);
        newChild.level++;
        newChild.transform.position = v3;
        newChild.currenthealth = maxHealth / 2;
    }
}
