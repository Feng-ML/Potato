using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public int maxPenetration = 1;     //����ͨ����
    private int curPenetration;        //��ǰʣ���ͨ����
    public float repelPower;           //���˳̶�
    public PlayerStatus playerStatus;

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
                var damage = (int)(attack * (1 + playerStatus.attack / 100));      //�˺�����

                // ����
                var isCritical = false;
                var critical = playerStatus.criticalRate / 100;
                if (Random.value < critical)
                {
                    damage = (int)(damage * (100 + playerStatus.criticalDamage) / 100);
                    isCritical = true;
                }

                enemy.TakeDamage(damage, isCritical);
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
