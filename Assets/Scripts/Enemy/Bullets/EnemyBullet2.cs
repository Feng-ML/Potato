using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet2 : Bullet
{
    private GameObject player;
    private Vector2 playerDirection;              //与玩家的向量

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Update()
    {
        playerDirection = player.transform.position - transform.position;
        if (playerDirection.magnitude > 3)
        {
            //插值转向
            Vector3 lerpedVector = Vector3.Lerp(forward, playerDirection.normalized, 10 * Time.deltaTime);
            forward = lerpedVector.normalized;
        }
        rb.velocity = forward * speed;
    }

    private void OnEnable()
    {
        StartCoroutine(HideBullet());
    }

    private IEnumerator HideBullet()
    {
        yield return new WaitForSeconds(4);
        if (gameObject.activeSelf) releaseAction.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
