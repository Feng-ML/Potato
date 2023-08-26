using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerBullet2 : PlayerBullet
{
    public int explosionRange;      //��ը��Χ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (curPenetration > 0)
            {
                curPenetration--;
                // ��ȡ��Χ�ڵ�����enemy
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange, LayerMask.GetMask("Enemy"));

                foreach (var item in colliders)
                {
                    var enemy = item.gameObject.GetComponent<EnemyControl>();

                    // ����
                    bool isCritical = false;
                    var damage = playerStatus.GetDamageNum(attack, ref isCritical);

                    enemy.TakeDamage(damage, isCritical);
                    enemy.ApplyKnockback(transform, repelPower);
                }

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
        var epsIns = PoolControl.Instance.explosionPool[1].Get();
        epsIns.transform.position = transform.position;
        SetSpriteSize(epsIns);
    }

    // ���ñ�ը Sprite �Ĵ�С
    public void SetSpriteSize(Explosion epsIns)
    {
        var sprite = epsIns.GetComponent<SpriteRenderer>();
        sprite.size = new Vector2(explosionRange, explosionRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
