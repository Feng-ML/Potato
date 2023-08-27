using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected List<BulletPool> bulletPoolList;    //子弹池列表
    public Bag playerBag;
    public PlayerStatus playerStatus;
    private Animator weaponAnimator;
    public AudioClip fireAudio;

    public float attackRange = 6;               //攻击范围
    private float realAttackRange;              //实际攻击范围
    public float attackTime = 1;                //攻击间隔
    private float realAttackTime;               //实际攻击间隔
    public int bulletIndex;                     //子弹种类 

    private bool hasEnemy;
    private float timer;
    private float flipY;
    private float rotaY;

    private void Start()
    {
        flipY = transform.localScale.y;
        rotaY = transform.localEulerAngles.y;
        weaponAnimator = GetComponent<Animator>();
        bulletPoolList = PoolControl.Instance.bulletPool;
        realAttackRange = playerStatus.GetAttackRange(attackRange);  //攻击范围计算
        realAttackTime = playerStatus.GetAttackTime(attackTime);     //攻击间隔计算
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Fire();
        //}
        if (hasEnemy)
        {
            if (timer >= realAttackTime)
            {
                Fire();
                timer -= realAttackTime;
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
        Gizmos.DrawWireSphere(transform.position, realAttackRange);
    }

    //指向最近敌人
    private void LookAtNearEnemy()
    {
        // 获取范围内的所有enemy
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, realAttackRange, LayerMask.GetMask("Enemy"));

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
    protected virtual void Fire()
    {
        Transform Muzzle = transform.Find("Muzzle");    //枪口
        weaponAnimator.SetTrigger("fire");
        AudioSource.PlayClipAtPoint(fireAudio, Muzzle.position, .7f);

        var bullentIns = bulletPoolList[bulletIndex].Get();
        bullentIns.transform.position = Muzzle.position;
        bullentIns.transform.rotation = transform.rotation;

        playerBag.relicsList.ForEach(item =>
        {
            item.OnAttack(transform.rotation, Muzzle.position, bulletIndex);
        });
    }
}
