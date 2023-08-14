using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boos1 : EnemyControl
{
    private float attackTime;
    public Transform healthBar;     //血条
    private Slider healthSlider;    //当前血量显示

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
        //攻击
        if (!isDeath)
        {
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
        gameObject.layer = 0;
        gameObject.tag = "Untagged";
        animator.SetBool("isDeath", true);
        isDeath = true;
    }

    private void DieAnimationOver()
    {
        gameObject.SetActive(false);
    }
}
