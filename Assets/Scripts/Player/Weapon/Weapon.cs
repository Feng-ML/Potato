using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected List<BulletPool> bulletPoolList;    //�ӵ����б�
    public Bag playerBag;
    public PlayerStatus playerStatus;
    private Animator weaponAnimator;
    public AudioClip fireAudio;

    public float attackRange = 6;               //������Χ
    private float realAttackRange;              //ʵ�ʹ�����Χ
    public float attackTime = 1;                //�������
    private float realAttackTime;               //ʵ�ʹ������
    public int bulletIndex;                     //�ӵ����� 

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
        realAttackRange = playerStatus.GetAttackRange(attackRange);  //������Χ����
        realAttackTime = playerStatus.GetAttackTime(attackTime);     //�����������
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

    //ָ���������
    private void LookAtNearEnemy()
    {
        // ��ȡ��Χ�ڵ�����enemy
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

            // ��תǹе
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
            // ��ԭ
            transform.localScale = new Vector3(flipY, flipY, 1);
            transform.localEulerAngles = new Vector3(rotaY, rotaY, 1);
        }
    }

    //����
    protected virtual void Fire()
    {
        Transform Muzzle = transform.Find("Muzzle");    //ǹ��
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
