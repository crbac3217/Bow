using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerReceiver : MonoBehaviour
{
    public AiHandler ai;
    public void DeadAnimFinished()
    {
        ai.DeadAnimFinished();
    }
    public void AttackTriggered(string attackName)
    {
        ai.AttackTriggered(attackName);
    }
    public void AdditionalAttackTriggered(string attackName)
    {
        ai.AdditionalAttackTriggered(attackName);
    }
}
