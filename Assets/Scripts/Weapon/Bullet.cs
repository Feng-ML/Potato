using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed;
    public int attack;


    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControl>().TakeDamage(attack);
        }
    }
}
