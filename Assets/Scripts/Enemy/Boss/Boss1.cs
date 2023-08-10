using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boos1 : EnemyControl
{
    private float attackTime;
    private bool isFlee;            //�Ƿ�Զ�봥��
    public Transform healthBar;     //Ѫ��
    private Slider healthSlider;    //��ǰѪ����ʾ

    protected override void Awake()
    {
        base.Awake();
        forward = GetForwardV2();

        healthSlider = healthBar.GetComponent<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    protected override void Update()
    {
        playerDirection = player.transform.position - transform.position;
        //����
        if (attackTime > 3)
        {
            Attack();
            attackTime -= 3;
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
        animator.SetTrigger("attack");
    }

    private Vector2 GetForwardV2()
    {
        int x = 0, y = 0;
        while (x == 0 && y == 0)
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
        }
        return new Vector2(x, y);
    }

    protected override void Move()
    {
        rb.velocity = forward * moveSpeed;
        // ת��
        if (forward.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (forward.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthSlider.value = currenthealth;
    }

    protected override void Die()
    {
        Debug.Log("Boss die");
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Wall")
        {
            forward = new Vector2(-forward.x, -forward.y);
        }
    }
}
