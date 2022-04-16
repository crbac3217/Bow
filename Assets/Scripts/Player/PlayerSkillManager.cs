using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PlayerSkillManager : MonoBehaviour
{
    public event EventHandler<SkillArgs> SkillUsageGeneral;
    public PlayerControl pc;

    public void Begin()
    {
        pc = GetComponent<PlayerControl>();
    }
    public void OnButtonDown (int numb)
    {
        pc.skills[numb].OnButtonPress(pc);
    }
    public void OnButtonUp(int numb)
    {
        pc.skills[numb].OnButtonRelease(pc);
    }
    public void InvokeEvent()
    {
        if (SkillUsageGeneral != null)
        {
            SkillArgs temp = new SkillArgs { };
            SkillUsageGeneral.Invoke(this, temp);
        }
    }
    public void ActivateForTime(Skill skill, float time)
    {
        StartCoroutine(skill.CoActivate(time, pc));
    }
    public void BeginCooldown(Skill skill)
    {
        StartCoroutine(skill.StartCooldownTimer());
    }
    public void ActivateCoroutine(IEnumerator method)
    {
        StartCoroutine(method);
    }
}
public class SkillArgs:EventArgs
{
}