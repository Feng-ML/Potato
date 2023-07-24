using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class TaskPool
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    [Serializable]
    public class CtsInfo
    {
        /// <summary>
        /// ����id
        /// </summary>
        [SerializeField]
        public int id;

        /// <summary>
        /// cstʵ��
        /// </summary>
        [SerializeField]
        public CancellationTokenSource cts;
    }

    /// <summary>
    /// �������
    /// </summary>
    public static List<CtsInfo> ctsInfos = new List<CtsInfo>();

    /// <summary>
    /// �����š�������
    /// </summary>
    private static int id = 0;

    /// <summary>
    /// ����һ������
    /// </summary>
    /// <returns></returns>
    public static CtsInfo CreatCts()
    {
        var cts = new CancellationTokenSource();
        var ci = new CtsInfo { cts = cts, id = id };
        id++;
        ctsInfos.Add(ci);
        return ci;
    }

    /// <summary>
    /// ȡ�����е�����
    /// </summary>
    public static void CancelAllTask()
    {
        ctsInfos.ForEach(ci => ci.cts.Cancel());
    }

    /// <summary>
    /// ȡ��ָ��������
    /// </summary>
    public static void CancelTask(int id)
    {
        ctsInfos.Where(ci => ci.id == id).ToList().ForEach(ci => ci.cts.Cancel());
    }
}
