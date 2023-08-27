using System;
using UnityEngine;

public class EnemyPool : Pool<EnemyControl>
{
    private void Awake()
    {
        InitPool(100, 500, false);
    }

    protected override EnemyControl OnCreateObj()
    {
        var ins = Instantiate(prefab, transform);
        ins.SetDeactiveAction(delegate { Release(ins); });
        ins.SetActiveAction(new Func<EnemyControl>(Get));

        return ins;
    }
}
