using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charactar", menuName = "Êý¾Ý¿â/½ÇÉ«")]
public class Charactar : ScriptableObject
{
    public string charactarName;
    public Sprite charactarImg;
    public AnimatorOverrideController charactarAnimator;
    [TextArea]
    public string charactarInfo;             //ÃèÊö
}
