using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int level;
    public float moveSpeed = 2;                   //�ƶ��ٶ�
    public int attack = 1;                        //������
    public float maxHealth;                       //�������ֵ
    public float currenthealth;                   //��ǰ����ֵ
    private float stayTime;                       //��������Ƶ��

    protected Action releaseAction;               //���յ�����ط���
    protected Func<EnemyControl> getAction;       //�Ӷ���ػ�ȡ���󷽷�

    private Animator animator;
    protected GameObject player;
    protected Vector3 playerDirection;              //��������ҵ�����

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        currenthealth = maxHealth;
    }

    protected virtual void Update()
    {
        playerDirection = player.transform.position - transform.position;
        Move();
    }

    protected virtual void OnEnable()
    {
        currenthealth = maxHealth;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Crash(collision);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (stayTime > 1)
        {
            Crash(collision);
            stayTime = 0;
        }
        else
        {
            stayTime += Time.deltaTime;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        stayTime = 0;
    }

    //��ײ
    private void Crash(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        switch (other.tag)
        {
            case "Player":
                // ��ȡĿ�������״̬
                PlayerManager target = collision.gameObject.GetComponent<PlayerManager>();
                if (target)
                {
                    target.TakeDamage(attack);
                }
                break;
            default:
                break;
        }
    }

    //�ƶ�
    protected virtual void Move()
    {
        if (playerDirection.magnitude > 0.5f)
        {
            animator.SetBool("isMoving", true);
            playerDirection.Normalize();

            // ת��
            if (playerDirection.x > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (playerDirection.x < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

            }
            //�ƶ�
            transform.position += playerDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            //animator.SetBool("isMoving", false);
            transform.position += Vector3.forward;
        }
    }

    //����
    public void TakeDamage(float damage)
    {
        if (currenthealth < damage)
        {
            currenthealth = 0;
            Die();
        }
        else
        {
            currenthealth -= damage;
        }
    }

    //����
    public void ApplyKnockback(float backPower)
    {
        transform.position -= playerDirection.normalized * backPower;
    }

    // ����
    protected virtual void Die()
    {
        releaseAction.Invoke();
        //����
        var gold = GoldPool.Instance.Get();
        gold.transform.position = transform.position;
    }

    public void SetDeactiveAction(Action releaseObj)
    {
        releaseAction = releaseObj;
    }

    public void SetActiveAction(Func<EnemyControl> getObj)
    {
        getAction = getObj;
    }
}
