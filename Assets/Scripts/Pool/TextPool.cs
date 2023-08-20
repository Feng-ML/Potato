using System;
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

    public HurtText GetText(Vector3 pos, int num, Color color)
    {
        var obj = base.Get();
        obj.transform.position = GetRandomPos(pos);
        var textComponent = obj.GetComponent<TextMeshPro>();
        textComponent.text = num.ToString();
        textComponent.color = color;
        textComponent.fontSize = 5f;
        return obj;
    }

    public HurtText GetText(Vector3 pos, String str, Color color)
    {
        var obj = base.Get();
        obj.transform.position = GetRandomPos(pos);
        var textComponent = obj.GetComponent<TextMeshPro>();
        textComponent.text = str;
        textComponent.color = color;
        textComponent.fontSize = 2f;
        return obj;
    }

    //Ëæ»úÎ»ÖÃ
    private Vector3 GetRandomPos(Vector3 pos)
    {
        var x = UnityEngine.Random.Range(-0.5f, 0.5f);
        var y = UnityEngine.Random.Range(-0.5f, 0.5f);
        return pos + new Vector3(x, y, 0);
    }
}
