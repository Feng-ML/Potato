using DG.Tweening;
using System;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerDirection;              //与玩家的向量
    private float ideaTime;                       //站立时间
    private float actionRadius = 1.5f;            //活动半径

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
            //移动
            transform.position += playerDirection.normalized * 10 * Time.deltaTime;
            ideaTime = 0;
        }
        else
        {
            //站立
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

        // 圆形范围随机位置
        Vector2 targetPosition = GetRandomPos();

        // 移动物体到目标位置
        transform.DOMove(targetPosition, 1);
    }

    private Vector2 GetRandomPos()
    {
        Vector2 playerPosition = player.transform.position;

        // 生成随机角度
        float randomAngle = UnityEngine.Random.Range(0f, 360f);
        // 将角度转换为弧度
        float radians = randomAngle * Mathf.Deg2Rad;
        // 计算随机点的坐标
        float x = playerPosition.x + actionRadius * Mathf.Cos(radians);
        float y = playerPosition.y + actionRadius * Mathf.Sin(radians);

        return new Vector2(x, y);
    }
}
