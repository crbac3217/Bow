using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmpleProjectile : Projectile
{
    public bool isEnhanced;
    public GameObject explosion;
    private Vector2 startpos;
    public int maxPen;
    private int nonedam;
    private List<EnemyController> hitEnemies = new List<EnemyController>() { };
    private List<AmpleExplosion> explosions = new List<AmpleExplosion>() { };
    public override void SetUp()
    {
        base.SetUp();
        startpos = transform.position;
        foreach (DamageType dt in damages)
        {
            if (dt.damageElement == DamageElement.None)
            {
                nonedam = dt.value;
            }
        }
    }
    public override void OnCollisionEnemy(EnemyController ec)
    {
        GetComponent<AudioSource>().clip = enemyImpact;
        GetComponent<AudioSource>().Play();
        DealDamage(ec);
        if (ec.hp > 0)
        {
            ec.aiHandler.KnockBack(startpos, 0.5f);
        }
        if (isEnhanced)
        {
            InstantiateExplosion(ec.gameObject);
        }
        hitEnemies.Add(ec);
        if (maxPen > 0)
        {
            maxPen--;
        }
        else
        {
            isHit = true;
            if (anim != null)
            {
                anim.enabled = false;
            }
            transform.SetParent(ec.transform);
            disappear = true;
            rb.bodyType = RigidbodyType2D.Static;
            Destroy(GetComponent<Collider2D>());
        }
    }
    public override void OnHit()
    {
    }
    public void InstantiateExplosion (GameObject go)
    {
        var exp = Instantiate(explosion, go.transform.position, Quaternion.identity);
        exp.transform.SetParent(go.transform);
        explosions.Add(exp.GetComponent<AmpleExplosion>());
    }
    public override void DestroyEdit()
    {
        if (isEnhanced)
        {
            foreach (EnemyController econ in hitEnemies)
            {
                if (econ)
                {
                    DealDamage(econ);
                }
            }
            foreach (AmpleExplosion ex in explosions)
            {
                if (ex)
                {
                    ex.Activate();
                }
            }
        }
        base.DestroyEdit();
    }
}
