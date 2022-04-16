using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallingEnhanceObj : EnhanceObj
{
    public GameObject prefab;
    public float multval;
    public override void OnHit(EnemyController ec, HwarangDefaultProjectile proj)
    {
        GameObject inst = Instantiate(prefab, ec.transform.position, Quaternion.identity);
        foreach (DamageType dt in proj.damages)
        {
            float mult = dt.value + dt.value * multval;
            DamageType temp = new DamageType
            {
                damageElement = dt.damageElement,
                value = (int)mult
            };
            inst.GetComponent<CallingInstObj>().damages.Add(temp);
        }
        base.OnHit(ec, proj);
    }
}
