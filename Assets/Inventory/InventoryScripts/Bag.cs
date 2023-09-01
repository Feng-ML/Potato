using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "数据库/背包")]
public class Bag : ScriptableObject
{
    public Charactar charactar;                             //角色

    public List<Item> itemsList;                            //物品列表
    public List<int> countList;                             //物品数量列表

    public List<Item> weaponList;                           //武器列表
    public List<MyEnums.QualityLevel> weaponQualityList;    //武器品质列表

    public List<Relic> relicsList;                          //遗物列表


    public void Init()
    {
        charactar = null;
        itemsList.Clear();
        countList.Clear();
        weaponList.Clear();
        weaponQualityList.Clear();
        relicsList.Clear();
    }
}
