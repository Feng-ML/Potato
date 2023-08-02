using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 forward;
    public int speed;
    public int attack;
    public float repelPower;          //����Ч��
    private Action releaseAction;   //���յ�����ط���
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        forward = transform.right.normalized;
    }

    private void Update()
    {
        rb.velocity = forward * speed;
        //transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        releaseAction.Invoke();
        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<EnemyControl>();
            enemy.TakeDamage(attack);
            enemy.ApplyKnockback(transform, repelPower);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(attack);
        }
    }

    public void SetDeactiveAction(Action release)
    {
        releaseAction = release;
    }
}
