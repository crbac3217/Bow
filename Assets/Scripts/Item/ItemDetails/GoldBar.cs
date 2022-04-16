using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBar : TargettedProjectile
{
    public GameObject coin;
    public override void DealDamage(EnemyController ec)
    {
        base.DealDamage(ec);
        DropCoin();
    }
    private void DropCoin()
    {
        var coint = Instantiate(coin, transform.position, Quaternion.identity);
    }
}
