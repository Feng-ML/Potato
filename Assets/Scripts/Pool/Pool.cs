using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> : MonoBehaviour where T : Component
{
    private ObjectPool<T> pool;
    public T prefab;

    public int activeCount => pool.CountActive;
    public int inActiveCount => pool.CountInactive;
    public int totalCount => pool.CountAll;

    protected void InitPool(int defaultSize = 100, int maxSize = 500, bool onCheck = true)
    {
        pool = new ObjectPool<T>(OnCreateObj, OnGetObj, OnReleaseObj, OnDestroyObj, onCheck, defaultSize, maxSize);
    }

    protected virtual T OnCreateObj() => Instantiate(prefab, transform);
    protected virtual void OnGetObj(T obj) => obj.gameObject.SetActive(true);
    protected virtual void OnReleaseObj(T obj) => obj.gameObject.SetActive(false);
    protected virtual void OnDestroyObj(T obj) => Destroy(obj.gameObject);

    public T Get() => pool.Get();
    public void Release(T obj) => pool.Release(obj);
    public void Clear() => pool.Clear();
    public void SetPrefab(T obj) => prefab = obj;

}
