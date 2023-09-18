using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public GameStatus gameStatus;           //游戏状态
    public GameObject bornAnimation;        //出生动画
    private List<EnemyPool> enemyPool;      //敌人对象池列表
    public int bornNum = 10;                //生成敌人数量
    public float gameTime = 30;             //游戏时间
    public float bornTimer = 3;             //生成敌人间隔
    private float bornTime;
    public GameObject[] bossList;           //Boss

    public TMP_Text gameTimeText;
    public TMP_Text waveText;
    public GameObject gameOverUI;
    private AudioSource BGM;

    private void Awake()
    {
        BGM = gameObject.GetComponent<AudioSource>();
        bornTime = bornTimer;
    }

    void Start()
    {
        waveText.text = "第" + gameStatus.wave + "波";
        enemyPool = PoolControl.Instance.enemyPool;

        BossBorn();
    }

    void Update()
    {
        bornTime += Time.deltaTime;
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            if (gameStatus.wave >= 5)
            {
                BGM.Stop();
                gameOverUI.GetComponent<GameOver>().Active(true);
            }
            else
            {
                SceneManager.LoadScene("shop");
            }
        }
        else
        {
            gameTimeText.text = ((int)gameTime).ToString();
            gameTimeText.color = gameTime <= 10 ? Color.red : Color.white;
        }

        if (bornTime > bornTimer)
        {
            for (int i = 0; i < bornNum; i++)
            {
                float x = UnityEngine.Random.Range(-16, 16);
                float y = UnityEngine.Random.Range(-7, 7);
                var enemyIndex = UnityEngine.Random.Range(0, 3);
                StartCoroutine(Born(enemyIndex, new Vector2(x, y)));
            }
            bornTime = 0;
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

    //Boss出现时机
    private void BossBorn()
    {
        if (gameStatus.wave == 3)
        {
            Instantiate(bossList[0], new Vector2(8, -4), Quaternion.identity);
        }
    }
}
