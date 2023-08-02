using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public GameObject bornAnimation;    //出生动画

    private List<EnemyPool> enemyPool;    //敌人对象池列表

    private float bornTimer;        //生成敌人间隔
    public int bornNum = 10;        //生成敌人数量

    public float gameTime = 30;
    public TMP_Text gameTimeText;
    public TMP_Text waveText;

    void Start()
    {
        enemyPool = PoolControl.Instance.enemyPool;
        bornTimer = 3;
    }

    void Update()
    {

        if (bornTimer > 3)
        {
            for (int i = 0; i < bornNum; i++)
            {
                float x = UnityEngine.Random.Range(-16, 16);
                float y = UnityEngine.Random.Range(-7, 7);
                var enemyIndex = UnityEngine.Random.Range(0, 3);
                StartCoroutine(Born(enemyIndex, new Vector2(x, y)));
            }
            bornTimer = 0;
        }
        else
        {
            bornTimer += Time.deltaTime;
        }

        if (gameTime <= 0)
        {
            SceneManager.LoadScene("shop");
        }
        else
        {
            gameTime -= Time.deltaTime;
            gameTimeText.text = ((int)gameTime).ToString();
        }
    }

    private IEnumerator Born(int enemyIndex, Vector2 position)
    {
        GameObject fork = Instantiate(bornAnimation, position, Quaternion.identity);

        yield return new WaitForSeconds(3);

        if (fork) Destroy(fork);
        var enemyInstance = enemyPool[enemyIndex].Get();
        enemyInstance.transform.position = position;
        if (enemyIndex == 1)
        {
            //史莱姆
            enemyInstance.level = 0;
            enemyInstance.transform.localScale = new Vector2(10, 10);
        }
    }
}
