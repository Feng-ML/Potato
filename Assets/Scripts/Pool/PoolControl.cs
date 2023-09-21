using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    //单例
    private static PoolControl instance;
    public static PoolControl Instance { get => instance; set => instance = value; }

    public EnemyControl[] enemyList;  //敌人列表
    [HideInInspector]
    public List<EnemyPool> enemyPool = new List<EnemyPool>();  //敌人对象池列表

    public Bullet[] bulletList;  //子弹列表
    [HideInInspector]
    public List<BulletPool> bulletPool = new List<BulletPool>();  //子弹对象池列表

    public Bullet[] enemyBulletList;  //怪物子弹列表
    [HideInInspector]
    public List<BulletPool> enemyBulletPool = new List<BulletPool>();  //怪物子弹对象池列表

    public Explosion[] explosionList;  //爆炸列表
    [HideInInspector]
    public List<ExplosionPool> explosionPool = new List<ExplosionPool>();  //爆炸对象池列表

    private void Awake()
    {
        Instance = this;
        SetEnemyPool();
        SetBulletPool();
        SetEnemyBulletPool();
        SetExplosionPool();
    }

    private void SetEnemyPool()
    {
        foreach (var item in enemyList)
        {
            var poolHolder = new GameObject($"pool: {item.name}");
            poolHolder.transform.parent = transform.Find("pool: enemy");
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
            poolHolder.transform.parent = transform.Find("pool: bullet");
            poolHolder.transform.position = transform.position;
            poolHolder.SetActive(false);

            var pool = poolHolder.AddComponent<BulletPool>();
            pool.SetPrefab(item);
            poolHolder.SetActive(true);
            bulletPool.Add(pool);
        }
    }

    private void SetEnemyBulletPool()
    {
        foreach (var item in enemyBulletList)
        {
            var poolHolder = new GameObject($"pool: {item.name}");
            poolHolder.transform.parent = transform.Find("pool: bullet");
            poolHolder.transform.position = transform.position;
            poolHolder.SetActive(false);

            var pool = poolHolder.AddComponent<BulletPool>();
            pool.SetPrefab(item);
            poolHolder.SetActive(true);
            enemyBulletPool.Add(pool);
        }
    }

    private void SetExplosionPool()
    {
        foreach (var item in explosionList)
        {
            var poolHolder = new GameObject($"pool: {item.name}");
            poolHolder.transform.parent = transform.Find("pool: explosion");
            poolHolder.transform.position = transform.position;
            poolHolder.SetActive(false);

            var pool = poolHolder.AddComponent<ExplosionPool>();
            pool.SetPrefab(item);
            poolHolder.SetActive(true);
            explosionPool.Add(pool);
        }
    }
}
