using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bag", menuName = "���ݿ�/����")]
public class Bag : ScriptableObject
{
    public List<Item> itemsList;       //��Ʒ�б�
    public List<int> countList;     //�����б�
}
