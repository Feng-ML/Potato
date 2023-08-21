using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerStatus playerStatus;       //玩家属性
    public AudioClip hurtAudio;

    public HealthBar healthBar;             //血条
    public ExpBar expBar;                   //经验条
    public TMP_Text goldText;               //金币文本
    public GameObject gameOverUI;

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
        UpdateUI();

        InvokeRepeating("HealthRecover", 0f, 1f);
    }

    //生命恢复
    private void HealthRecover()
    {
        var recoverNum = playerStatus.GetHealthRecover();
        if (recoverNum > 0)
        {
            TextPool.Instance.GetText(transform.position, recoverNum, Color.green);
            healthBar.SetHealth(playerStatus.health);
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
            var realDamage = playerStatus.GetDamageReduce(damage);
            AudioSource.PlayClipAtPoint(hurtAudio, transform.position);

            if (playerStatus.health < realDamage)
            {
                playerStatus.health = 0;
                Die();
            }
            else
            {
                playerStatus.health -= realDamage;
            }
            healthBar.SetHealth(playerStatus.health);
            TextPool.Instance.GetText(transform.position, -realDamage, new Color(146, 0, 0));
        }
    }

    // 玩家死亡
    private void Die()
    {
        Debug.Log("Die");
        gameOverUI.GetComponent<GameOver>().Active();
    }

    public void AddGold(int num)
    {
        playerStatus.gold += num;
        goldText.SetText(playerStatus.gold.ToString());
    }

    public void AddExp(int num)
    {
        playerStatus.AddExp(num);
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.SetMaxHealth(playerStatus.maxHealth);
        healthBar.SetHealth(playerStatus.health);
        expBar.SetLevel(playerStatus.level);
        expBar.SetMaxExp(playerStatus.maxExp);
        expBar.SetExp(playerStatus.currentExp);
        goldText.SetText(playerStatus.gold.ToString());
    }
}
