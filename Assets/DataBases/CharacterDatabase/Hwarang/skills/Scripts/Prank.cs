using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crit_Prank", menuName = "Crits/Hwarang/Prank", order = 110)]
public class Prank : Skill
{
    public float additionalMult;
    public LayerMask layer;
    public GameObject prefab, instance;
    public override void ActivePress(PlayerControl pc)
    {
        base.ActivePress(pc);
        instance = Instantiate(prefab, GroundHitPoint(pc.gameObject.transform.position), Quaternion.identity);
        instance.transform.localScale = new Vector2(instance.transform.localScale.x * pc.pm.body.transform.localScale.x, instance.transform.localScale.y);
        foreach (DamageType dtype in pc.damageTypes)
        {
            float multval = dtype.value + (dtype.value * additionalMult);
            DamageType temp = new DamageType
            {
                damageElement = dtype.damageElement,
                value = (int)multval
            };
            instance.GetComponent<DokebiInst>().damages.Add(temp);
        }
        instance.GetComponent<DokebiInst>().campar = pc.campar;
    }
    private Vector2 GroundHitPoint(Vector2 startpos)
    {
        RaycastHit2D hit = Physics2D.Raycast(startpos, -Vector2.up, layer);
        if (hit)
        {
            return hit.point;
        }
        else
        {
            return startpos;
        }
    }
    public override void ActiveRelease(PlayerControl pc)
    {
        base.ActiveRelease(pc);
        instance.GetComponent<DokebiInst>().anim.SetTrigger("skillReleased");
    }
}
