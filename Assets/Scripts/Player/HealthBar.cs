using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text currentHealthText;
    public TMP_Text maxHealthText;


    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        maxHealthText.SetText(maxHealth.ToString());
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        currentHealthText.SetText(health.ToString());
    }

    public void AddHealth(int health)
    {
        slider.value += health;
    }
}
