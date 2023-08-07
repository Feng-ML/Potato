using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPool : Pool<HurtText>
{
    //µ¥Àý
    private static TextPool instance;
    public static TextPool Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
        InitPool();
    }

    protected override HurtText OnCreateObj()
    {
        var ins = Instantiate(prefab, transform);
        ins.SetDeactiveAction(delegate { Release(ins); });

        return ins;
    }

    public HurtText GetText(Vector3 pos, int num)
    {
        var obj = base.Get();
        //Ëæ»úÎ»ÖÃ
        var x = UnityEngine.Random.Range(-0.5f, 0.5f);
        var y = UnityEngine.Random.Range(-0.5f, 0.5f);
        obj.transform.position = pos + new Vector3(x, y, 0);
        obj.GetComponent<TextMeshPro>().text = num.ToString();
        return obj;
    }
}
