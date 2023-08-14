using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic1 : Relic
{
    public override void Attack(Quaternion rotation, Vector2 pos, int bulletIndex)
    {
        var bulletPoolList = PoolControl.Instance.bulletPool;
        Bullet bullentIns1 = bulletPoolList[bulletIndex].Get();
        Bullet bullentIns2 = bulletPoolList[bulletIndex].Get();

        bullentIns1.transform.position = pos;
        bullentIns2.transform.position = pos;

        // 计算正负15度的偏移Quaternion
        Quaternion positiveOffset = Quaternion.AngleAxis(15f, Vector3.forward) * rotation;
        Quaternion negativeOffset = Quaternion.AngleAxis(-15f, Vector3.forward) * rotation;
        bullentIns1.transform.rotation = positiveOffset;
        bullentIns2.transform.rotation = negativeOffset;
    }
}
