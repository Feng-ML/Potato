using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    private AudioSource AS;

    private void Awake()
    {
        AS = gameObject.GetComponent<AudioSource>();
        AS.volume = Seting.Instance.GetVolume() / 100f;
        Seting.OnSetingChanged += ChangeSeting;
    }

    private void OnDestroy()
    {
        Seting.OnSetingChanged -= ChangeSeting;
    }

    // 订阅设置改变
    private void ChangeSeting(JsonData obj)
    {
        //音效修改
        AS.volume = Seting.Instance.GetVolume() / 100f;
    }
}
