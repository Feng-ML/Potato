using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDictionary
{
    //Æ·ÖÊÑÕÉ«
    public static Dictionary<MyEnums.QualityLevel, Color> qualityColor = new Dictionary<MyEnums.QualityLevel, Color>() {
        { MyEnums.QualityLevel.common, new Color32(0,0,0,100) },
        { MyEnums.QualityLevel.rare, new Color32(0,114,255,50)},
        { MyEnums.QualityLevel.epic, new Color32(234,0,209,50) },
        { MyEnums.QualityLevel.legendary, new Color32(255,142,0,50) },
        { MyEnums.QualityLevel.mythic, new Color32(253,26,11,50) },
    };
}
