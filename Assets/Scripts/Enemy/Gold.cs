using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;
    public PlayerStatus playerStatus;
    public AudioClip gainAudio;             // ∞»°“Ù–ß
    private Vector3 playerDirection;
    private bool isGoing;
    private float speed = 5;
    private float pickUpRange;      // ∞»°∑∂Œß

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerManager = PlayerManager.Instance;
    }

    private void OnEnable()
    {
        pickUpRange = playerStatus.GetPickUpRange(2);
    }

    void Update()
    {
        playerDirection = player.transform.position - transform.position;

        if (playerDirection.magnitude <= pickUpRange)
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

    private void OnDisable()
    {
        isGoing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(gainAudio, transform.position);
            playerManager.AddGold(5);
            playerManager.AddExp(20);
            GoldPool.Instance.Release(this);
        }
    }
}
