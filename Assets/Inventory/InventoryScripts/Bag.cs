using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "���ݿ�/����")]
public class Bag : ScriptableObject
{
    public List<Item> itemsList;                            //��Ʒ�б�
    public List<int> countList;                             //��Ʒ�����б�

    public List<Item> weaponList;                           //�����б�
    public List<Enums.QualityLevel> weaponQualityList;       //����Ʒ���б�
}
