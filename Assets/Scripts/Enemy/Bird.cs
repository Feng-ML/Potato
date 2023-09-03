using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : EnemyControl
{
    private float attackTime;

    protected override void Awake()
    {
        base.Awake();
        forward = GetRandomV2();
    }

    protected override void Update()
    {
        base.Update();
        //攻击
        if (attackTime > 3)
        {
            Attack();
            attackTime = 0;
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }

    private void Attack()
    {
        var bullet = PoolControl.Instance.enemyBulletPool[0].Get();
        bullet.transform.position = transform.position;
        bullet.transform.right = playerDirection;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        forward = GetRandomV2();
    }

    protected override void Move()
    {
        //远离
        if (playerDirection.magnitude < 5)
        {
            forward = (transform.position - player.transform.position).normalized;
        }
        rb.velocity = forward * moveSpeed;
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        // 撞墙反弹
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.tag == "Wall")
        {
            //竖面与横面反弹角度不同
            if (collision.name == "airWallX")
            {
                forward = new Vector2(forward.x, -forward.y);
            }
            else
            {
                forward = new Vector2(-forward.x, forward.y);
            }
        }
    }
}
