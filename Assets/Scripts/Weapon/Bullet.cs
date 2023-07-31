using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;
    public int attack;
    private Action releaseAction;   //回收到对象池方法


    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.activeSelf)
        {
            releaseAction.Invoke();
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyControl>().TakeDamage(attack);
            }
        }
    }

    public void SetDeactiveAction(Action release)
    {
        releaseAction = release;
    }
}
