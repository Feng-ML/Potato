using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class shopProduct : MonoBehaviour
{
    private VisualElement shopUI;
    public ShopProductList productList;     //���б���
    public PlayerStatus playerStatus;
    public GameStatus gameStatus;           //��Ϸ״̬
    public Bag playerBag;

    public AudioClip buyAudio;
    public AudioClip deniedAudio;

    private List<Item> currentProductList = new List<Item>();     //��ǰ��Ʒ�б�
    private int surplusProductNum;      //ʣ����Ʒ��
    private int refreshNum;             //ˢ���̵����
    private int refreshPriceNum;        //ˢ�¼۸�

    void Start()
    {
        shopUI = GetComponent<UIDocument>().rootVisualElement;
        RefreshShop();
        SetPlayerStatus();
        LoadPlayerBag();
        LoadPlayerWeapon();

        AddEvent();
    }

    private void AddEvent()
    {
        // ˢ���̵����¼�
        shopUI.Q<Button>("refreshButton").clicked += RefreshShop;

        // ������Ʒ����¼�
        shopUI.Query("productItem").ForEach((item) =>
        {
            int index = int.Parse(item.Q<Button>().name.Split("-")[1]);
            item.Q<Button>().clicked += () => BuyProduct(index);
        });

        // ������ť
        shopUI.Q<Button>("fightButton").clicked += () =>
        {
            gameStatus.wave++;
            SceneManager.LoadScene("Fight");
        };

        // ��ťHover��ʽ
        shopUI.Query(className: "button").ForEach(item =>
        {
            item.RegisterCallback<MouseEnterEvent>(ev =>
            {
                item.style.opacity = 0.7f;
            });
            item.RegisterCallback<MouseOutEvent>(ev =>
            {
                item.style.opacity = 1f;
            });
        });
    }


    // ˢ���̵�
    private void RefreshShop()
    {
        if (!Pay(refreshPriceNum))
        {
            AudioSource.PlayClipAtPoint(deniedAudio, transform.position);
            return;
        };

        currentProductList.Clear();
        // ���4����ͬ�������
        var randomNumbers = new HashSet<int>();
        while (randomNumbers.Count < 4)
        {
            int randomNumber = UnityEngine.Random.Range(0, productList.itemsList.Count);
            randomNumbers.Add(randomNumber);
        }
        // �����ֵ��Ʒ
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

        // �۸���㸳ֵ
        refreshNum++;
        refreshPriceNum = refreshNum * 5;

        SetProduct();
        SetPlayerStatus();
    }

    // ��Ⱦ��Ʒ ˢ�°�ť
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

            if (playerStatus.gold < currentProductList[i].cost)
            {
                item.Q<Label>("productCost").style.color = Color.red;
            }
        }

        shopUI.Q<Label>("refreshCost").text = "ˢ�� -" + refreshPriceNum;
        if (playerStatus.gold < refreshPriceNum)
        {
            shopUI.Q<Label>("refreshCost").style.color = Color.red;
        }
    }

    // ���״̬��ֵ
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
        SetTextAndColor("gold", playerStatus.gold);
    }

    private void SetTextAndColor(string statusName, float num)
    {
        shopUI.Q<Label>(statusName).text = num.ToString();
        var color = num > 0 ? Color.green : Color.red;
        if (num == 0) color = Color.white;
        shopUI.Q<Label>(statusName).style.color = color;
    }

    //������Ʒ
    private void BuyProduct(int productIndex)
    {
        var item = currentProductList[productIndex];
        //�Ƿ���ǹеλ��
        if (item.isWeapon && CanBuyWeapon(item) < 0)
        {
            AudioSource.PlayClipAtPoint(deniedAudio, transform.position);
            return;
        }
        //������Ǯ
        if (!Pay(item.cost))
        {
            AudioSource.PlayClipAtPoint(deniedAudio, transform.position);
            return;
        }

        AudioSource.PlayClipAtPoint(buyAudio, transform.position);
        if (item.isWeapon)
        {
            //����
            if (playerBag.weaponList.Count < 2)
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
                // �����ڱ���
                playerBag.itemsList.Add(item);
                playerBag.countList.Add(1);
            }
            else
            {
                //���ڱ���
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
    }

    //�ܷ�������
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

    // ����
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

    // ���ر�����Ʒ
    private void LoadPlayerBag()
    {
        var playerBagUI = shopUI.Q("bag");
        playerBagUI.Clear();
        var dialog = shopUI.Q("itemDialog");

        // ����չʾ
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

            //��Ʒ����
            itemTemplate.RegisterCallback<MouseEnterEvent>(evt =>
            {
                dialog.visible = true;
                dialog.style.left = evt.mousePosition.x;
                dialog.style.top = evt.mousePosition.y - dialog.contentRect.height - 20;

                dialog.Q("productImg").style.backgroundImage = background;
                dialog.Q<Label>("productName").text = item.itemName;
                dialog.Q<Label>("productInfo").text = item.itemInfo;
            }, TrickleDown.TrickleDown);


            //������Ʒ����
            itemTemplate.RegisterCallback<MouseOutEvent>(evt =>
            {
                dialog.visible = false;
            }, TrickleDown.TrickleDown);

            DialogEvent(dialog);
        }

    }

    // ���������б�
    private void LoadPlayerWeapon()
    {
        var dialog = shopUI.Q("weaponDialog");

        for (int i = 0; i < playerBag.weaponList.Count; i++)
        {
            var weaponItem = playerBag.weaponList[i];
            var weaponBoxUI = shopUI.Query("weapon").AtIndex(i);
            var weaponUI = shopUI.Query("weaponImg").AtIndex(i);
            var background = new StyleBackground(weaponItem.itemImg);
            weaponUI.style.backgroundImage = background;
            weaponBoxUI.style.backgroundColor = MyDictionary.qualityColor[playerBag.weaponQualityList[i]];

            //��������
            weaponBoxUI.RegisterCallback<MouseEnterEvent>((evt) =>
            {
                dialog.visible = true;
                dialog.style.left = evt.mousePosition.x;
                dialog.style.top = evt.mousePosition.y - dialog.contentRect.height - 20;

                dialog.Q("productImg").style.backgroundImage = background;
                dialog.Q<Label>("productName").text = weaponItem.itemName;
                dialog.Q<Label>("productInfo").text = weaponItem.itemInfo;
                dialog.Q<Label>("productCost").text = weaponItem.cost.ToString();
            }, TrickleDown.TrickleDown);

            //���ص���
            weaponBoxUI.RegisterCallback<MouseOutEvent>(evt =>
            {
                dialog.visible = false;
            }, TrickleDown.TrickleDown);

            DialogEvent(dialog);
        }
    }

    //������ʾ���������Ż�
    private void DialogEvent(VisualElement dialog)
    {
        dialog.RegisterCallback<MouseOverEvent>(evt =>
        {
            dialog.visible = true;
        }, TrickleDown.TrickleDown);

        dialog.RegisterCallback<MouseOutEvent>(evt =>
        {
            dialog.visible = false;
        }, TrickleDown.TrickleDown);
    }
}
