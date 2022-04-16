using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceObj_Bite : EnhanceObj
{
    public DamageElement delem;
    public GameObject biteObj;
    public float addmult;

    public override void OnHit(EnemyController ec, HwarangDefaultProjectile proj)
    {
        var instance = Instantiate(biteObj, ec.transform.position, Quaternion.identity);
        instance.transform.SetParent(ec.transform);
        if (proj.transform.position.x > ec.transform.position.x)
        {
            instance.transform.localScale = new Vector3(-instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }
        else
        {
            instance.transform.localScale = new Vector3(instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
        }
        instance.GetComponent<BiteObj>().enemy = ec;
        foreach (DamageType dt in proj.damages)
        {
            float totval = dt.value * addmult + dt.value;
            if (dt.damageElement != DamageElement.None)
            {
                if (dt.damageElement == delem)
                {
                    totval *= 2;
                }
            }
            DamageType temp = new DamageType
            {
                damageElement = dt.damageElement,
                value = (int)totval
            };
            instance.GetComponent<BiteObj>().damages.Add(temp);
        }
        base.OnHit(ec, proj);
    }
}
