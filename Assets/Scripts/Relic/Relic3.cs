using System;

public class Relic3 : Relic
{
    public override void GetRelic()
    {
        playerStatus.isAttackWithPoison = true;
    }
}
