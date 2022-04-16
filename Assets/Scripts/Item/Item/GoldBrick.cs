using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoldBrick", menuName = "Modifier/Items/GoldBrick", order = 110)]
public class GoldBrick : Modifier
{
    public GameObject goldBar;
    int counter = 0;

    public override void OnEnemyModActive(EnemyArg da)
    {
        if (counter >= 9)
        {
            GameObject temp = Instantiate(goldBar, da.epc.transform.position, Quaternion.identity);
            temp.GetComponent<TargettedProjectile>().target = da.hitObj;
            List<DamageType> damages = new List<DamageType>();
            DamageType damage = new DamageType
            {
                damageElement = DamageElement.None,
                value = (int)da.damageAmount
            };
            damages.Add(damage);
            temp.GetComponent<TargettedProjectile>().damages = damages;
            counter = 0;
        }
        else
        {
            counter++;
        }
        base.OnEnemyModActive(da);
    }
}
