using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : Pool<Explosion>
{
    private void Awake()
    {
        InitPool();
    }

    protected override void OnGetObj(Explosion ins)
    {
        base.OnGetObj(ins);
        StartCoroutine(HideIns(ins));
    }

    private IEnumerator HideIns(Explosion ins)
    {
        yield return new WaitForSeconds(.5f);
        Release(ins);
    }
}
