using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SelectUI : MonoBehaviour
{
    public ShopProductList productList;     //所有资产列表
    public GameObject player;
    public Bag bag;

    public AudioClip confirmAudio;
    private float volume;

    private VisualElement rootEl;
    private GameObject weapon;

    private int charactarIndex;
    private int weaponIndex;

    private void Awake()
    {
        rootEl = GetComponent<UIDocument>().rootVisualElement;
        weapon = player.transform.Find("Weapon").gameObject;
        volume = Seting.Instance.GetVolume() / 100f;
    }

    private void Start()
    {
        LoadCharactarList();
        LoadWeaponList();

        rootEl.Q<Button>("goButton").clicked += GoToFight;
    }

    private void LoadCharactarList()
    {
        var charactarListUI = rootEl.Q("charactarList");
        charactarListUI.Clear();

        for (int i = 0; i < productList.charactarList.Count; i++)
        {
            var index = i;
            var itemTemplate = Resources.Load<VisualTreeAsset>("charactarItem").Instantiate();
            var item = productList.charactarList[i];
            var background = new StyleBackground(item.charactarImg);
            itemTemplate.Q("img").style.backgroundImage = background;

            charactarListUI.Add(itemTemplate);

            itemTemplate.RegisterCallback<MouseUpEvent>(evt =>
            {
                player.GetComponent<Animator>().runtimeAnimatorController = item.charactarAnimator;
                charactarIndex = index;
                AudioSource.PlayClipAtPoint(confirmAudio, transform.position, volume);
            }, TrickleDown.TrickleDown);

            SetItemEvent(itemTemplate);
        }
    }

    private void LoadWeaponList()
    {
        var weaponListUI = rootEl.Q("weaponList");
        weaponListUI.Clear();

        for (int i = 0; i < productList.weaponList.Count; i++)
        {
            var index = i;
            var itemTemplate = Resources.Load<VisualTreeAsset>("charactarItem").Instantiate();
            var item = productList.weaponList[i];
            var background = new StyleBackground(item.itemImg);
            itemTemplate.Q("img").style.backgroundImage = background;

            weaponListUI.Add(itemTemplate);

            itemTemplate.RegisterCallback<MouseUpEvent>(evt =>
            {
                weapon.GetComponent<SpriteRenderer>().sprite = item.itemImg;
                weaponIndex = index;
                AudioSource.PlayClipAtPoint(confirmAudio, transform.position, volume);
            }, TrickleDown.TrickleDown);

            SetItemEvent(itemTemplate);
        }
    }

    private void SetItemEvent(TemplateContainer temp)
    {
        temp.RegisterCallback<MouseOverEvent>(evt =>
            {
                temp.Q("item").style.backgroundColor = new Color(255, 255, 255, .4f);
            }, TrickleDown.TrickleDown);

        temp.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                temp.Q("item").style.backgroundColor = new Color(0, 0, 0, .5f);
            });
    }

    private void GoToFight()
    {
        AudioSource.PlayClipAtPoint(confirmAudio, transform.position, volume);
        bag.charactar = productList.charactarList[charactarIndex];

        bag.weaponList.Add(productList.weaponList[weaponIndex]);
        bag.weaponQualityList.Add(MyEnums.QualityLevel.common);
        SceneManager.LoadScene("Fight");
    }
}
