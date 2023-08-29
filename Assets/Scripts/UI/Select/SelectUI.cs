using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectUI : MonoBehaviour
{
    public ShopProductList productList;     //�����ʲ��б�
    public GameObject player;
    private VisualElement rootEl;
    private List<Charactar> charactarList;  //��ɫ�б�

    private void Awake()
    {
        rootEl = GetComponent<UIDocument>().rootVisualElement;
        charactarList = productList.charactarList;
    }

    private void Start()
    {
        LoadCharactarList();
    }

    private void LoadCharactarList()
    {
        var charactarListUI = rootEl.Q("charactarList");
        charactarListUI.Clear();

        for (int i = 0; i < charactarList.Count; i++)
        {
            var itemTemplate = Resources.Load<VisualTreeAsset>("charactarItem").Instantiate();
            var item = charactarList[i];
            var background = new StyleBackground(item.charactarImg);
            itemTemplate.Q("img").style.backgroundImage = background;

            charactarListUI.Add(itemTemplate);

            itemTemplate.RegisterCallback<MouseUpEvent>(evt =>
            {
                player.GetComponent<Animator>().runtimeAnimatorController = item.charactarAnimator;
            }, TrickleDown.TrickleDown);

            itemTemplate.RegisterCallback<MouseOverEvent>(evt =>
            {
                itemTemplate.Q("item").style.backgroundColor = new Color(255, 255, 255, .4f);
            }, TrickleDown.TrickleDown);

            itemTemplate.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                itemTemplate.Q("item").style.backgroundColor = new Color(0, 0, 0, .5f);
            });
        }
    }
}
