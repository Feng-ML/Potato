using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public GameStatus gameStatus;           //��Ϸ״̬
    public GameObject bornAnimation;        //��������
    private List<EnemyPool> enemyPool;      //���˶�����б�
    public int bornNum = 10;                //���ɵ�������
    public float gameTime = 30;             //��Ϸʱ��
    public float bornTimer = 3;             //���ɵ��˼��
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
        waveText.text = "��" + gameStatus.wave + "��";
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
            //ʷ��ķ
            enemyInstance.level = 0;
            enemyInstance.transform.localScale = new Vector2(10, 10);
        }
    }

    //Boss����ʱ��
    private void BossBorn()
    {
        if (gameStatus.wave == 3)
        {
            Instantiate(bossList[0], new Vector2(8, -4), Quaternion.identity);
        }
    }
}
