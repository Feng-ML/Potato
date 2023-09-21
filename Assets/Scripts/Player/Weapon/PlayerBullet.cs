using UnityEngine;

public class PlayerBullet : Bullet
{
    public int maxPenetration = 1;     //����ͨ����
    protected int curPenetration;      //��ǰʣ���ͨ����
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

                if (playerStatus.isAttackWithPoison) enemy.Poison(attack);

                if (curPenetration <= 0)
                {
                    explosion();
                    releaseAction.Invoke();
                }
            }
        }
        else
        {
            explosion();
            releaseAction.Invoke();
        }
    }

    private void explosion()
    {
        var epsIns = PoolControl.Instance.explosionPool[0].Get();
        epsIns.transform.position = transform.position;
    }
}
