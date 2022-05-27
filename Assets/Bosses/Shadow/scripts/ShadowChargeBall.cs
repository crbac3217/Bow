using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowChargeBall : EnemyParticle
{
    public GameObject projectilePrefab;
    public int count;
    public void Explode()
    {
        for (int i = 0; i < count * 2; i++)
        {
            var inst = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            EnemyProjectile ep = inst.GetComponent<EnemyProjectile>();
            ep.dir = (Vector2)(Quaternion.Euler(0, 0, (i * 360 / count * 2)) * Vector2.right);
            ep.damage = damage;
        }
        pc.campar.StartCoroutine(pc.campar.CamShake(Vector2.up * 0.03f, 0.3f));
    }
}
