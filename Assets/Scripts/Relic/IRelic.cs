using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRelic
{
    public void OnGetRelic(PlayerManager player);
    public void OnAttack(Quaternion rotation, Vector2 pos, int bulletIndex);
    public void OnDamage(int damage);
}
