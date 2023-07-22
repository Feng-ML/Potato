using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerStatus;
    private Vector3 playerDirection;
    private bool isGoing;
    private float speed = 5;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerStatus = PlayerManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = player.transform.position - transform.position;

        if (playerDirection.magnitude < 2f)
        {
            isGoing = true;
        }

        if (isGoing)
        {
            playerDirection.Normalize();
            transform.position += playerDirection * speed * Time.deltaTime;
            speed += Time.deltaTime;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerStatus.AddGold(1);
            playerStatus.AddExp(20);
            Destroy(gameObject);
        }
    }
}
