using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "数据库/背包")]
public class Bag : ScriptableObject
{
    public List<Item> itemsList;       //物品列表
    public List<int> countList;     //数量列表
}
