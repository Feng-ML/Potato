using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic", menuName = "���ݿ�/����")]
public class RelicItem : ScriptableObject
{
    public string id;
    public string relicName;
    public Sprite relicImg;
    [TextArea]
    public string relicInfo;             //����
    public GameObject relicPrefab;       //����Ԥ����
}