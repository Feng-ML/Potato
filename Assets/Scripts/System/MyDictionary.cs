using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDictionary
{
    //Æ·ÖÊÑÕÉ«
    public static Dictionary<Enums.QualityLevel, Color> qualityColor = new Dictionary<Enums.QualityLevel, Color>() {
        { Enums.QualityLevel.common, new Color32(0,0,0,100) },
        { Enums.QualityLevel.rare, new Color32(0,114,255,50)},
        { Enums.QualityLevel.epic, new Color32(234,0,209,50) },
        { Enums.QualityLevel.legendary, new Color32(255,142,0,50) },
        { Enums.QualityLevel.mythic, new Color32(253,26,11,50) },
    };
}
