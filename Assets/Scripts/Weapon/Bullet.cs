using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 forward;
    public int speed;
    public int attack = 5;
    public int maxPenetration = 1;     //最大贯通个数
    private int curPenetration;        //当前剩余贯通个数
    public float repelPower;           //击退程度
    public Action releaseAction;       //回收到对象池方法
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        forward = transform.right.normalized;
        rb.velocity = forward * speed;
        //transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }

    private void OnEnable()
    {
        curPenetration = maxPenetration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (curPenetration > 0)
            {
                curPenetration--;
                var enemy = collision.gameObject.GetComponent<EnemyControl>();
                enemy.TakeDamage(attack);
                enemy.ApplyKnockback(transform, repelPower);
            }
            if (curPenetration <= 0) releaseAction.Invoke();
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(attack);
            releaseAction.Invoke();
        }
        else
        {
            releaseAction.Invoke();
        }
    }

    public void SetDeactiveAction(Action release)
    {
        releaseAction = release;
    }
}
