using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    //be sure to get damageCrits from playertype and dm from level manager
    public AiHandler aiHandler;
    public Color color;
    public bool invincible;
    public int hp, maxHp, minGoldDrop, maxGoldDrop, chestDropMax, chestTier;
    public List<DamageType> weakness, strength = new List<DamageType>();
    public List<Crit> activeCrits = new List<Crit>();
    public Crit[] damageCrits = new Crit[] { };
    public GameObject critRotate, panel;
    public DamageManager dm;
    public LootManager lm;
    public LevelManager lvlm;
    public Image hpIndi;

    private void Start()
    {
        Setup();
    }
    public virtual void Setup()
    {
        hp = maxHp;
        UpdateHpUI();
        aiHandler = GetComponent<AiHandler>();
    }
    public void CalculateDamage(List<DamageType> damages, bool isProj, float critChance)
    {
        if (!invincible)
        {
            DamageElement critElem = DamageElement.None;
            float totalValueAfterCalc = 0f;
            float totalvalueBeforeCalc = 0f;
            foreach (DamageType induced in damages)
            {
                float tempvalue = (float)induced.value;
                foreach (DamageType enemyweak in weakness)
                {
                    if (induced.damageElement == enemyweak.damageElement)
                    {
                        tempvalue = tempvalue * ((20 + (float)enemyweak.value) / 20);
                    }
                }
                foreach (DamageType enemystr in strength)
                {
                    if (induced.damageElement == enemystr.damageElement)
                    {
                        tempvalue = tempvalue * (20 / (20 + (float)enemystr.value));
                    }
                }
                totalvalueBeforeCalc += induced.value;
                totalValueAfterCalc += tempvalue;
            }
            float ifCrit = Random.Range(0, 20);
            bool didCrit = false;
            if (ifCrit < critChance)
            {
                didCrit = true;
                float whichCrit = Random.Range(1, totalvalueBeforeCalc);
                foreach (DamageType dtype in damages)
                {
                    if (totalvalueBeforeCalc - dtype.value < whichCrit && whichCrit <= totalvalueBeforeCalc)
                    {
                        if (dtype.damageElement == DamageElement.None)
                        {
                            totalValueAfterCalc *= 1.5f;
                        }
                        else
                        {
                            CritEffect(dtype.value, damageCrits[(int)dtype.damageElement - 1]);
                            critElem = dtype.damageElement;
                        }
                    }
                    totalvalueBeforeCalc -= dtype.value;
                }
            }
            int intvalue = Mathf.RoundToInt(totalValueAfterCalc);
            dm.DamageInflicted(intvalue, gameObject, isProj, didCrit);
            dm.DisplayDamage(panel, critElem, intvalue);
        }
        OnDamageTaken();
    }
    public virtual void OnDamageTaken()
    {

    }
    public virtual void UpdateHpUI()
    {
        float amount = (float)hp / (float)maxHp;
        hpIndi.fillAmount = amount;
    }
    public void CritEffect(int value, Crit crit)
    {
        //check if we already have the specific crit active right now
        for (int i = activeCrits.Count-1; i >= 0 ; i--)
        {
            Crit c = activeCrits[i];
            if (crit.GetType() == c.GetType())
            {
                if (crit.isTick)
                {
                    c.TickStack(value);
                    return;
                }
                else
                {
                    c.RemoveStatusEffect(this);
                }
            }
        }
        if (crit.isTick)
        {
            CritTickEffect(value, crit);
        }
        else
        {
            CritNonTickEffect(value, crit);
        }
    }
    public void CritTickEffect(int value, Crit crit)
    {
        Crit temp = Instantiate(crit);
        activeCrits.Add(temp);
        temp.TickSetUp(value);
        critRotate.GetComponent<CritRotator>().CreateCrits(activeCrits);
        StartCoroutine(temp.PerTick(this));
    }
    public void CritNonTickEffect(int value, Crit crit)
    {
        Crit temp = Instantiate(crit);
        activeCrits.Add(temp);
        critRotate.GetComponent<CritRotator>().CreateCrits(activeCrits);
        StartCoroutine(temp.EffectTimer(value, this));
    }
    public void InflictDamage(int damage, bool isProj, bool didCrit)
    {
        
    }
    public void ConfirmDamage(int value)
    {
        hp -= value;
        UpdateHpUI();
        if (hp <= 0)
        {
            Dead(true);
        }
    }
    public virtual void Dead(bool doDrop)
    {
        if (!doDrop)
        {
            chestDropMax = 0;
            maxGoldDrop = 0;
            minGoldDrop = 0;
        }
        else
        {
            dm.OnKill(this.gameObject);
        }
        for (int i = activeCrits.Count - 1; i >= 0; i--)
        {
            Crit c = activeCrits[i];
            if (!activeCrits[i].isTick)
            {

                c.RemoveStatusEffect(this);
            }
            else
            {
                c.ticksLeft.Clear();
            }
        }
        lvlm.enemies.Remove(this.gameObject);
        aiHandler.Dead();
    }
    public virtual void Drop()
    {
        lm.DropGold(Random.Range(minGoldDrop, maxGoldDrop), aiHandler.visuals.transform.position);
        int itemDropRand = Random.Range(0, chestDropMax);
        if (itemDropRand == 1 || chestDropMax == 1)
        {
            lm.DropChest(chestTier, transform.position);
        }
    }
    public void InvokeAnotherTick(Crit crit)
    {
        StartCoroutine(crit.PerTick(this));
    }
}
