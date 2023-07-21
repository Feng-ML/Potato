using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float moveSpeed = 2;     //移动速度
    public int attack = 1;    //攻击力
    public float health = 100;      //生命值
    public GameObject diamond;      //掉落宝石
    private float stayTime;     //攻击触发频率


    private Animator animator;
    private GameObject player;
    private Vector3 playerDirection;  //敌人与玩家的向量

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Crash(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (stayTime > 1)
        {
            Crash(collision);
            stayTime = 0;
        }
        else
        {
            stayTime += Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        stayTime = 0;
    }

    //碰撞
    private void Crash(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        switch (other.tag)
        {
            case "Player":
                // 获取目标物体的状态
                PlayerManager target = collision.gameObject.GetComponent<PlayerManager>();
                if (target)
                {
                    target.TakeDamage(attack);
                }
                break;
            default:
                break;
        }
    }

    //移动
    private void Move()
    {
        playerDirection = player.transform.position - transform.position;

        if (playerDirection.magnitude > 0.5f)
        {
            animator.SetBool("isMoving", true);
            playerDirection.Normalize();

            // 转向
            if (playerDirection.x > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (playerDirection.x < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            }
            //移动
            transform.position += playerDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            //animator.SetBool("isMoving", false);
            transform.position += Vector3.forward;
        }
    }

    //受伤
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    //击退
    public void ApplyKnockback(float backPower)
    {
        transform.position -= playerDirection.normalized * backPower;
    }

    // 死亡
    private void Die()
    {
        Destroy(gameObject);
        Instantiate(diamond, transform.position, Quaternion.identity);  //掉落
    }
}
