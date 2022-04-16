using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticle : MonoBehaviour
{
    public PlayerControl pc;
    public int damage;
    public bool doBlockHit;
    public GameObject rangeChecker;

    public void TriggerDamage()
    {
        if (rangeChecker.GetComponent<EnemyAttackRange>().avail)
        {
            pc.ph.OnPlayerHit(transform.position, damage);
        }
    }
    public void OnAnimFinished()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doBlockHit && collision.CompareTag("Projectile"))
        {
            collision.GetComponent<Projectile>().OnHit();
        }
    }
}
