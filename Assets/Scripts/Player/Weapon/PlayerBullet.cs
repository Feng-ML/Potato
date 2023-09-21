using UnityEngine;

public class PlayerBullet : Bullet
{
    public int maxPenetration = 1;     //最大贯通个数
    protected int curPenetration;      //当前剩余贯通个数
    public float repelPower;           //击退程度
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

                // 暴击
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
