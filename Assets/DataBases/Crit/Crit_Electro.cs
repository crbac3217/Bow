using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_Electro", menuName = "Crits/Hwarang/Electro", order = 111)]
public class Crit_Electro : Crit
{
    public Electrocuted elecc;
    public float radius, minDam;
    public int targettableEnemies;
    public GameObject electroLine;
    public override void StatusEffect(int value, EnemyController ec)
    {
        base.StatusEffect(value, ec);
        elecc = ScriptableObject.CreateInstance<Electrocuted>();
        elecc.ect = ec;
        elecc.radius = radius;
        elecc.minDam = minDam;
        elecc.valuet = value / 3f;
        elecc.targettableEnemies = targettableEnemies;
        elecc.electroLine = electroLine;
        ec.dm.damagedMods.Add(elecc);
    }
    public override void RemoveStatusEffect(EnemyController ec)
    {
        ec.dm.damagedMods.Remove(elecc);
        base.RemoveStatusEffect(ec);
        
    }
}
[CreateAssetMenu(fileName = "Electrocuted", menuName = "Modifier/Crit/Electrocuted", order = 110)]
public class Electrocuted : Modifier
{
    public float radius, minDam, valuet;
    public int targettableEnemies;
    public EnemyController ect;
    public GameObject electroLine;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        if (da.hitObj == ect.gameObject)
        {
            float counter = targettableEnemies;
            foreach (Collider2D col in Physics2D.OverlapCircleAll(ect.gameObject.transform.position, radius * ect.gameObject.transform.localScale.x))
            {
                if (col.gameObject.CompareTag("Enemy"))
                {
                    if (counter > 0)
                    {
                        counter--;
                        Electrocute(da.hitObj, col.gameObject);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
    public void Electrocute(GameObject from, GameObject to)
    {
        GameObject Line = Instantiate(electroLine, from.transform.position, Quaternion.identity);
        ElectroLine elec = Line.GetComponent<ElectroLine>();
        elec.val = valuet;
        elec.from = from;
        elec.to = to;
    }
}
