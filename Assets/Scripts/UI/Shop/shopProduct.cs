using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class shopProduct : MonoBehaviour
{
    private VisualElement shopUI;
    public ShopProductList productList;     //所有宝物
    public PlayerStatus playerStatus;
    public GameStatus gameStatus;           //游戏状态
    public Bag playerBag;

    public AudioClip buyAudio;
    public AudioClip deniedAudio;

    public int weaponSlot = 6;              //武器槽位

    private BindableProperty<int> gold;
    private List<Item> currentProductList = new List<Item>();     //当前商品列表
    private int surplusProductNum;      //剩余商品数
    private int refreshNum;             //刷新商店次数
    private int refreshPriceNum;        //刷新价格
    private int selectWeaponIndex;      //当前选择武器下标

    void Start()
    {
        shopUI = GetComponent<UIDocument>().rootVisualElement;
        gold = new BindableProperty<int>(playerStatus.gold);
        RefreshShop();
        SetPlayerStatus();
        LoadPlayerBag();
        LoadPlayerWeapon();

        AddEvent();
        AddWeaponEvent();

        //监听金币改变
        gold.RegisterWithInitValue(goldChange).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    private void AddEvent()
    {
        // 刷新商店点击事件
        shopUI.Q<Button>("refreshButton").clicked += RefreshShop;

        // 购买商品点击事件
        shopUI.Query("productItem").ForEach((item) =>
        {
            int index = int.Parse(item.Q<Button>().name.Split("-")[1]);
            item.Q<Button>().clicked += () => BuyProduct(index);
        });

        // 出发按钮
        shopUI.Q<Button>("fightButton").clicked += () =>
        {
            gameStatus.wave++;
            playerStatus.gold = gold.Value;
            SceneManager.LoadScene("Fight");
        };

        // 按钮Hover样式
        shopUI.Query(className: "button").ForEach(item =>
        {
            item.RegisterCallback<MouseOverEvent>(ev =>
            {
                item.style.opacity = 0.7f;
            }, TrickleDown.TrickleDown);
            item.RegisterCallback<MouseLeaveEvent>(ev =>
            {
                item.style.opacity = 1f;
            });
        });
    }

    private void AddWeaponEvent()
    {
        //合并武器
        shopUI.Q<Button>("mergeButton").clicked += () =>
        {
            int sameIndex = -1;
            for (int i = 0; i < playerBag.weaponList.Count; i++)
            {
                //相同武器 相同品质
                if (i != selectWeaponIndex
                && playerBag.weaponList[i] == playerBag.weaponList[selectWeaponIndex]
                && playerBag.weaponQualityList[i] == playerBag.weaponQualityList[selectWeaponIndex])
                {
                    sameIndex = i;
                    break;
                }
            }

            if (sameIndex > -1)
            {
                shopUI.Q("weaponDialog").visible = false;
                playerBag.weaponQualityList[selectWeaponIndex]++;
                playerBag.weaponList.RemoveAt(sameIndex);
                playerBag.weaponQualityList.RemoveAt(sameIndex);
                LoadPlayerWeapon();
            }
            else
            {
                AudioSource.PlayClipAtPoint(deniedAudio, transform.position);
            }
        };

        //卖出武器
        shopUI.Q<Button>("sellButton").clicked += () =>
        {
            shopUI.Q("weaponDialog").visible = false;
            gold.Value += playerBag.weaponList[selectWeaponIndex].cost / 3;
            playerBag.weaponList.RemoveAt(selectWeaponIndex);
            playerBag.weaponQualityList.RemoveAt(selectWeaponIndex);
            AudioSource.PlayClipAtPoint(buyAudio, transform.position);
            LoadPlayerWeapon();
        };
    }


    // 刷新商店
    private void RefreshShop()
    {
        var price = refreshPriceNum;
        if (gold < price)
        {
            AudioSource.PlayClipAtPoint(deniedAudio, transform.position);
            return;
        };

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

        //刷新价格计算
        refreshNum++;
        refreshPriceNum = refreshNum * 5;

        SetProduct();
        gold.Value -= price;
    }

    // 渲染商品 刷新按钮
    private void SetProduct()
    {
        for (int i = 0; i < 4; i++)
        {
            var item = shopUI.Query("productItem").AtIndex(i);
            var background = new StyleBackground(currentProductList[i].itemImg);
            item.Q("productImg").style.backgroundImage = background;
            item.Q<Label>("productName").text = currentProductList[i].itemName;
            item.Q<Label>("productInfo").text = currentProductList[i].itemInfo;
            item.Q<Label>("productCost").text = currentProductList[i].cost.ToString();
            item.style.backgroundColor = MyDictionary.qualityColor[currentProductList[i].quality];
        }

        shopUI.Q<Label>("refreshCost").text = "刷新 -" + refreshPriceNum;
    }

    // 玩家状态赋值
    private void SetPlayerStatus()
    {
        SetTextAndColor("maxHealthValue", playerStatus.maxHealth);
        SetTextAndColor("healthRecoverValue", playerStatus.healthRecover);
        SetTextAndColor("attackValue", playerStatus.attack);
        SetTextAndColor("attackSpeedValue", playerStatus.attackSpeed);
        SetTextAndColor("criticalRateValue", playerStatus.criticalRate);
        SetTextAndColor("criticalDamageValue", playerStatus.criticalDamage);
        SetTextAndColor("attackRangeValue", playerStatus.attackRange);
        SetTextAndColor("armorValue", playerStatus.armor);
        SetTextAndColor("dodgeRateValue", playerStatus.dodgeRate);
        SetTextAndColor("speedValue", playerStatus.speed);
        SetTextAndColor("pickUpRangeValue", playerStatus.pickUpRange);
    }

    private void SetTextAndColor(string statusName, float num)
    {
        shopUI.Q<Label>(statusName).text = num.ToString();
        var color = num > 0 ? Color.green : Color.red;
        if (num == 0) color = Color.white;
        shopUI.Q<Label>(statusName).style.color = color;
    }

    //购买商品
    private void BuyProduct(int productIndex)
    {
        var item = currentProductList[productIndex];
        //是否有枪械位置
        if (item.isWeapon && CanBuyWeapon(item) < 0)
        {
            AudioSource.PlayClipAtPoint(deniedAudio, transform.position);
            return;
        }
        //不够钱
        if (gold < item.cost)
        {
            AudioSource.PlayClipAtPoint(deniedAudio, transform.position);
            return;
        }

        AudioSource.PlayClipAtPoint(buyAudio, transform.position);
        if (item.isWeapon)
        {
            //武器
            if (playerBag.weaponList.Count < weaponSlot)
            {
                playerBag.weaponList.Add(item);
                playerBag.weaponQualityList.Add(item.quality);
            }
            else
            {
                int sameIndex = CanBuyWeapon(item);
                playerBag.weaponQualityList[sameIndex]++;
            }
            LoadPlayerWeapon();
        }
        else
        {
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
            LoadPlayerBag();
        }
        shopUI.Query("productItem").AtIndex(productIndex).style.visibility = Visibility.Hidden;

        playerStatus.AttrsChange(item.effectList);
        surplusProductNum--;
        if (surplusProductNum < 1)
        {
            refreshNum--;
            refreshPriceNum = 0;
        }
        SetProduct();
        SetPlayerStatus();
        gold.Value -= item.cost;
    }

    //能否购买武器
    private int CanBuyWeapon(Item item)
    {
        if (playerBag.weaponList.Count < 2)
        {
            return 1;
        }
        int sameWeaponIndex = -1;
        for (int i = 0; i < playerBag.weaponList.Count; i++)
        {
            var quality = playerBag.weaponQualityList[i];

            if (quality < MyEnums.QualityLevel.mythic)
            {
                if (playerBag.weaponList[i].id == item.id && quality == item.quality)
                {
                    sameWeaponIndex = i;
                    break;
                }
            }
        }

        return sameWeaponIndex;
    }

    // 加载背包物品
    private void LoadPlayerBag()
    {
        var playerBagUI = shopUI.Q("bag");
        playerBagUI.Clear();
        var dialog = shopUI.Q("itemDialog");

        // 倒序展示
        for (int i = playerBag.itemsList.Count - 1; i >= 0; i--)
        {
            var itemTemplate = Resources.Load<VisualTreeAsset>("bagItem").Instantiate();
            var item = playerBag.itemsList[i];
            var background = new StyleBackground(item.itemImg);
            itemTemplate.Q("item").style.backgroundImage = background;
            itemTemplate.Q("itemBox").style.backgroundColor = MyDictionary.qualityColor[item.quality];
            if (playerBag.countList[i] > 1)
            {
                itemTemplate.Q<Label>().text = playerBag.countList[i].ToString();
            }
            else
            {
                itemTemplate.Q<Label>().text = "";
            }
            playerBagUI.Add(itemTemplate);

            //物品弹窗
            itemTemplate.RegisterCallback<MouseEnterEvent>(evt =>
            {
                dialog.visible = true;
                dialog.style.left = evt.mousePosition.x;
                dialog.style.top = evt.mousePosition.y - dialog.contentRect.height - 20;

                dialog.Q("productImg").style.backgroundImage = background;
                dialog.Q<Label>("productName").text = item.itemName;
                dialog.Q<Label>("productInfo").text = item.itemInfo;
            }, TrickleDown.TrickleDown);


            //隐藏物品弹窗
            itemTemplate.RegisterCallback<MouseOutEvent>(evt =>
            {
                dialog.visible = false;
            }, TrickleDown.TrickleDown);

            DialogEvent(dialog);
        }

    }

    // 加载武器列表
    private void LoadPlayerWeapon()
    {
        var dialog = shopUI.Q("weaponDialog");

        var weaponBagUI = shopUI.Q("weaponList");
        weaponBagUI.Clear();
        for (int i = 0; i < weaponSlot; i++)
        {
            var itemTemplate = Resources.Load<VisualTreeAsset>("weaponItem").Instantiate();
            weaponBagUI.Add(itemTemplate);

            if (i < playerBag.weaponList.Count)
            {
                var weaponItem = playerBag.weaponList[i];
                var weaponIndex = i;
                var weaponBoxUI = shopUI.Query("weapon").AtIndex(i);
                var weaponUI = shopUI.Query("weaponImg").AtIndex(i);
                var background = new StyleBackground(weaponItem.itemImg);
                weaponUI.style.backgroundImage = background;
                weaponBoxUI.style.backgroundColor = MyDictionary.qualityColor[playerBag.weaponQualityList[i]];

                //武器弹窗
                weaponBoxUI.RegisterCallback<MouseEnterEvent>((evt) =>
                {
                    selectWeaponIndex = weaponIndex;
                    dialog.visible = true;
                    dialog.style.left = evt.mousePosition.x;
                    dialog.style.top = evt.mousePosition.y - dialog.contentRect.height - 20;

                    dialog.Q("productImg").style.backgroundImage = background;
                    dialog.Q<Label>("productName").text = weaponItem.itemName;
                    dialog.Q<Label>("productInfo").text = weaponItem.itemInfo;
                    dialog.Q<Label>("productCost").text = weaponItem.cost.ToString();
                    dialog.Q<Label>("sellNum").text = (weaponItem.cost / 3).ToString();
                    if (playerStatus.gold < weaponItem.cost)
                    {
                        dialog.Q<Label>("productCost").style.color = Color.red;
                    }
                }, TrickleDown.TrickleDown);

                //隐藏弹窗
                weaponBoxUI.RegisterCallback<MouseOutEvent>(evt =>
                {
                    dialog.visible = false;
                }, TrickleDown.TrickleDown);

                DialogEvent(dialog);
            }
        }
    }

    //弹窗显示隐藏体验优化
    private void DialogEvent(VisualElement dialog)
    {
        dialog.RegisterCallback<MouseOverEvent>(evt =>
        {
            dialog.visible = true;
        }, TrickleDown.TrickleDown);

        dialog.RegisterCallback<MouseLeaveEvent>(evt =>
        {
            dialog.visible = false;
        });
    }

    private void goldChange(int newVal)
    {
        shopUI.Q<Label>("gold").text = newVal.ToString();

        //商品价格颜色
        shopUI.Q("productList").Query<Label>("productCost").ForEach(item =>
        {
            var textColor = int.Parse(item.text) > newVal ? Color.red : Color.white;
            item.style.color = textColor;
        });

        //刷新按钮颜色
        var refreshColor = refreshPriceNum > newVal ? Color.red : Color.white;
        shopUI.Q<Label>("refreshCost").style.color = refreshColor;
    }
}
