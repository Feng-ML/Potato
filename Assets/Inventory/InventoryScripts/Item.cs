using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "���ݿ�/����")]
public class Item : ScriptableObject
{
    public string id;
    public string itemName;
    public Sprite itemImg;
    [TextArea]
    public string itemInfo;             //����
    public int cost;                    //�۸�
    public Enums.QualityLevel quality;  //Ʒ��
    public List<AttrObj> effectList;    //����Ч��

    public bool isWeapon;                //�Ƿ�Ϊ����
    public GameObject weaponPrefab;     //����Ԥ����
}

[System.Serializable]
public class AttrObj
{
    public Enums.character Attr;
    public int value;
}