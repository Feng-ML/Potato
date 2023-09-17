using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boos1 : EnemyControl
{
    public Transform healthBar;     //Ѫ��
    public Bullet bulletPrefab;
    private float attackTime;
    private Slider healthSlider;    //��ǰѪ����ʾ

    protected override void Awake()
    {
        base.Awake();
        //forward = GetRandomV2();
        isSuperArmor = true;

        healthSlider = healthBar.GetComponent<Slider>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    protected override void Update()
    {
        base.Update();
        //����
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
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.forward = forward;
    }

    public override void TakeDamage(int damage, bool isCritical = false)
    {
        base.TakeDamage(damage, isCritical);
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
