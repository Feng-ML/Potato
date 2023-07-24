using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading;

public class MapManager : MonoBehaviour
{
    public GameObject bornAnimation;    //出生动画

    public GameObject[] EnemyList;
    private float bornTimer;        //生成敌人间隔
    private int bornNum = 3;        //生成敌人数量

    public float gameTime = 30;
    public TMP_Text gameTimeText;

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

    async void Born(GameObject enemy, Vector2 position)
    {
        GameObject fork = Instantiate(bornAnimation, position, Quaternion.identity);
        var ctsInfo = TaskPool.CreatCts();
        await Task.Delay(TimeSpan.FromSeconds(3), ctsInfo.cts.Token);

        if (fork) Destroy(fork);
        Instantiate(enemy, position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        TaskPool.CancelAllTask();
    }
}
