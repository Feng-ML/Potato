using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPool : Pool<Diamond>
{
    //����
    private static GoldPool instance;
    public static GoldPool Instance { get => instance; set => instance = value; }


    private void Awake()
    {
        Instance = this;
        InitPool();
    }
}
