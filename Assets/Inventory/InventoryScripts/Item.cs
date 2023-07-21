using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "数据库/宝物")]
public class Item : ScriptableObject
{
    public string id;
    public string itemName;
    public Texture2D itemImg;
    [TextArea]
    public string itemInfo;         //描述
    public int cost;                //价格
    public QualityLevel quality;    //品质
    public bool isEquip;            //是否为装备

    // 品质
    public enum QualityLevel
    {
        common,
        rare,
        epic,
        legendary,
        mythic
    }
}
