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
        int rand = Random.Range(1, 4);
        for (int i = 0; i < rand; i++)
        {
            var coint = Instantiate(coin, transform.position, Quaternion.identity);
        }
    }
}
