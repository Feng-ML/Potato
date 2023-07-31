using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : EnemyControl
{
    public Vector2 forward;
    //private Rigidbody2D rb;

    private void Awake()
    {
        forward = GetForwardV2();
        //rb = GetComponent<Rigidbody2D>();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        forward = GetForwardV2();
    }

    private Vector2 GetForwardV2()
    {
        int x = 0, y = 0;
        while (x == 0 && y == 0)
        {
            x = Random.Range(-1, 2);
            y = Random.Range(-1, 2);
        }
        return new Vector2(x, y);
    }

    protected override void Move()
    {
        //rb.velocity = forward * moveSpeed;
        transform.position += (Vector3)forward * moveSpeed * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Wall")
        {
            forward = new Vector2(-forward.x, -forward.y);
        }
    }
}
