using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ӳ�׷���ӵ�
public class EnemyBullet_Track : EnemyBullet
{
    private GameObject player;
    private Vector2 playerDirection;              //����ҵ�����

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(HideBullet());

        SetDeactiveAction(() => Destroy(gameObject));
    }

    protected override void Update()
    {
        playerDirection = player.transform.position - transform.position;
        if (playerDirection.magnitude > 3)
        {
            //��ֵת��
            Vector3 lerpedVector = Vector3.Lerp(forward, playerDirection.normalized, 10 * Time.deltaTime);
            forward = lerpedVector.normalized;
        }
        rb.velocity = forward * speed;
    }

    private IEnumerator HideBullet()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
