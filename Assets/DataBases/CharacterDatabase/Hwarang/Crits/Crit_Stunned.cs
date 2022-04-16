using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_Stunned", menuName = "Crits/Hwarang/Stunned", order = 111)]
public class Crit_Stunned : Crit
{
    public override void StatusEffect(int value, EnemyController ec)
    {
        base.StatusEffect(value, ec);
        ec.aiHandler.Hit();
    }
    public override void RemoveStatusEffect(EnemyController ec)
    {
        ec.aiHandler.RemoveSnareStack();
        base.RemoveStatusEffect(ec);
    }
}
