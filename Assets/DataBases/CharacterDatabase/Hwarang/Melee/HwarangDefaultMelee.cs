using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HwarangDefaultMelee", menuName = "Melee/Hwarang/DefaultMelee", order = 109)]
public class HwarangDefaultMelee : Melee
{
    public bool visualize = false;
    public GameObject visualizerPrefab, hitParticle;
    public float range, knockBackmult, meleeDamageMult;
    public LayerMask layer;

    public override void InvokeMelee(AttackArgs aa)
    {
        Destroy(aa.projectile);
        Vector2 meleePos;
        float knockBackForce = aa.multval;
        if (aa.apc.pm.isRight)
        {
            meleePos = new Vector2(aa.apc.transform.position.x + range, aa.apc.transform.position.y);
        }
        else
        {
            meleePos = new Vector2(aa.apc.transform.position.x - range, aa.apc.transform.position.y);
        }
        //if (visualize)
        //{
        //    GameObject temp = Instantiate(visualizerPrefab, meleePos, Quaternion.identity);
        //    temp.GetComponent<LineRenderer>().positionCount = 4;
        //   temp.GetComponent<LineRenderer>().SetPosition(0, new Vector2(meleePos.x - range, meleePos.y));
        //    temp.GetComponent<LineRenderer>().SetPosition(1, new Vector2(meleePos.x, meleePos.y - range));
        //    temp.GetComponent<LineRenderer>().SetPosition(2, new Vector2(meleePos.x + range, meleePos.y));
        //    temp.GetComponent<LineRenderer>().SetPosition(3, new Vector2(meleePos.x, meleePos.y + range));
        //}
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePos, range, layer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                EnemyController ec = enemy.GetComponent<EnemyController>();
                ec.aiHandler.KnockBack(aa.apc.transform.position, knockBackForce);
                List<DamageType> damages = new List<DamageType>();
                foreach (DamageType damage in aa.apc.damageTypes)
                {
                    if (damage.value > 0)
                    {
                        float val = (damage.value * meleeDamageMult * aa.multval);
                        DamageType tempDamage = new DamageType
                        {
                            damageElement = damage.damageElement,
                            value = Mathf.RoundToInt(val)
                        };
                        damages.Add(tempDamage);
                    }
                }
                ec.CalculateDamage(damages, true, aa.apc.stats[3].value);
                GameObject particleobj = Instantiate(hitParticle, Vector2.Lerp(aa.apc.transform.position, enemy.gameObject.transform.position, 0.5f), Quaternion.identity);
                particleobj.GetComponent<ParticleSystem>().Play();
            }
        }
        base.InvokeMelee(aa);
    }
}
