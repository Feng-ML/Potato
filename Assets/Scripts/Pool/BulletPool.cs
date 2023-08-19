using UnityEngine;

public class BulletPool : Pool<Bullet>
{

    private void Awake()
    {
        InitPool();
    }

    protected override Bullet OnCreateObj()
    {
        var ins = Instantiate(prefab, transform);
        ins.SetDeactiveAction(delegate { Release(ins); });

        return ins;
    }

    protected override void OnReleaseObj(Bullet obj)
    {
        base.OnReleaseObj(obj);
        //����λ�ã���ֹ��ȡʱ������ײ
        obj.transform.position = new Vector2(-40, 0);
    }
}
