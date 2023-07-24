using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStatus playerStatus;       //�������

    public HealthBar healthBar;   //Ѫ��
    public ExpBar expBar;       //������
    public TMP_Text goldText;   //����ı�

    //����
    private static PlayerManager instance;
    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerStatus.health = playerStatus.maxHealth;

        healthBar.SetMaxHealth(playerStatus.maxHealth);
        healthBar.SetHealth(playerStatus.maxHealth);
        expBar.SetLevel(playerStatus.level);
        expBar.SetMaxExp(playerStatus.maxExp);
        expBar.SetExp(playerStatus.currentExp);
        goldText.SetText(playerStatus.gold.ToString());
    }

    //����
    public void TakeDamage(int damage)
    {
        if (playerStatus.health < damage)
        {
            playerStatus.health = 0;
            Die();
        }
        else
        {
            playerStatus.health -= damage;
        }
        healthBar.SetHealth(playerStatus.health);
    }

    // �������
    private void Die()
    {
        Debug.Log("Die");
    }

    public void AddGold(int num)
    {
        playerStatus.gold += num;
        goldText.SetText(playerStatus.gold.ToString());
    }

    public void AddExp(int num)
    {
        playerStatus.currentExp += num;
        //����
        if (playerStatus.currentExp >= playerStatus.maxExp)
        {
            playerStatus.currentExp -= playerStatus.maxExp;
            playerStatus.level++;
            expBar.SetLevel(playerStatus.level);
            healthBar.SetMaxHealth(++playerStatus.maxHealth);
            playerStatus.maxExp += 20;
            expBar.SetMaxExp(playerStatus.maxExp);
        }
        expBar.SetExp(playerStatus.currentExp);
    }


    // �޸�����
    public void AttrsChange(List<AttrObj> attrList)
    {
        foreach (var item in attrList)
        {
            var value = item.value;

            Dictionary<string, Action> setArrt = new Dictionary<string, Action>()
            {
                { Enums.character.maxHealth.ToString(), ()=> playerStatus.maxHealth += value  },
                { Enums.character.healthRecover.ToString(), ()=> playerStatus.healthRecover += value  },
                { Enums.character.attack.ToString(), ()=> playerStatus.attack += value  },
                { Enums.character.attackSpeed.ToString(), ()=> playerStatus.attackSpeed += value  },
                { Enums.character.criticalRate.ToString(), ()=> playerStatus.criticalRate += value  },
                { Enums.character.criticalDamage.ToString(), ()=> playerStatus.criticalDamage += value  },
                { Enums.character.attackRange.ToString(), ()=> playerStatus.attackRange += value  },
                { Enums.character.armor.ToString(), ()=> playerStatus.armor += value  },
                { Enums.character.dodgeRate.ToString(), ()=> playerStatus.dodgeRate += value  },
                { Enums.character.speed.ToString(), ()=> playerStatus.speed += value  },
                { Enums.character.pickUpRange.ToString(), ()=> playerStatus.pickUpRange += value  },
            };

            setArrt[item.Attr.ToString()].Invoke();
        }
    }
}
