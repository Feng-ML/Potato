using System;

public class Relic2 : Relic
{
    public override void GetRelic()
    {
        foreach (var name in Enum.GetNames(typeof(MyEnums.character)))
        {
            // ȫ����+10
            playerStatus.AttrsChange(name, 10);
        }
    }
}
