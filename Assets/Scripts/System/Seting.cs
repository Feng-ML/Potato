using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Seting
{
    public JsonData setingObj;

    // ����
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

    //���øı��¼�
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

        //�����ı��¼�
        OnSetingChanged.Invoke(setingObj);
    }

    #region ��ȡ����
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
