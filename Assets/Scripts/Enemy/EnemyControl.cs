using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int level;
    public float moveSpeed = 2;                   //移动速度
    public int attack = 1;                        //攻击力
    public float maxHealth;                       //最大生命值
    public float currenthealth;                   //当前生命值
    private float stayTime;                        //攻击触发频率
    private bool isHurt;

    //[HideInInspector]
    public Vector2 forward;                       //前进方向
    protected Action releaseAction;               //回收到对象池方法
    protected Func<EnemyControl> getAction;       //从对象池获取对象方法

    protected Rigidbody2D rb;
    protected Animator animator;
    protected GameObject player;
    protected Vector3 playerDirection;              //敌人与玩家的向量

    protected virtual void Awake()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        currenthealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        playerDirection = player.transform.position - transform.position;
        forward = playerDirection.normalized;
    }

    private void FixedUpdate()
    {
        if (!isHurt) Move();
    }

    protected virtual void OnEnable()
    {
        currenthealth = maxHealth;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Crash(collision);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
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

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stayTime = 0;
        }
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
    protected virtual void Move()
    {
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
            rb.velocity = forward * moveSpeed;
            //transform.position += playerDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            //animator.SetBool("isMoving", false);
            transform.position += Vector3.forward;
        }
    }

    //受伤
    public virtual void TakeDamage(int damage)
    {
        if (currenthealth <= damage)
        {
            currenthealth = 0;
            Die();
        }
        else
        {
            currenthealth -= damage;
        }

        TextPool.Instance.GetText(transform.position, damage, Color.white);
    }

    //击退
    public void ApplyKnockback(Transform attackTrans, float backPower)
    {
        //transform.position -= playerDirection.normalized * backPower;
        if (currenthealth <= 0) return;
        var dir = (transform.position - attackTrans.position).normalized;
        StartCoroutine(Knockback(dir * backPower));
    }

    private IEnumerator Knockback(Vector3 power)
    {
        isHurt = true;
        rb.AddForce(power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        isHurt = false;
    }


    // 死亡
    protected virtual void Die()
    {
        releaseAction?.Invoke();
        //掉落
        var gold = GoldPool.Instance.Get();
        gold.transform.position = transform.position;
    }

    public void SetDeactiveAction(Action releaseObj)
    {
        releaseAction = releaseObj;
    }

    public void SetActiveAction(Func<EnemyControl> getObj)
    {
        getAction = getObj;
    }

    //获取随机方向
    protected Vector2 GetRandomV2()
    {
        int x = 0, y = 0;
        while (x == 0 && y == 0)
        {
            x = UnityEngine.Random.Range(-1, 2);
            y = UnityEngine.Random.Range(-1, 2);
        }
        return new Vector2(x, y);
    }
}
