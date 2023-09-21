using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    //����
    private static PoolControl instance;
    public static PoolControl Instance { get => instance; set => instance = value; }

    public EnemyControl[] enemyList;  //�����б�
    [HideInInspector]
    public List<EnemyPool> enemyPool = new List<EnemyPool>();  //���˶�����б�

    public Bullet[] bulletList;  //�ӵ��б�
    [HideInInspector]
    public List<BulletPool> bulletPool = new List<BulletPool>();  //�ӵ�������б�

    public Bullet[] enemyBulletList;  //�����ӵ��б�
    [HideInInspector]
    public List<BulletPool> enemyBulletPool = new List<BulletPool>();  //�����ӵ�������б�

    public Explosion[] explosionList;  //��ը�б�
    [HideInInspector]
    public List<ExplosionPool> explosionPool = new List<ExplosionPool>();  //��ը������б�

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
