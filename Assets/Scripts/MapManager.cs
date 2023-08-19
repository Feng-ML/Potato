using QFramework;
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
    public float gameTime = 10;             //游戏时间
    public BindableProperty<float> bornTimer = new BindableProperty<float>(3);  //生成敌人间隔

    public TMP_Text gameTimeText;
    public TMP_Text waveText;

    void Start()
    {
        waveText.text = "第" + gameStatus.wave + "波";
        enemyPool = PoolControl.Instance.enemyPool;

        //监听敌人出生时间
        bornTimer.Register(newNum =>
        {
            if (newNum > 3)
            {
                for (int i = 0; i < bornNum; i++)
                {
                    float x = UnityEngine.Random.Range(-16, 16);
                    float y = UnityEngine.Random.Range(-7, 7);
                    var enemyIndex = UnityEngine.Random.Range(0, 3);
                    StartCoroutine(Born(0, new Vector2(x, y)));
                }
                bornTimer.Value = 0;
            }
        });
    }

    void Update()
    {
        bornTimer.Value += Time.deltaTime;
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            SceneManager.LoadScene("shop");
        }
        else
        {
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
