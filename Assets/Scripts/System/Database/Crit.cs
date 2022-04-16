using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crit : ScriptableObject
{
    public string critName;
    public Sprite icon;
    public bool isTick, hasEnded;
    public float timeMult, minTime, maxTime;
    public List<int> ticksLeft = new List<int>();
    public void TickSetUp(int value)
    {
        int tick = (int)Mathf.Clamp(value * timeMult + minTime, minTime, maxTime);
        for (int i = 0; i < tick; i++)
        {
            ticksLeft.Add(value);
        }
    }
    public void TickStack(int value)
    {
        int tick = (int)Mathf.Clamp(value * timeMult + minTime, minTime, maxTime);
        for (int i = 0; i < tick; i++)
        {
            if (ticksLeft.Contains(i))
            {
                ticksLeft[i] += value;
            }
            else
            {
                ticksLeft.Add(value);
            }
        }
    }
    public virtual IEnumerator PerTick(EnemyController ec)
    {
        yield return new WaitForSeconds(1);
        if (ticksLeft.Count > 0)
        {
            TickEffect(ticksLeft[0], ec);
            if (ticksLeft.Count > 0)
            {
                ticksLeft.Remove(ticksLeft[0]);
                if (ticksLeft.Count <= 0)
                {
                    RemoveTickEffect(ec);
                }
                else
                {
                    ec.InvokeAnotherTick(this);
                }
            } 
        }
    }
    public IEnumerator EffectTimer(int value, EnemyController ec)
    {
        StatusEffect(value, ec);
        yield return new WaitForSeconds(Mathf.Clamp(value * timeMult + minTime, minTime, maxTime));
        RemoveStatusEffect(ec);
    }
    public virtual void StatusEffect(int value, EnemyController ec)
    {

    }
    public virtual void TickEffect(int value, EnemyController ec)
    {

    }
    public virtual void RemoveStatusEffect(EnemyController ec)
    {
        ec.activeCrits.Remove(this);
        ec.critRotate.GetComponent<CritRotator>().CreateCrits(ec.activeCrits);
    }
    private void RemoveTickEffect(EnemyController ec)
    {
        ec.activeCrits.Remove(this);
        ec.critRotate.GetComponent<CritRotator>().CreateCrits(ec.activeCrits);
    }
}
