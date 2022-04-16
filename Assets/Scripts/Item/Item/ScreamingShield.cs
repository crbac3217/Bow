using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScreamingShield", menuName = "Modifier/Items/ScreamingShield", order = 110)]
public class ScreamingShield : Modifier
{
    public int chanceMax;
    public float radius, multiplier;

    public override void OnModifierActive(PlayerControl pc)
    {
        int chance = Random.Range(0, chanceMax);

        if (chance == 1)
        {
            float norval = 0;
            foreach (DamageType dtype in pc.damageTypes)
            {
                norval += dtype.value * multiplier;
            }
            DamageType temp = new DamageType
            {
                damageElement = DamageElement.None,
                value = (int)norval
            };
            List<DamageType> damages = new List<DamageType>();
            damages.Add(temp);
            foreach (Collider2D col in Physics2D.OverlapCircleAll(pc.transform.position, radius))
            {
                if (col.gameObject.CompareTag("Enemy"))
                {
                    col.GetComponent<EnemyController>().CalculateDamage(damages, false, 0);
                    col.GetComponent<AiHandler>().KnockBack(pc.transform.position, 1);
                }
            }
        }
    }
}
