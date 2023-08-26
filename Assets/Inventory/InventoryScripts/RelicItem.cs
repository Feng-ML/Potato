using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relic", menuName = "数据库/遗物")]
public class RelicItem : ScriptableObject
{
    public string id;
    public string relicName;
    [TextArea]
    public string relicInfo;             //描述
    public GameObject relicPrefab;       //遗物预制体
}
