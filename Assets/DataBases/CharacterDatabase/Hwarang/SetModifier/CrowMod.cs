using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "crow", menuName = "Modifier/Sets/crow", order = 110)]
public class CrowMod : Modifier
{
    public int counter;
    public GameObject Additional;
    public override void OnEnemyModActive(EnemyArg da)
    {
        base.OnEnemyModActive(da);
        counter++;
        if (counter == 3)
        {
            FireAdditional(da.epc, da.hitObj);
            counter = 0;
        }
    }
    private void FireAdditional(PlayerControl pc, GameObject hit)
    {
        var proj = Instantiate(Additional, pc.transform.position, Quaternion.identity);
        foreach (DamageType dt in pc.damageTypes)
        {
            DamageType temp = new DamageType
            {
                damageElement = dt.damageElement,
                value = dt.value
            };
            proj.GetComponent<TargettedProjectile>().damages.Add(temp);
        }
        proj.GetComponent<TargettedProjectile>().target = hit;
    }
}
