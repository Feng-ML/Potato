using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 forward;
    public int speed;
    public int attack = 5;
    public Action releaseAction;       //���յ�����ط���
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        forward = transform.right.normalized;
        rb.velocity = forward * speed;
        //transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }

    public void SetDeactiveAction(Action release)
    {
        releaseAction = release;
    }
}
