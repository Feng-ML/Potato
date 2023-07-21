using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopProductList", menuName = "数据库/商店列表")]
public class ShopProductList : ScriptableObject
{
    public List<Item> itemsList;
}
