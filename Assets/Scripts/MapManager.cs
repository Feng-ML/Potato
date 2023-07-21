using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MapManager : MonoBehaviour
{
    public GameObject bornAnimation;    //出生动画

    public GameObject[] EnemyList;
    private float bornTimer;        //生成敌人间隔
    private int bornNum = 3;        //生成敌人数量

    void Start()
    {

    }

    void Update()
    {

        if (bornTimer > 3)
        {
            for (int i = 0; i < bornNum; i++)
            {
                float x = UnityEngine.Random.Range(-7, 7);
                float y = UnityEngine.Random.Range(-4, 4);
                Born(EnemyList[0], new Vector2(x, y));
            }
            bornTimer = 0;
        }
        else
        {
            bornTimer += Time.deltaTime;
        }
    }

    async void Born(GameObject enemy, Vector2 position)
    {
        GameObject fork = Instantiate(bornAnimation, position, Quaternion.identity);
        await Task.Delay(TimeSpan.FromSeconds(3));
        if (fork) Destroy(fork);

        Instantiate(enemy, position, Quaternion.identity);

    }
}
