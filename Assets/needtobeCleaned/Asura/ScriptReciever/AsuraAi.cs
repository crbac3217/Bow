using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsuraAi : BossAi
{
    public int currentFormIndex;
    public float swordSpeed, bowSpeed, magicSpeed;
    public List<EnemyAttack> swordAttacksPref, bowAttacksPref, magicAttacksPref = new List<EnemyAttack>();
    public List<EnemyAttack> swordAttacks, bowAttacks, magicAttacks = new List<EnemyAttack>();
    private AsuraController acon;

    public override void SetUp()
    {
        base.SetUp();
        foreach (EnemyAttack at in swordAttacksPref)
        {
            EnemyAttack temp = Instantiate(at);
            swordAttacks.Add(temp);
            temp.aiHandler = this;
            temp.SetUp();
        }
        foreach (EnemyAttack at in bowAttacksPref)
        {
            EnemyAttack temp = Instantiate(at);
            bowAttacks.Add(temp);
            temp.aiHandler = this;
            temp.SetUp();
        }
        foreach (EnemyAttack at in magicAttacksPref)
        {
            EnemyAttack temp = Instantiate(at);
            magicAttacks.Add(temp);
            temp.aiHandler = this;
            temp.SetUp();
        }
        acon = GetComponent<AsuraController>();
    }

    public void FormChange()
    {
        ChangeFrom();
        bool up = (Random.value >= 0.5);
        ChangeTo(up);
    }
    private void ChangeFrom()
    {
        if (currentFormIndex == 1)
        {
            foreach (DamageType sdty in ec.strength)
            {
                foreach (DamageType str in acon.swordStrength)
                {
                    if (sdty.damageElement == str.damageElement)
                    {
                        sdty.value -= str.value;
                    }
                }
            }
            foreach (DamageType wdty in ec.weakness)
            {
                foreach (DamageType wk in acon.swordWeakness)
                {
                    if (wdty.damageElement == wk.damageElement)
                    {
                        wdty.value -= wk.value;
                    }
                }
            }
            
        }
        else if (currentFormIndex == 2)
        {
            foreach (DamageType sdty in ec.strength)
            {
                foreach (DamageType str in acon.bowStrength)
                {
                    if (sdty.damageElement == str.damageElement)
                    {
                        sdty.value -= str.value;
                    }
                }
            }
            foreach (DamageType wdty in ec.weakness)
            {
                foreach (DamageType wk in acon.bowWeakness)
                {
                    if (wdty.damageElement == wk.damageElement)
                    {
                        wdty.value -= wk.value;
                    }
                }
            }
        }
        else if (currentFormIndex == 3)
        {
            foreach (DamageType sdty in ec.strength)
            {
                foreach (DamageType str in acon.magicStrength)
                {
                    if (sdty.damageElement == str.damageElement)
                    {
                        sdty.value -= str.value;
                    }
                }
            }
            foreach (DamageType wdty in ec.weakness)
            {
                foreach (DamageType wk in acon.magicWeakness)
                {
                    if (wdty.damageElement == wk.damageElement)
                    {
                        wdty.value -= wk.value;
                    }
                }
            }
        }
        ec.invincible = true;
    }
    private void ChangeTo(bool up)
    {
        attacks.Clear();
        if (up)
        {
            currentFormIndex++;
            if (currentFormIndex > 3)
            {
                currentFormIndex = 1;
            }
        }
        else
        {
            currentFormIndex--;
            if (currentFormIndex < 1)
            {
                currentFormIndex = 3;
            }
        }
        anim.SetInteger("FormIndex", currentFormIndex);
        if (currentFormIndex == 1)
        {
            foreach (DamageType sdty in ec.strength)
            {
                foreach (DamageType str in acon.swordStrength)
                {
                    if (sdty.damageElement == str.damageElement)
                    {
                        sdty.value += str.value;
                    }
                }
            }
            foreach (DamageType wdty in ec.weakness)
            {
                foreach (DamageType wk in acon.swordWeakness)
                {
                    if (wdty.damageElement == wk.damageElement)
                    {
                        wdty.value += wk.value;
                    }
                }
            }
            attacks = swordAttacks;
            chaseSpeed = swordSpeed;
        }
        else if (currentFormIndex == 2)
        {
            foreach (DamageType sdty in ec.strength)
            {
                foreach (DamageType str in acon.bowStrength)
                {
                    if (sdty.damageElement == str.damageElement)
                    {
                        sdty.value += str.value;
                    }
                }
            }
            foreach (DamageType wdty in ec.weakness)
            {
                foreach (DamageType wk in acon.bowWeakness)
                {
                    if (wdty.damageElement == wk.damageElement)
                    {
                        wdty.value += wk.value;
                    }
                }
            }
            attacks = bowAttacks;
            chaseSpeed = bowSpeed;
        }
        else if (currentFormIndex == 3)
        {
            foreach (DamageType sdty in ec.strength)
            {
                foreach (DamageType str in acon.magicStrength)
                {
                    if (sdty.damageElement == str.damageElement)
                    {
                        sdty.value += str.value;
                    }
                }
            }
            foreach (DamageType wdty in ec.weakness)
            {
                foreach (DamageType wk in acon.magicWeakness)
                {
                    if (wdty.damageElement == wk.damageElement)
                    {
                        wdty.value += wk.value;
                    }
                }
            }
            attacks = magicAttacks;
            chaseSpeed = magicSpeed;
        }
        ec.invincible = false;
    }
}
