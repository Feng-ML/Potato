using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "���ݿ�/����")]
public class Bag : ScriptableObject
{
    public Charactar charactar;                             //��ɫ

    public List<Item> itemsList;                            //��Ʒ�б�
    public List<int> countList;                             //��Ʒ�����б�

    public List<Item> weaponList;                           //�����б�
    public List<MyEnums.QualityLevel> weaponQualityList;    //����Ʒ���б�

    public List<Relic> relicsList;                          //�����б�


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
