using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "数据库/宝物")]
public class Item : ScriptableObject
{
    public string id;
    public string itemName;
    public Sprite itemImg;
    [TextArea]
    public string itemInfo;             //描述
    public int cost;                    //价格
    public Enums.QualityLevel quality;  //品质
    public List<AttrObj> effectList;    //宝物效果

    public bool isWeapon;                //是否为武器
    public GameObject weaponPrefab;     //武器预制体
}

[System.Serializable]
public class AttrObj
{
    public Enums.character Attr;
    public int value;
}