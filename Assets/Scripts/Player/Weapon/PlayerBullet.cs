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

                // ����
                bool isCritical = false;
                var damage = playerStatus.GetDamageNum(attack, ref isCritical);

                enemy.TakeDamage(damage, isCritical);
                enemy.ApplyKnockback(transform, repelPower);

                if (curPenetration <= 0) releaseAction.Invoke();
            }
        }
        else
        {
            releaseAction.Invoke();
        }
    }
}
