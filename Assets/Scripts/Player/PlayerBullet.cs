using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public int maxPenetration = 1;     //����ͨ����
    private int curPenetration;        //��ǰʣ���ͨ����
    public float repelPower;           //���˳̶�

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
        else
        {
            releaseAction.Invoke();
        }
    }
}
