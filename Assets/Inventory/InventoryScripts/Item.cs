using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "���ݿ�/����")]
public class Item : ScriptableObject
{
    public string id;
    public string itemName;
    public Texture2D itemImg;
    [TextArea]
    public string itemInfo;             //����
    public int cost;                    //�۸�
    public Enums.QualityLevel quality;        //Ʒ��
    public bool isEquip;                //�Ƿ�Ϊװ��
    public List<AttrObj> effectList;    //����Ч��

}

[System.Serializable]
public class AttrObj
{
    public Enums.character Attr;
    public int value;
}