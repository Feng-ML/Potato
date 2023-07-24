using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class TaskPool
{
    /// <summary>
    /// 任务信息
    /// </summary>
    [Serializable]
    public class CtsInfo
    {
        /// <summary>
        /// 任务id
        /// </summary>
        [SerializeField]
        public int id;

        /// <summary>
        /// cst实例
        /// </summary>
        [SerializeField]
        public CancellationTokenSource cts;
    }

    /// <summary>
    /// 任务池子
    /// </summary>
    public static List<CtsInfo> ctsInfos = new List<CtsInfo>();

    /// <summary>
    /// 任务编号【自增】
    /// </summary>
    private static int id = 0;

    /// <summary>
    /// 创建一个任务
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
    /// 取消所有的任务
    /// </summary>
    public static void CancelAllTask()
    {
        ctsInfos.ForEach(ci => ci.cts.Cancel());
    }

    /// <summary>
    /// 取消指定的任务
    /// </summary>
    public static void CancelTask(int id)
    {
        ctsInfos.Where(ci => ci.id == id).ToList().ForEach(ci => ci.cts.Cancel());
    }
}
