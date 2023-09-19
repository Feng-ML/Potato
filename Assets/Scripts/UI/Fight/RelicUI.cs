using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicUI : MonoBehaviour
{
    public Bag bag;
    public Relic[] relicList;          //�������б�

    private List<Relic> relicSelectList = new List<Relic>();    //��ѡ�������б�

    private void OnEnable()
    {
        // ���3����ͬ�������
        var randomNumbers = new HashSet<int>();
        while (randomNumbers.Count < 3)
        {
            int randomNumber = UnityEngine.Random.Range(0, relicList.Length);
            randomNumbers.Add(randomNumber);
        }
        var numList = randomNumbers.ToArray();

        relicSelectList.Clear();
        var RelicUIList = transform.GetComponentsInChildren<Transform>().Where(t => t.name == "RelicItem").ToArray();
        for (int i = 0; i < 3; i++)
        {
            Relic relicItem = relicList[numList[i]];
            relicSelectList.Add(relicItem);
            RelicUIList[i].Find("RelicImg").GetComponent<Image>().sprite = relicItem.GetComponent<SpriteRenderer>().sprite;
            RelicUIList[i].Find("RelicName").GetComponent<TMP_Text>().text = relicItem.relicName;
            RelicUIList[i].Find("RelicInfo").GetComponent<TMP_Text>().text = relicItem.relicInfo;
        }
    }

    public void RelicSelect(int index)
    {
        gameObject.SetActive(false);
        Relic relicItem = relicSelectList[index];
        bag.relicsList.Add(relicItem);
        relicItem.OnGetRelic();
        Time.timeScale = 1;
    }
}
