using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class shopProduct : MonoBehaviour
{
    private VisualElement shopUI;
    public ShopProductList productList;     //所有宝物
    public PlayerStatus playerStatus;
    public Bag playerBag;

    public List<Item> currentProductList;     //当前商品列表
    private int surplusProductNum;      //剩余商品数
    private int refreshNum;     //刷新商店次数
    private int refreshPriceNum;     //刷新价格

    void Start()
    {
        shopUI = GetComponent<UIDocument>().rootVisualElement;
        RefreshShop();
        SetPlayerStatus();
        LoadPlayerBag();

        // 刷新商店点击事件
        shopUI.Q<Button>("refreshButton").clicked += RefreshShop;

        // 购买商品点击事件
        shopUI.Query("productItem").ForEach((item) =>
        {
            int index = int.Parse(item.Q<Button>().name.Split("-")[1]);
            item.Q<Button>().clicked += () => BuyProduct(index);
        });
    }


    // 刷新商店
    private void RefreshShop()
    {
        if (!Pay(refreshPriceNum)) return;

        currentProductList.Clear();
        // 获得4个不同的随机数
        var randomNumbers = new HashSet<int>();
        while (randomNumbers.Count < 4)
        {
            int randomNumber = UnityEngine.Random.Range(0, productList.itemsList.Count);
            randomNumbers.Add(randomNumber);
        }
        // 随机赋值商品
        foreach (int number in randomNumbers)
        {
            currentProductList.Add(productList.itemsList[number]);
            surplusProductNum++;
        }

        shopUI.Query("productItem").ForEach(item =>
        {
            item.style.visibility = Visibility.Visible;
            item.Q<Label>("productCost").style.color = Color.white;
        });

        // 价格计算赋值
        refreshNum++;
        refreshPriceNum = refreshNum * 5;

        SetProduct();
        SetPlayerStatus();
    }

    // 渲染商品 刷新按钮
    private void SetProduct()
    {
        for (int i = 0; i < 4; i++)
        {
            var item = shopUI.Query("productItem").AtIndex(i);
            item.Q("productImg").style.backgroundImage = currentProductList[i].itemImg;
            item.Q<Label>("productName").text = currentProductList[i].itemName;
            item.Q<Label>("productInfo").text = currentProductList[i].itemInfo;
            item.Q<Label>("productCost").text = currentProductList[i].cost.ToString();
            if (playerStatus.gold < currentProductList[i].cost)
            {
                item.Q<Label>("productCost").style.color = Color.red;
            }
        }

        shopUI.Q<Label>("refreshCost").text = "刷新 -" + refreshPriceNum;
        if (playerStatus.gold < refreshPriceNum)
        {
            shopUI.Q<Label>("refreshCost").style.color = Color.red;
        }
    }

    // 玩家状态赋值
    private void SetPlayerStatus()
    {
        shopUI.Q<Label>("maxHealthValue").text = playerStatus.maxHealth.ToString();
        shopUI.Q<Label>("healthRecoverValue").text = playerStatus.healthRecover.ToString();
        shopUI.Q<Label>("attackValue").text = playerStatus.attack.ToString();
        shopUI.Q<Label>("attackSpeedValue").text = playerStatus.attackSpeed.ToString();
        shopUI.Q<Label>("criticalRateValue").text = playerStatus.criticalRate.ToString();
        shopUI.Q<Label>("criticalDamageValue").text = playerStatus.criticalDamage.ToString();
        shopUI.Q<Label>("attackRangeValue").text = playerStatus.attackRange.ToString();
        shopUI.Q<Label>("armorValue").text = playerStatus.armor.ToString();
        shopUI.Q<Label>("dodgeRateValue").text = playerStatus.dodgeRate.ToString();
        shopUI.Q<Label>("speedValue").text = playerStatus.speed.ToString();
        shopUI.Q<Label>("pickUpRangeValue").text = playerStatus.pickUpRange.ToString();
        shopUI.Q<Label>("gold").text = playerStatus.gold.ToString();
    }

    //购买商品
    private void BuyProduct(int productIndex)
    {
        var item = currentProductList[productIndex];
        if (!Pay(item.cost)) return;


        var itemInx = playerBag.itemsList.FindIndex((i) => i.id == item.id);
        if (itemInx < 0)
        {
            // 不存在宝物
            playerBag.itemsList.Add(item);
            playerBag.countList.Add(1);
        }
        else
        {
            //存在宝物
            playerBag.countList[itemInx]++;
        }
        shopUI.Query("productItem").AtIndex(productIndex).style.visibility = Visibility.Hidden;
        LoadPlayerBag();
        surplusProductNum--;
        if (surplusProductNum < 1)
        {
            refreshNum--;
            refreshPriceNum = 0;
        }
        SetProduct();
    }

    // 加载背包物品
    private void LoadPlayerBag()
    {
        var playerBagUI = shopUI.Q("bag");
        playerBagUI.Clear();

        // 倒序展示
        for (int i = playerBag.itemsList.Count - 1; i >= 0; i--)
        {
            var itemTemplate = Resources.Load<VisualTreeAsset>("bagItem").Instantiate();
            itemTemplate.Q("item").style.backgroundImage = playerBag.itemsList[i].itemImg;
            if (playerBag.countList[i] > 1)
            {
                itemTemplate.Q<Label>().text = playerBag.countList[i].ToString();
            }
            else
            {
                itemTemplate.Q<Label>().text = "";
            }
            playerBagUI.Add(itemTemplate);
        }

    }

    // 付款
    private bool Pay(int price)
    {
        if (playerStatus.gold >= price)
        {
            playerStatus.gold -= price;
            shopUI.Q<Label>("gold").text = playerStatus.gold.ToString();
            return true;
        }
        return false;
    }
}
