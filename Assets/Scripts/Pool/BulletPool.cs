using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Pool<Bullet>
{

    private void Awake()
    {
        InitPool();
    }

    protected override Bullet OnCreateObj()
    {
        var ins = Instantiate(prefab, transform);
        ins.SetDeactiveAction(delegate { Release(ins); });

        return ins;
    }
}
