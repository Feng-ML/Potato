using System;
using System.Collections;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int level;
    public float moveSpeed = 2;                   //�ƶ��ٶ�
    public int attack = 1;                        //������
    public float maxHealth;                       //�������ֵ
    public float currenthealth;                   //��ǰ����ֵ
    private float crashTime;                      //��ײ�˺�����Ƶ��
    protected bool isHurt;
    protected bool isDeath;
    protected bool isSuperArmor;                  //����

    //[HideInInspector]
    public Vector2 forward;                       //ǰ������
    protected Action releaseAction;               //���յ�����ط���
    protected Func<EnemyControl> getAction;       //�Ӷ���ػ�ȡ���󷽷�

    protected Rigidbody2D rb;
    protected Animator animator;
    protected GameObject player;
    protected Vector3 playerDirection;              //��������ҵ�����

    protected virtual void Awake()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currenthealth = maxHealth;
    }

    protected virtual void OnEnable()
    {
        currenthealth = maxHealth;
        isDeath = false;
        isHurt = false;
    }

    protected virtual void Update()
    {
        playerDirection = player.transform.position - transform.position;

        if (isDeath || (isHurt && !isSuperArmor))
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            forward = playerDirection.normalized;
            Move();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Crash(collision);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (crashTime > 1)
        {
            Crash(collision);
            crashTime = 0;
        }
        else
        {
            crashTime += Time.deltaTime;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            crashTime = 0;
        }
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
            rb.velocity = forward * moveSpeed;
            //transform.position += playerDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            //animator.SetBool("isMoving", false);
            transform.position += Vector3.forward;
        }
    }

    //����
    public virtual void TakeDamage(int damage, bool isCritical = false)
    {
        if (isDeath) return;
        if (currenthealth <= damage)
        {
            currenthealth = 0;
            Die();
        }
        else
        {
            currenthealth -= damage;
        }

        var color = isCritical ? Color.yellow : Color.white;
        TextPool.Instance.GetText(transform.position, damage, color);
    }

    //����
    public void ApplyKnockback(Transform attackTrans, float backPower)
    {
        //transform.position -= playerDirection.normalized * backPower;
        if (currenthealth <= 0) return;
        var dir = (transform.position - attackTrans.position).normalized;
        StartCoroutine(Knockback(dir * backPower));
    }

    private IEnumerator Knockback(Vector3 power)
    {
        isHurt = true;
        rb.AddForce(power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        isHurt = false;
    }

    // ����
    protected virtual void Die()
    {
        isDeath = true;
        releaseAction?.Invoke();
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

    //��ȡ�������
    protected Vector2 GetRandomV2()
    {
        int x = 0, y = 0;
        while (x == 0 && y == 0)
        {
            x = UnityEngine.Random.Range(-1, 2);
            y = UnityEngine.Random.Range(-1, 2);
        }
        return new Vector2(x, y);
    }
}
