using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Seting
{
    public JsonData setingObj;

    // 单例
    public static Seting Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Seting();
            }
            return instance;
        }
    }
    private static Seting instance;
    private string setingPath;

    //设置改变事件
    public delegate void SetingChanged(JsonData setingObj);
    public static event SetingChanged OnSetingChanged;

    private Seting()
    {
        setingObj = GetSetingObj();
    }

    private JsonData GetSetingObj()
    {
        setingPath = Application.persistentDataPath + "/Seting.json";

        if (!File.Exists(setingPath))
        {
            File.Create(setingPath).Dispose();
        }
        var json = File.ReadAllText(setingPath);

        JsonData setingObj = JsonMapper.ToObject(json);
        return setingObj;
    }

    public void SaveJson(JsonData obj)
    {
        setingObj = obj;
        var json = JsonMapper.ToJson(obj);
        File.WriteAllText(setingPath, json);

        //发布改变事件
        OnSetingChanged.Invoke(setingObj);
    }

    #region 获取数据
    private bool HasProp(string propName)
    {
        return ((IDictionary)setingObj).Contains(propName);
    }

    public int GetVolume()
    {
        if (HasProp("volume"))
        {
            return setingObj["volume"].IsInt ? (int)setingObj["volume"] : 100;
        }
        else
        {
            return 100;
        }
    }

    public int GetMusic()
    {
        if (HasProp("music"))
        {
            return setingObj["music"].IsInt ? (int)setingObj["music"] : 100;
        }
        else
        {
            return 100;
        }
    }

    public bool GetFullScreen()
    {
        if (HasProp("fullScreen"))
        {
            return setingObj["fullScreen"].IsBoolean ? (bool)setingObj["fullScreen"] : false;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
