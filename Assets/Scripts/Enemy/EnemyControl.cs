using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float moveSpeed = 2;     //�ƶ��ٶ�
    public int attack = 1;    //������
    public float health = 100;      //����ֵ
    public GameObject diamond;      //���䱦ʯ
    private float stayTime;     //��������Ƶ��


    private Animator animator;
    private GameObject player;
    private Vector3 playerDirection;  //��������ҵ�����

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Crash(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
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

    private void OnTriggerExit2D(Collider2D collision)
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
    private void Move()
    {
        playerDirection = player.transform.position - transform.position;

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
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    //����
    public void ApplyKnockback(float backPower)
    {
        transform.position -= playerDirection.normalized * backPower;
    }

    // ����
    private void Die()
    {
        Destroy(gameObject);
        Instantiate(diamond, transform.position, Quaternion.identity);  //����
    }
}
