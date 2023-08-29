using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charactar", menuName = "���ݿ�/��ɫ")]
public class Charactar : ScriptableObject
{
    public string charactarName;
    public Sprite charactarImg;
    public AnimatorOverrideController charactarAnimator;
    [TextArea]
    public string charactarInfo;             //����
}
