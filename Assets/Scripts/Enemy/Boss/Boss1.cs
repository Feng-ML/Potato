using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boos1 : EnemyControl
{
    private float attackTime;
    public Transform healthBar;     //Ѫ��
    private Slider healthSlider;    //��ǰѪ����ʾ
    public GameObject bullet;

    protected override void Awake()
    {
        base.Awake();
        //forward = GetRandomV2();

        healthSlider = healthBar.GetComponent<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    protected override void Update()
    {
        base.Update();
        //����
        if (attackTime > 3)
        {
            Attack();
            attackTime -= 3;
        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        var bullet = PoolControl.Instance.bulletPool[2].Get();
        bullet.transform.position = transform.position;
        bullet.forward = forward;
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
        // ײǽ����
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
