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
    public ShopProductList productList;     //���б���
    public PlayerStatus playerStatus;
    public GameStatus gameStatus;           //��Ϸ״̬
    public Bag playerBag;

    public AudioClip buyAudio;
    public AudioClip deniedAudio;

    public int weaponSlot = 6;              //������λ

    private BindableProperty<int> gold;
    private List<Item> currentProductList = new List<Item>();     //��ǰ��Ʒ�б�
    private int surplusProductNum;      //ʣ����Ʒ��
    private int refreshNum;             //ˢ���̵����
    private int refreshPriceNum;        //ˢ�¼۸�
    private int selectWeaponIndex;      //��ǰѡ�������±�

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

        //������Ҹı�
        gold.RegisterWithInitValue(goldChange).UnRegisterWhenGameObjectDestroyed(gameObject);
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
            playerStatus.gold = gold.Value;
            SceneManager.LoadScene("Fight");
        };

        // ��ťHover��ʽ
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
        //�ϲ�����
        shopUI.Q<Button>("mergeButton").clicked += () =>
        {
            int sameIndex = -1;
            for (int i = 0; i < playerBag.weaponList.Count; i++)
            {
                //��ͬ���� ��ͬƷ��
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

        //��������
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


    // ˢ���̵�
    private void RefreshShop()
    {
        var price = refreshPriceNum;
        if (gold < price)
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

        //ˢ�¼۸����
        refreshNum++;
        refreshPriceNum = refreshNum * 5;

        SetProduct();
        gold.Value -= price;
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
        }

        shopUI.Q<Label>("refreshCost").text = "ˢ�� -" + refreshPriceNum;
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
        //����Ǯ
        if (gold < item.cost)
        {
            AudioSource.PlayClipAtPoint(deniedAudio, transform.position);
            return;
        }

        AudioSource.PlayClipAtPoint(buyAudio, transform.position);
        if (item.isWeapon)
        {
            //����
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
        gold.Value -= item.cost;
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

                //��������
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

                //���ص���
                weaponBoxUI.RegisterCallback<MouseOutEvent>(evt =>
                {
                    dialog.visible = false;
                }, TrickleDown.TrickleDown);

                DialogEvent(dialog);
            }
        }
    }

    //������ʾ���������Ż�
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

        //��Ʒ�۸���ɫ
        shopUI.Q("productList").Query<Label>("productCost").ForEach(item =>
        {
            var textColor = int.Parse(item.text) > newVal ? Color.red : Color.white;
            item.style.color = textColor;
        });

        //ˢ�°�ť��ɫ
        var refreshColor = refreshPriceNum > newVal ? Color.red : Color.white;
        shopUI.Q<Label>("refreshCost").style.color = refreshColor;
    }
}
