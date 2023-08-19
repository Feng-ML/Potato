using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStatus playerStatus;       //玩家属性
    public Bag playerBag;

    public HealthBar healthBar;             //血条
    public ExpBar expBar;                   //经验条
    public TMP_Text goldText;               //金币文本

    public List<Vector2> weaponPsList;      //武器位置

    //单例
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

        LoadPlayerWeapon();
    }

    //加载武器
    private void LoadPlayerWeapon()
    {
        for (int i = 0; i < playerBag.weaponList.Count; i++)
        {
            Instantiate(playerBag.weaponList[i].weaponPrefab, weaponPsList[i], Quaternion.identity, transform);
        }
    }

    //受伤
    public void TakeDamage(int damage)
    {
        //闪避
        if (playerStatus.GetIsDodge())
        {
            TextPool.Instance.GetText(transform.position, "闪避", Color.white);
        }
        else
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
            TextPool.Instance.GetText(transform.position, damage, new Color(146, 0, 0));
        }
    }

    // 玩家死亡
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
        //升级
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

    private void OnDrawGizmosSelected()
    {
        weaponPsList.ForEach(item =>
        {
            Gizmos.DrawSphere(item, 0.1f);
        });
    }
}
