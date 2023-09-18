using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicUI : MonoBehaviour
{
    public Relic[] relicList;      //遗物列表

    private void OnEnable()
    {
        // 获得3个不同的随机数
        var randomNumbers = new HashSet<int>();
        while (randomNumbers.Count < 3)
        {
            int randomNumber = UnityEngine.Random.Range(0, relicList.Length);
            randomNumbers.Add(randomNumber);
        }
        var numList = randomNumbers.ToArray();

        var RelicUIList = transform.GetComponentsInChildren<Transform>().Where(t => t.name == "RelicItem").ToArray();
        for (int i = 0; i < 3; i++)
        {
            RelicUIList[i].Find("RelicImg").GetComponent<Image>().sprite = relicList[numList[i]].GetComponent<SpriteRenderer>().sprite;
            RelicUIList[i].Find("RelicName").GetComponent<TMP_Text>().text = relicList[numList[i]].relicName;
            RelicUIList[i].Find("RelicInfo").GetComponent<TMP_Text>().text = relicList[numList[i]].relicInfo;
        }
    }
}
