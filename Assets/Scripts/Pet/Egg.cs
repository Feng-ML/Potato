using DG.Tweening;
using System;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerDirection;              //����ҵ�����
    private float ideaTime;                       //վ��ʱ��
    private float actionRadius = 1.5f;            //��뾶

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        playerDirection = player.transform.position - transform.position;
        Move();
    }

    private void Move()
    {
        if (playerDirection.magnitude > actionRadius)
        {
            //�ƶ�
            transform.position += playerDirection.normalized * 10 * Time.deltaTime;
            ideaTime = 0;
        }
        else
        {
            //վ��
            ideaTime += Time.deltaTime;
            if (ideaTime > 3)
            {
                ideaTime = 0;
                Walk();
            }
        }
    }

    private void Walk()
    {
        Vector2 playerPosition = player.transform.position;

        // Բ�η�Χ���λ��
        Vector2 targetPosition = GetRandomPos();

        // �ƶ����嵽Ŀ��λ��
        transform.DOMove(targetPosition, 1);
    }

    private Vector2 GetRandomPos()
    {
        Vector2 playerPosition = player.transform.position;

        // ��������Ƕ�
        float randomAngle = UnityEngine.Random.Range(0f, 360f);
        // ���Ƕ�ת��Ϊ����
        float radians = randomAngle * Mathf.Deg2Rad;
        // ��������������
        float x = playerPosition.x + actionRadius * Mathf.Cos(radians);
        float y = playerPosition.y + actionRadius * Mathf.Sin(radians);

        return new Vector2(x, y);
    }
}
