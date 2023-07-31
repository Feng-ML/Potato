using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    //单例
    private static PoolControl instance;
    public static PoolControl Instance { get => instance; set => instance = value; }

    public EnemyControl[] enemyList;                             //敌人列表
    public List<EnemyPool> enemyPool = new List<EnemyPool>();    //敌人对象池列表

    public Bullet[] bulletList;                                  //子弹列表
    public List<BulletPool> bulletPool = new List<BulletPool>();        //子弹对象池列表

    private void Awake()
    {
        Instance = this;
        SetEnemyPool();
        SetBulletPool();
    }

    private void SetEnemyPool()
    {
        foreach (var item in enemyList)
        {
            var poolHolder = new GameObject($"pool: {item.name}");
            poolHolder.transform.parent = transform;
            poolHolder.transform.position = transform.position;
            poolHolder.SetActive(false);

            var pool = poolHolder.AddComponent<EnemyPool>();
            pool.SetPrefab(item);
            poolHolder.SetActive(true);
            enemyPool.Add(pool);
        }
    }

    private void SetBulletPool()
    {
        foreach (var item in bulletList)
        {
            var poolHolder = new GameObject($"pool: {item.name}");
            poolHolder.transform.parent = transform;
            poolHolder.transform.position = transform.position;
            poolHolder.SetActive(false);

            var pool = poolHolder.AddComponent<BulletPool>();
            pool.SetPrefab(item);
            poolHolder.SetActive(true);
            bulletPool.Add(pool);
        }
    }


}
