using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeType", menuName = "Archive/CreateMeleeType", order = 111)]
[System.Serializable]
public class MeleeType : ScriptableObject
{
    public int level;

    public virtual void Melee(bool isRight, float range, float held, float knockbackStr, float indicatorAmount, PlayerControl pc)
    {
        Vector2 meleePos;
        if (isRight)
        {
            meleePos = new Vector2(pc.transform.position.x + range, pc.transform.position.y);
        }
        else
        {
            meleePos = new Vector2(pc.transform.position.x - range, pc.transform.position.y);
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePos, range);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                EnemyController ec = enemy.GetComponent<EnemyController>();
                ec.aiHandler.KnockBack(pc.transform.position, knockbackStr * (level + held/indicatorAmount));
                Debug.Log(indicatorAmount);
                List<DamageType> damages = new List<DamageType>();
                foreach (DamageType damage in pc.damageTypes)
                {
                    if (damage.value > 0)
                    {
                        DamageType tempDamage = new DamageType
                        {
                            damageElement = damage.damageElement,
                            value = Mathf.RoundToInt(damage.value * (level + held / indicatorAmount) * 0.85f)
                        };
                        damages.Add(tempDamage);
                    }
                }
                ec.CalculateDamage(damages, true, pc.stats[3].value);
            }
        }
    }
}
