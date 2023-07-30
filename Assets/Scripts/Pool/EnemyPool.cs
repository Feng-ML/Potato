using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : Pool<EnemyControl>
{
    private void Awake()
    {
        InitPool();
    }

    public void SetPrefab(EnemyControl obj)
    {
        prefab = obj;
    }

    protected override EnemyControl OnCreateObj()
    {
        var ins = Instantiate(prefab, transform);
        ins.SetDeactiveAction(delegate { Release(ins); });
        ins.SetActiveAction(new Func<EnemyControl>(Get));

        return ins;
    }
}
