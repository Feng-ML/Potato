using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : EnemyControl
{
    private float attackTime;
    private bool isFlee;            //�Ƿ�Զ�봥��

    protected override void Awake()
    {
        base.Awake();
        forward = GetRandomV2();
    }

    protected override void Update()
    {
        base.Update();
        //����
        if (attackTime > 3)
        {
            Attack();
            attackTime = 0;
            isFlee = false;
        }
        else
        {
            attackTime += Time.deltaTime;
        }
        //Զ��
        if (playerDirection.magnitude < 5 && isFlee == false)
        {
            isFlee = true;
            forward = (transform.position - player.transform.position).normalized;
        }
    }

    private void Attack()
    {
        var bullet = PoolControl.Instance.bulletPool[1].Get();
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
        rb.velocity = forward * moveSpeed;
        //transform.position += (Vector3)forward * moveSpeed * Time.deltaTime;
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        // ײǽ����
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.tag == "Wall")
        {
            //��������淴���ǶȲ�ͬ
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
