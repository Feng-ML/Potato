using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text levelText;


    public void SetMaxExp(int maxExp)
    {
        slider.maxValue = maxExp;
    }

    public void SetExp(int exp)
    {
        slider.value = exp;
    }

    public void AddExp(int exp)
    {
        slider.value += exp;
    }

    public void SetLevel(uint level)
    {
        levelText.SetText("Lv " + level);
    }
}
