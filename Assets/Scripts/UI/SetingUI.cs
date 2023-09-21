using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetingUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;

    private void OnEnable()
    {
        musicSlider.value = Seting.Instance.GetMusic();
        volumeSlider.value = Seting.Instance.GetVolume();
        fullScreenToggle.isOn = Seting.Instance.GetFullScreen();
    }

    public void SaveSeting()
    {
        JsonData seting = new JsonData();
        seting["music"] = (int)musicSlider.value;
        seting["volume"] = (int)volumeSlider.value;
        seting["fullScreen"] = fullScreenToggle.isOn;
        Seting.Instance.SaveJson(seting);
        gameObject.SetActive(false);

        // 切换到全屏模式
        //Screen.fullScreen = fullScreenToggle.isOn;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, fullScreenToggle.isOn);
    }
}
