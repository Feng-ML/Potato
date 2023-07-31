using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public GameObject bullent;
    private List<BulletPool> bulletPoolList;    //子弹池列表
    private Animator weaponAnimator;

    private float attackRange = 6f;     //攻击范围
    private bool hasEnemy;
    public LayerMask enemyLayer;

    private float timer;
    private float flipY;
    private float rotaY;

    private void Start()
    {
        flipY = transform.localScale.y;
        rotaY = transform.localEulerAngles.y;
        weaponAnimator = GetComponent<Animator>();
        bulletPoolList = PoolControl.Instance.bulletPool;
    }

    private void Update()
    {
        if (hasEnemy)
        {
            if (timer >= 0.3f)
            {
                Fire();
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        LookAtNearEnemy();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    //指向最近敌人
    private void LookAtNearEnemy()
    {
        // 获取范围内的所有enemy
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        float shortDirection = Mathf.Infinity;
        GameObject nearEnemy = null;

        foreach (Collider2D collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < shortDirection)
            {
                shortDirection = distance;
                nearEnemy = collider.gameObject;
            }
        }

        if (nearEnemy)
        {
            hasEnemy = true;
            Vector3 enemyPosition = nearEnemy.transform.position;
            Vector2 dir = (enemyPosition - transform.position).normalized;
            transform.right = dir;

            // 翻转枪械
            if (enemyPosition.x < transform.position.x)
            {
                transform.localScale = new Vector3(flipY, -flipY, 1);
            }
            else
            {
                transform.localScale = new Vector3(flipY, flipY, 1);
            }
        }
        else
        {
            hasEnemy = false;
            // 复原
            transform.localScale = new Vector3(flipY, flipY, 1);
            transform.localEulerAngles = new Vector3(rotaY, rotaY, 1);
        }
    }

    //开火
    private void Fire()
    {
        Transform Muzzle = transform.Find("Muzzle");    //枪口
        weaponAnimator.SetTrigger("fire");

        var bullentIns = bulletPoolList[0].Get();
        bullentIns.transform.position = Muzzle.position;
        bullentIns.transform.rotation = transform.rotation;
        bullentIns.GetComponent<Bullet>().attack = 5;
    }
}
